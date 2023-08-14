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
	private static List<CommunityCard> CreateCommunityCards()
	{
		return new List<CommunityCard>()
			{
				new CommunityCard(CommunityCardType.FineOrTakeChanceCard, "Pay a Fine: You violated traffic rules. Pay a $15 fine."),
				new CommunityCard(CommunityCardType.Reward, "Get a Reward: You received a performance bonus. Collect $50 from the bank."),
				new CommunityCard(CommunityCardType.Tax, "Pay Tax: Property taxes have increased. Pay $100 to the bank."),
				new CommunityCard(CommunityCardType.GoToJail, "Go to Jail: You have been arrested by the police! Go directly to Jail. Do not pass Start, do not collect $200."),
				new CommunityCard(CommunityCardType.FreeFromJail, "Free from Jail: Get out of Jail free! Keep this card for future use or sell it for a good price."),
				new CommunityCard(CommunityCardType.BackToLandmark, "Back to Landmark: Go to the Station. If unowned, you may buy it. If owned, pay rent."),
				new CommunityCard(CommunityCardType.HeadToStart, "Head to Start: Go back to Start. Collect $200 from the bank.")
			};
	}

	public static List<CommunityCard> communityCards = CreateCommunityCards();
	public string? OpenCard()
	{
		return _description;
	}
}