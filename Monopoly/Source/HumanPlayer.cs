using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public class HumanPlayer : IPlayer
{
	private int _id = 0;
	private string? _name = "";
	public HumanPlayer(int id,string? name)
	{
		SetId(id);
		SetName(name);
	}
	public bool SetName(string? name)
	{
		if ( _id > 0 )
		{
			this._name = name;
			return true;
		}
		return false;
	}
	public string? GetName()
	{
		return _name;
	}
	public bool SetId(int id)
	{
		if (id > 0)
		{
			this._id = id;
			return true;
		}
		return false;
	}
	public int GetId()
	{
		return _id;
	}
}
