namespace MonopolyProjectSource;

public class StartTile : Tile
{
	private int _amountRegular;
	public StartTile(string name, int location, string description)
	{
		this._type = TileType.START;
		this._name = name;
		this._location = location;
		this._description = description;
		GetAmount();
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
		return _amountRegular;
	}
}