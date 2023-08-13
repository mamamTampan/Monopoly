namespace MonopolyProjectSource;

public class TaxTile : Tile
{
	private int _amount;
	public TaxTile(string name, int location, string description, int amount)
	{
		this._type = TileType.TAX;
		this._name = name;
		this._location = location;
		this._description = description;
		this._amount = amount;
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
	public int GetAmount()
	{
		return _amount;
	}
}