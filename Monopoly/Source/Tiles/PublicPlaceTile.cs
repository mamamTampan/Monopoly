namespace MonopolyProjectSource;

public class PublicPlaceTile : Tile
{
	private int _amount;
	private int _rent;
	public PublicPlaceTile(string name, int location, string description, int amount, int rent)
	{
		this._type = TileType.PUBLIC_PLACE;
		this._name = name;
		this._location = location;
		this._description = description;
		this._amount = amount;
		this._rent = rent;
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