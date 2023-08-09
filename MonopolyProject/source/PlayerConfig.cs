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
			_pieceColor = GetPiece();
			_position = 1;
			_balance = 2000;
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
			while (_position == 31)
			{
				SetPositionToNew(11);
				_isInJail = true;
				return true;
			}
			return false;
		}
		public bool IsInJail()
		{
			if (_position == 11 && _isInJail == true)
			{
				return true;
			}
			return false;
		}
		public string? OpenCard()
		{
			return _keptCard?.OpenCard();
		}
		public bool SetKeptCard(ICard card)
		{
			if (_keptCard == null)
			{
				_keptCard = card;
				return true;
			}
			return false;
		}
		public bool UseCard(ICard card)
		{
			if (_keptCard == card)
			{
				_keptCard = null;
				return true;
			}
			return false;
		}
		public bool AddProperty(Dictionary<Tile, KeyValuePair<string,int>> properties)
		{
			if (_property.Keys != properties.Keys)
			{
				return true;
			}
			return false;
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