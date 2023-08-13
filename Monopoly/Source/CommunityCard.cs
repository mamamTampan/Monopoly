using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public class CommunityCard : ICard
{
	private CommunityCardType _type;
	private string _description;

	public CommunityCard(CommunityCardType type, string description)
	{
		this._type = type;
		this._description = description;
	}
	public string? OpenCard()
	{
		return _description;
	}
}