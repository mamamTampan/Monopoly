using MonopolyProjectInterface;

namespace MonopolyProjectSource
{
    public class ChanceCard : ICard
    {
        private ChanceCardType _type { get; set; }
        private string _description { get; set; }

        private ChanceCard(ChanceCardType type, string description)
        {
            this._type = type;
            this._description = description;
        }

        private static List<ChanceCard> CreateChanceCards()
        {
            return new List<ChanceCard>()
            {
                new ChanceCard(ChanceCardType.Fine, "Pay a Fine: You violated traffic rules. Pay a $15 fine."),
                new ChanceCard(ChanceCardType.Reward, "Get a Reward: You received a performance bonus. Collect $50 from the bank."),
                new ChanceCard(ChanceCardType.Tax, "Pay Tax: Property taxes have increased. Pay $100 to the bank."),
                new ChanceCard(ChanceCardType.GoToJail, "Go to Jail: You have been arrested by the police! Go directly to Jail. Do not pass Start, do not collect $200."),
                new ChanceCard(ChanceCardType.FreeFromJail, "Free from Jail: Get out of Jail free! Keep this card for future use or sell it for a good price."),
                new ChanceCard(ChanceCardType.HeadToLandmark, "Head to Landmark: Advance to the Indonesia. If unowned, you may buy it. If owned, pay rent."),
                new ChanceCard(ChanceCardType.BackToLandmark, "Back to Landmark: Go to the Station. If unowned, you may buy it. If owned, pay rent."),
                new ChanceCard(ChanceCardType.StepForward, "Step Forward: Advance 3 spaces. If you pass Go, collect $200."),
                new ChanceCard(ChanceCardType.StepBack, "Step Back: Move back 3 spaces. If you land on Go, collect $50."),
                new ChanceCard(ChanceCardType.HeadToStart, "Head to Start: Go back to Start. Collect $200 from the bank.")
            };
        }

        public static List<ChanceCard> chanceCards = CreateChanceCards();

        public string OpenCard()
        {
            return _description;
        }
    }
}
