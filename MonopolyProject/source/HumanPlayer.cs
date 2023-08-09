using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public class HumanPlayer : IPlayer
{
	private int _id;
	private string? _name;
	public HumanPlayer(int id,string name)
	{
		SetId(id);
		SetName(name);
	}
	public bool SetName(string name)
	{
		if (name.Length >= 2 )
		{
			_name = name;
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
			_id = id;
			return true;
		}
        return false;
	}
	public int GetId()
	{
		return _id;
	}
}
