using MonopolyLog;

namespace MonopolyProjectSource
{
	public class CardDeck
	{
		private Stack<ChanceCard> chanceDeck;
		private Stack<CommunityCard> communityDeck;
		public CardDeck()
		{
			List<ChanceCard> chanceCards = ChanceCard.chanceCards;
			chanceDeck = new Stack<ChanceCard>(chanceCards);
			List<CommunityCard> communityCards = CommunityCard.communityCards;
			communityDeck = new Stack<CommunityCard>(communityCards);
			ShuffleCard(chanceDeck);
			ShuffleCard(communityDeck);
		}

		public bool ShuffleCard<T>(Stack<T> deck)
		{
			Log.Instance.Info(" Shuffle Card ");
			
			if (deck == null || deck.Count <= 1)
			{
				return false;
			}
			Random shuffle = new Random();
			List<T> tempDeck = new List<T>(deck);
			deck.Clear();

			while (tempDeck.Count > 0)
			{
				int randomIndex = shuffle.Next(0, tempDeck.Count);
				deck.Push(tempDeck[randomIndex]);
				tempDeck.RemoveAt(randomIndex);
			}
			return true;
		}

		public ChanceCard DrawChanceCard()
		{
			if (chanceDeck.Count > 0)
			{
				return chanceDeck.Pop();
			}
			throw new InvalidOperationException("Chance card deck is empty.");
		}
		
		public CommunityCard DrawCommunityCard()
		{
			if (communityDeck.Count > 0)
			{
				return communityDeck.Pop();
			}
			throw new InvalidOperationException("Chance card deck is empty.");
		}
	}
}
