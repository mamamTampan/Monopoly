namespace MonopolyProjectSource;

public class ChanceTile : Tile
{
	public ChanceTile(string name, int location, string description)
	{
		this._type = TileType.CHANCE_CARD;
		this._name = name;
		this._location = location;
		this._description = description;
	}
	public override TileType Type => _type;
    public override string GetName()
	{
		_name = "Chance";
		return _name;
	}
	public override int GetLocation()
	{
		return _location;
	}
	public override string GetDescription()
	{
		_description = "Chance";
		return _description;
	}
}