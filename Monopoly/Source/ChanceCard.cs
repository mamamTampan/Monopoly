using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public class ChanceCard : ICard
{
	private ChanceCardType _type;
	private string _description;

	public ChanceCard(ChanceCardType type)
	{
		this._type = type;
		this._description = GetDescriptionByType(type);
	}
	
	private string GetDescriptionByType(ChanceCardType type)
    {
        switch (type)
        {
            case ChanceCardType.Fine:
                return "Pay a Fine: You violated traffic rules. Pay a $15 fine.";
            case ChanceCardType.Reward:
                return "Get a Reward: You received a performance bonus. Collect $50 from the bank.";
            default:
                return "Card description not found.";
        }
    }

	public string? OpenCard()
	{
		return _description;
	}
}