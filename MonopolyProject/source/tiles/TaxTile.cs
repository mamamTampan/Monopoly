namespace MonopolyProjectSource;

public class TaxTile : Tile
{
	private int _amount;
	public TaxTile(string name, int location, string description)
	{
		this._type = TileType.TAX;
		this._name = name;
		this._location = location;
		this._description = description;
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
	public int GetAmount()
	{
		if (_location == 5)
		{
			_amount = 200;
		}
		else if (_location == 39)
		{
			_amount = 100;
		}
		return _amount;
	}
}