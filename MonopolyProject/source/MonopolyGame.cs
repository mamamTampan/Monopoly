using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public class MonopolyGame : IDice
{
	private Board board;
	private GameStatus gameStatus;
	private List<IDice> dices;
	private CardDeck cardDeck;
	private IPlayer currentPlayer;
	private Dictionary<IPlayer, PlayerConfig> playerSet;
	private List<IPlayer> TurnsOrder;


	public MonopolyGame()
	{
		board = new Board();
		gameStatus = GameStatus.NOT_STARTED;
		dices = new List<IDice>();
		cardDeck = new CardDeck();
		playerSet = new Dictionary<IPlayer, PlayerConfig>();
		TurnsOrder = new List<IPlayer>();
		diceSide = 6;
		diceDoubleCount = 0;
		currentPlayer = new HumanPlayer(0, "");
	}

	public GameStatus CheckGameStatus()
	{
		return gameStatus;
	}
	public bool AddPlayer(IPlayer player)
	{
		if (playerSet.Count < 2)
		{
			if (playerSet.ContainsKey(player))
			{
				var playerConfig = new PlayerConfig();
				playerSet.Add(player, playerConfig);
				return true;
			}
		}
		return false;
	}
	public List<IPlayer> GetPlayers()
	{
		foreach (var key in playerSet.Keys)
		{
			TurnsOrder.Add(key);
		}
		return TurnsOrder;
	}
	public int ThrowDices()
	{
		var diceValue1 = dices[0].Roll();
		var diceValue2 = dices[1].Roll();
		return diceValue1 += diceValue2;
	}

	public int ThrowDices(int index)
	{
		if (index < 0 || index >= dices.Count)
		{
			throw new ArgumentOutOfRangeException(nameof(index), "Invalid dice index");
		}

		return dices[index].Roll();
	}

	public bool SetInitialState()
	{

		board = new Board();
		board.CreatingBoard();
		cardDeck.ShuffleCard<CommunityCard>(new Stack<CommunityCard>());
		cardDeck.ShuffleCard<ChanceCard>(new Stack<ChanceCard>());
		foreach (var player in GetPlayers())
		{
			var playerConfig = new PlayerConfig();
			playerSet.Add(player, playerConfig);
		}
		gameStatus = GameStatus.ONGOING;
		SetTurnsOrder();
		return true;
	}

	public void SetTurnsOrder()
	{
		var playerOrder = new Dictionary<IPlayer, int>();
		var usedDiceResults = new List<int>();
		foreach (var player in TurnsOrder)
		{
			int diceResult = ThrowDices();
			while (usedDiceResults.Contains(diceResult))
			{
				diceResult = ThrowDices();
			}
			usedDiceResults.Add(diceResult);
			playerOrder.Add(player, diceResult);
		}

		TurnsOrder = playerOrder.OrderByDescending(pair => pair.Value)
								.Select(pair => pair.Key)
								.ToList();
		currentPlayer = TurnsOrder[0];
	}

	public IPlayer GetCurrentTurn()
	{
		return currentPlayer;
	}

	public bool Move(IPlayer player, int step)
	{
		if (playerSet.ContainsKey(player))
		{
			var playerConfig = playerSet[player];
			playerConfig.SetPositionFromDice(step);
			return true;
		}

		return false;
	}

	public int CheckPlayerPosition(IPlayer player)
	{
		var playerConfig = playerSet[player];
		return playerConfig.GetPosition();
	}

	public int CheckPlayerBalance(IPlayer player)
	{
		var playerConfig = playerSet[player];
		return playerConfig.GetBalance();
	}

	public Dictionary<Tile, KeyValuePair<string, int>> CheckPlayerProperties(IPlayer player)
	{
		var playerConfig = playerSet[player];
		return playerConfig.GetProperty();
	}

	public bool SetToJail(IPlayer player)
	{
		var playerConfig = playerSet[player];
		return playerConfig.IsInJail();
	}

	public bool GrantRegular(IPlayer player)
	{
		var playerConfig = playerSet[player];
		int initialPosition = playerConfig.GetPosition();
		int finalPosition = initialPosition + ThrowDices();
		if (finalPosition > initialPosition && finalPosition == 0)
		{
			int startReward = 200;
			playerConfig.IncreaseBalance(startReward);
		}
		return true;

	}/*
	public ICard TakeChanceCard()
		{
			var card = cardDeck.ShuffleCard<ChanceCard>(Stack<ChanceCard>);
			if (cardDeck.ShuffleCard<ChanceCard>(chanceCards). > 0)
				{
					ChanceCard chanceCard = cardDeck.ChanceDeck.Pop();
					currentPlayer.SetKeptCard(chanceCard);
					return chanceCard;
				}
			return null;
			}
			//public ICard TakeCommCard()
			//{
			//	var comm = new Stack<CommunityCard>();
			//	if (comm.Count > 0)
			//	{
			//		comm.Pop();
			//		var card = playerSet[currentPlayer].OpenCard();
			//	}
		//}*/

	public bool ExecuteCard(ICard card)
	{
		var playerConfig = playerSet[currentPlayer];
		//	var chance = new ChanceCard(s);
		//	var cardMove = new CardMove();
		// if (chanceChanceCardType.BackToLandmark){cardMove.FineCard(playerConfig);}
		playerConfig.UseCard(card);
		return true;
	}

	public bool SetCardToPlayer(ICard card, IPlayer player)
	{
		var playerConfig = playerSet[player];
		playerConfig.SetKeptCard(card);
		return true;
	}

	public TransactionStatus TakeTax(IPlayer player, Tile tile)
	{
		if (playerSet.ContainsKey(player))
		{
			if (tile is TaxTile taxTile)
			{
				taxTile.GetLocation();
				var taxAmount = taxTile.GetAmount();
				var success = playerSet[player].DecreaseBalance(taxAmount);
				return TransactionStatus.SUCCESSFUL;
			}
			else
			{
				return TransactionStatus.PAID;
			}
		}
		return TransactionStatus.BALANCE_NOT_ENOUGH;

	}
	public TransactionStatus PlayerBuyLandmark(IPlayer player, Tile tile)
	{
		if (playerSet.ContainsKey(player))
		{
			if (tile is LandmarkTile landmark)
			{
				landmark.GetLocation();
				var initLandmark = landmark.GetInitialPrice();
				var success = playerSet[player].DecreaseBalance(initLandmark);
				return TransactionStatus.SUCCESSFUL;
			}
			else
			{
				return TransactionStatus.PAID;
			}
		}
		return TransactionStatus.BALANCE_NOT_ENOUGH;
	}
	public TransactionStatus PlayerPayRent(IPlayer playerRent, Tile tile)
	{
		if (playerSet.ContainsKey(playerRent))
		{
			if (tile is LandmarkTile landmark)
			{
				landmark.GetLocation();
				var taxAmount = landmark.GetRent();
				var success = playerSet[playerRent].DecreaseBalance(taxAmount);
				return TransactionStatus.SUCCESSFUL;
			}
			else
			{
				return TransactionStatus.PAID;
			}
		}
		return TransactionStatus.BALANCE_NOT_ENOUGH;

	}
	public TransactionStatus PlayerPayResource(IPlayer player, Tile tile)
	{
		if (playerSet.ContainsKey(player))
		{
			if (tile is PublicPlaceTile publicPlace)
			{
				publicPlace.GetLocation();
				var taxAmount = publicPlace.GetAmount();
				var success = playerSet[player].DecreaseBalance(taxAmount);
				return TransactionStatus.SUCCESSFUL;
			}
			else
			{
				return TransactionStatus.PAID;
			}
		}
		return TransactionStatus.BALANCE_NOT_ENOUGH;

	}
	public TransactionStatus PlayerBuyHouse(IPlayer player, Tile tile)
	{
		if (playerSet.ContainsKey(player))
		{
			if (tile is LandmarkTile landmark)
			{
				landmark.GetLocation();
				var taxAmount = landmark.GetHousePrice();
				var success = playerSet[player].DecreaseBalance(taxAmount);
				return TransactionStatus.SUCCESSFUL;
			}
			else
			{
				return TransactionStatus.PAID;
			}
		}
		return TransactionStatus.BALANCE_NOT_ENOUGH;

	}
	public TransactionStatus PlayerBuyHotel(IPlayer player, Tile tile)
	{
		if (playerSet.ContainsKey(player))
		{
			if (tile is LandmarkTile landmark)
			{
				landmark.GetLocation();
				var taxAmount = landmark.GetHotelPrice();
				var success = playerSet[player].DecreaseBalance(taxAmount);
				return TransactionStatus.SUCCESSFUL;
			}
			else
			{
				return TransactionStatus.PAID;
			}
		}
		return TransactionStatus.BALANCE_NOT_ENOUGH;
	}
	
	public TransactionStatus PlayerSellLandmark(IPlayer player, Tile tile)
	{
		if (playerSet.ContainsKey(player))
		{
			if (tile is LandmarkTile landmark)
			{
				landmark.GetLocation();
				var taxAmount = landmark.GetInitialPrice();
				var success = playerSet[player].IncreaseBalance(taxAmount);
				return TransactionStatus.SUCCESSFUL;
			}
			else
			{
				return TransactionStatus.PAID;
			}
		}
		return TransactionStatus.BALANCE_NOT_ENOUGH;
	}
	
	public bool SetNextTurn()
	{
		if (gameStatus == GameStatus.NOT_STARTED)
		{
			gameStatus = GameStatus.ONGOING;
			currentPlayer = TurnsOrder[0];
			return true;
		}
		else if (gameStatus == GameStatus.ONGOING)
		{
			var index = TurnsOrder.IndexOf(currentPlayer);
			if (index != -1)
			{
				var nextIndex = (index + 1) % TurnsOrder.Count;
				currentPlayer = TurnsOrder[nextIndex];
				return true;
			}
		}
		return false;
	}
	
	public IPlayer CheckRichest()
	{
		//currentPlayer = null;
		var highestBalance = int.MinValue;
		foreach (var player in playerSet.Keys)
		{
			var playerConfig = playerSet[player];
			var playerBalance = playerConfig.GetBalance();
			if (playerBalance > highestBalance)
			{
				currentPlayer = player;
				highestBalance = playerBalance;
			}
		}
		return currentPlayer;
	}
	
	public IPlayer CheckWinner()
	{
		gameStatus = GameStatus.WIN;
		return currentPlayer;
	}
}