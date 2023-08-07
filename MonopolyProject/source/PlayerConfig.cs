using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public interface IPlayerConfig : IPlayerCard, IProperty, IJail, IBalance, IPosition, IPiece{}

	public class PlayerConfig : IPlayerConfig
	{
		private PieceType _pieceColor;
		private int _position;
		private int _balance;
		private bool _isInJail;
		private ICard? _keptCard;
		private Dictionary<Tile, KeyValuePair<string, int>> _property;

		public PlayerConfig()
		{
			_position = 1;
			_balance = 1500;
			_isInJail = false;
			_keptCard = null;
			_property = new();
		}
		public bool SetPiece(PieceType pieceType)
		{
			_pieceColor = pieceType;
			return true;
		}
		public PieceType GetPiece()
		{
			return _pieceColor;
		}
		public bool SetPositionToNew(int newPosition)
		{
			_position = newPosition;
			return true;
		}
		public bool SetPositionFromDice(int diceValue)
		{
			_position += diceValue;
			return true;
		}
		public int GetPosition()
		{
			return _position;
		}
		public bool SetBalance(int balance)
		{
			_balance = balance;
			return true;
		}
		public int GetBalance()
		{
			return _balance;
		}
		public bool IncreaseBalance(int incBalance)
		{
			_balance += incBalance;
			return true;
		}
		public bool DecreaseBalance(int decBalance)
		{
			_balance -= decBalance;
			return true;
		}
		public bool GoToJail()
		{
			_position = 31;
			return true;
		}
		public bool IsInJail()
		{
			if (_position == 11)
			{
				
			}
			return _isInJail;
		}
		public string? OpenCard()
		{
			return _keptCard;
		}
		public bool SetKeptCard(ICard card)
		{
			_keptCard = card;
			return true;
		}
		public bool UseCard(ICard card)
		{
			_keptCard = card;
			return true;
		}
		public bool AddProperty(Dictionary<Tile, KeyValuePair<string, int>> properties)
		{
			_property = properties;
			return true;
		}
		public bool SellProperty(Tile property)
		{
			if (_property.ContainsKey(property))
			{
				_property.Remove(property);
				return true;
			}
			return false;
		}
		public Dictionary<Tile, KeyValuePair<string, int>> GetProperty()
		{
			return _property;
		}
	}