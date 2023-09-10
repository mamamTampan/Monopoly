using MonopolyProjectInterface;
using MonopolyLog;

namespace MonopolyProjectSource;

public delegate void GameInitializationDelegate();
public class MonopolyGame
{
	private Board board;
	private GameStatus gameStatus;
	private CardDeck cardDeck;
	private IPlayer currentPlayer;
	private Dictionary<IPlayer, PlayerConfig> playerSet;
	private List<IPlayer> TurnsOrder;
	private List<int> _totalDice;
	private List<int> _twinDice;

	public MonopolyGame()
	{
		board = new();
		gameStatus = GameStatus.NOT_STARTED;
		cardDeck = new CardDeck();
		playerSet = new Dictionary<IPlayer, PlayerConfig>();
		TurnsOrder = new List<IPlayer>();
		currentPlayer = new HumanPlayer(0, "");
		_totalDice = new List<int>();
		_twinDice = new List<int>();
	}

	public GameStatus CheckGameStatus()
	{
		Log.Instance.Info(" Check Game Status ");
		
		return gameStatus;
	}

	public bool AddPlayer(IPlayer player)
	{
		Log.Instance.Info(" Add every players ");
		
		if (playerSet.Count <= 8)
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
		foreach (var key in playerSet.Keys)
		{
			TurnsOrder.Add(key);
		}
		return TurnsOrder;
	}

	public IPlayer GetCurrentTurn(IPlayer player)
	{
		return currentPlayer;
	}

	public void ThrowDice()
	{
		_totalDice.Clear();
		var firstDice = new Dice().Roll();
		var secondDice = new Dice().Roll();
		_totalDice.Add(firstDice);
		_totalDice.Add(secondDice);
	}

	public int ThrowDices(int index)
	{
		return _totalDice[index];
	}

	public int TwinDice()
	{
		_twinDice.Add(0);
		if (_totalDice[0] == _totalDice[1])
		{
			_twinDice[0]++;
			if (_twinDice[0] == 3)
			{
				_twinDice[0] = 0;
				return 3;
			}
			else if (_twinDice[0] == 2)
			{
				return 2;
			}
			return 1;
		}
		else
		{
			_twinDice[0] = 0;
		}
		return 0;
	}

	public void SetTurnsOrder()
	{
		Random random = new();
		if (TurnsOrder.Contains(currentPlayer))
		{
			TurnsOrder = TurnsOrder.OrderByDescending(_ => random.Next())
								   .ToList();
		}
	}

	public event GameInitializationDelegate? GameInitialized;
	public bool SetInitialState()
	{
		board.CreatingBoard();
		SetTurnsOrder();
		gameStatus = GameStatus.ONGOING;

		GameInitialized?.Invoke();
		return true;
	}

	public bool Move(IPlayer player, int step)
	{
		if (playerSet.ContainsKey(player))
		{
			var playerConfig = playerSet[player];
			step = _totalDice.Sum();
			playerConfig.SetPositionFromDice(step);
			return true;
		}
		return false;
	}

	public string? TileName(int position)
	{
		var tile = board.GetTileNameAtPosition(position);
		return tile;
	}

	public int CheckPlayerPosition(IPlayer player)
	{
		if (playerSet.ContainsKey(player))
		{
			var playerConfig = playerSet[player];
			return playerConfig.GetPosition() % 40;
		}
		else
		{
			throw new ArgumentException("Player not found");
		}
	}

	public int CheckPlayerBalance(IPlayer player)
	{
		var playerConfig = playerSet[player];
		if (playerSet.ContainsKey(player))
		{
			return playerConfig.GetBalance();
		}
		else
		{
			throw new ArgumentException("Player not found.");
		}
	}

	public Dictionary<Tile, KeyValuePair<string, int>> CheckPlayerProperties(IPlayer player)
	{
		if (playerSet.ContainsKey(player))
		{
			var playerConfig = playerSet[player];
			return playerConfig.GetProperty();
		}
		else
		{
			throw new ArgumentException("Player not found.");
		}
	}

	public bool SetToJail(IPlayer player)
	{
		if (playerSet.ContainsKey(player))
		{
			var playerConfig = playerSet[player];
			playerConfig.IsInJail();
			return true;
		}
		return false;
	}

	public bool GrantRegular(IPlayer player)
	{
		if (playerSet.ContainsKey(player))
		{
			var playerConfig = playerSet[player];
			int initialPosition = playerConfig.GetPosition();
			int finalPosition = initialPosition + _totalDice.Sum();
			if (finalPosition > initialPosition && finalPosition == 0)
			{
				int startReward = 200;
				playerConfig.IncreaseBalance(startReward);
				return true;
			}
		}
		return false;
	}
	public ICard? TakeChanceCard()
	{
		cardDeck.DrawChanceCard();
		var card = playerSet[currentPlayer].OpenCard();
		return null;
	}

	public ICard? TakeCommCard()
	{
		cardDeck.DrawCommunityCard();
		var card = playerSet[currentPlayer].OpenCard();
		return null;
	}

	public bool ExecuteCard(ICard card)
	{
		var cardMove = new CardMove();
		var playerConfig = playerSet[currentPlayer];
		playerConfig.UseCard(card);
		return true;
	}

	public bool SetCardToPlayer(ICard card, IPlayer player)
	{
		if (playerSet.ContainsKey(player))
		{
			var playerConfig = playerSet[player];
			playerConfig.SetKeptCard(card);
			return true;
		}
		return false;
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

	public List<IPlayer> GetRichestPlayers()
	{
		TurnsOrder = GetPlayers();
		var richestList = TurnsOrder.OrderByDescending(player => CheckPlayerBalance(player))
											  .ToList();

		return richestList;
	}

	public IPlayer? CheckWinner()
	{
		TurnsOrder = GetRichestPlayers();
		var richestPlayer = TurnsOrder.FirstOrDefault();
		if (richestPlayer != null)
		{
			gameStatus = GameStatus.WIN;
			return richestPlayer;
		}
		return null;
	}
}