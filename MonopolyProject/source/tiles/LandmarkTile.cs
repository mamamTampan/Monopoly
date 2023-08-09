using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public class LandmarkTile : Tile
{
	private IPlayer _owner;
	private bool _hasProperty;
	private int _initialPrice;
	private int _housePrice;
	private int _hotelPrice;
	private int _rent;
	private int _houseTotal;
	private int _hotelTotal;
	private int _maxHouse;
	private int _maxHotel;

	public LandmarkTile(string name, int location, string description)
	{
		this._type = TileType.LANDMARK;
		this._name = name;
		this._location = location;
		this._description = description;
		_hasProperty = false;
		//SetOwner();
	}
	public override TileType GetType()
	{
		return _type;
	}
	public override string GetName()
	{
		return _name;
	}
	public override int GetLocation()
	{
		return _location;
	}
	public override string GetDescription()
	{
		return _description;
	}
	public bool SetOwner(IPlayer player)
	{
		_hasProperty = false;
		return true;
	}
	public IPlayer GetOwner()
	{
		return _owner;
	}
	public bool HasOwner()
	{
		_owner = null;
		return true;
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