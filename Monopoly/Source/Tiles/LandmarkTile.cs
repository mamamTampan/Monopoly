using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public class LandmarkTile : Tile
{
	private IPlayer? _owner;
	private bool _hasProperty = false;
	private int _initialPrice;
	private int _housePrice;
	private int _hotelPrice;
	private int _rent;
	private int _houseTotal;
	private int _hotelTotal;
	private int _maxHouse;
	private int _maxHotel;

	public LandmarkTile(string name, int location, string description, bool hasProperty, int initialPrice, int rent, int houseTotal, int hotelTotal, int housePrice, int hotelPrice, int maxHouse, int maxHotel)
	{
		this._type = TileType.LANDMARK;
		this._name = name;
		this._location = location;
		this._description = description;
		this._hasProperty = hasProperty;
		this._initialPrice = initialPrice;
		this._housePrice = housePrice;
		this._hotelPrice = hotelPrice;
		this._rent = rent;
		this._houseTotal = houseTotal;
		this._hotelTotal = hotelTotal;
		this._maxHouse = maxHouse;
		this._maxHotel = maxHotel;
		this._owner = null;
	}
	public override TileType Type => _type;
	public override string? GetName()
	{
		return _name;
	}
	public override int GetLocation()
	{
		return _location;
	}
	public override string? GetDescription()
	{
		return _description;
	}
	public bool SetOwner(IPlayer player)
	{
		if (_owner == null)
		{
			_hasProperty = true;
			this._owner = player;
			return true;
		}
		return false;
	}

	public IPlayer? GetOwner()
	{
		return _owner;
	}
	public bool HasOwner()
	{
		_hasProperty = true;
		return _hasProperty;
	}
	public int GetMaxHouse()
	{
		return _maxHouse;
	}
	public int GetMaxHotel()
	{
		return _maxHotel;
	}
	public int GetHouseTotal()
	{
		return _houseTotal;
	}
	public int GetHotelTotal()
	{
		return _hotelTotal;
	}
	public int GetInitialPrice()
	{
		return _initialPrice;
	}
	public int GetHousePrice()
	{
		return _housePrice;
	}
	public int GetHotelPrice()
	{
		return _hotelPrice;
	}
	public int GetRent()
	{
		return _rent;
	}
}