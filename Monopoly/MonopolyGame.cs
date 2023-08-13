using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public class MonopolyGame
{
	private Board board;
	private GameStatus gameStatus;
	private List<IDice> dices;
	private CardDeck cardDeck;
	private IPlayer currentPlayer;
	private Dictionary<IPlayer, PlayerConfig> playerSet;
	private List<IPlayer> TurnsOrder;
	private List<ChanceCardType> _chanceCards;
	private List<CommunityCardType> _commCards;
	private Stack<ChanceCard> _chanceDeck;
	private Stack<CommunityCard> _commDeck;

	public MonopolyGame()
	{
		board = new();
		gameStatus = GameStatus.NOT_STARTED;
		dices = new List<IDice>
		{
			new Dice(),
			new Dice()
		};
		_commCards = new();
		_chanceCards = new List<ChanceCardType>
		{
			ChanceCardType.Fine,
			ChanceCardType.Reward,
			ChanceCardType.Tax,
			ChanceCardType.GoToJail,
			ChanceCardType.FreeFromJail,
			ChanceCardType.HeadToLandmark,
			ChanceCardType.BackToLandmark,
			ChanceCardType.StepForward,
			ChanceCardType.StepBack,
			ChanceCardType.HeadToStart
		};
		cardDeck = new CardDeck(_chanceCards);
		_chanceDeck = new();
		_commDeck = new();
		playerSet = new Dictionary<IPlayer, PlayerConfig>();
		TurnsOrder = new List<IPlayer>();
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
			if (!playerSet.ContainsKey(player))
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
		TurnsOrder = new();
		foreach (var key in playerSet.Keys)
		{
			TurnsOrder.Add(key);
		}
		return TurnsOrder;
	}
	public IPlayer GetCurrentTurn()
	{
		return currentPlayer;
	}
	public int ThrowDices(int index)
	{
		return dices[index].Roll();
	}
	public void SetTurnsOrder()
	{
		TurnsOrder = TurnsOrder.OrderBy(_ => Guid.NewGuid()).ToList();
	}
	public bool SetInitialState()
	{
		board.CreatingBoard();
		cardDeck.ShuffleCard(_chanceDeck);
		cardDeck.ShuffleCard(_commDeck);
		gameStatus = GameStatus.ONGOING;
		return true;
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
		int position = 0;
		if (playerSet.ContainsKey(player))
		{
			foreach (var playerConfig in playerSet.Values)
			{
				position += playerConfig.GetPosition();
			}
		}
		return position;
	}

	public int CheckPlayerBalance(IPlayer player)
	{
		int balance = 0;
		if (playerSet.ContainsKey(player))
		{
			foreach (var playerConfig in playerSet.Values)
			{
				balance += playerConfig.GetBalance();
			}
		}
		return balance;
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
		int finalPosition = initialPosition + ThrowDices(0) + ThrowDices(1);
		if (finalPosition > initialPosition && finalPosition == 0)
		{
			int startReward = 200;
			playerConfig.IncreaseBalance(startReward);
		}
		return true;

	}
	public ICard? TakeChanceCard()
	{
		var chance = new Stack<ChanceCard>();
		if (chance.Count > 0)
		{
			chance.Pop();
			var card = playerSet[currentPlayer].OpenCard();
		}
		return null;
	}


	public ICard? TakeCommCard()
	{
		var comm = new Stack<CommunityCard>();
		if (comm.Count > 0)
		{
			comm.Pop();
			var card = playerSet[currentPlayer].OpenCard();
		}
		return null;
	}


	public bool ExecuteCard(ICard card)
	{
		var playerConfig = playerSet[currentPlayer];
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
				_ = playerSet[player].DecreaseBalance(taxAmount);
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
				_ = playerSet[player].DecreaseBalance(initLandmark);
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
		}
		else if (gameStatus == GameStatus.ONGOING)
		{
			var index = TurnsOrder.IndexOf(currentPlayer);
			if (index != -1)
			{
				var nextIndex = (index + 1) % TurnsOrder.Count;
				currentPlayer = TurnsOrder[nextIndex];
			}
		}
		return true;
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