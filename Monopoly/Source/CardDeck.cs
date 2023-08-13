namespace MonopolyProjectSource;
public class CardDeck
{
	private Stack<ChanceCard> chanceDeck;

	public CardDeck(List<ChanceCardType> chanceCardTypes)
	{
		chanceDeck = new Stack<ChanceCard>(chanceCardTypes.Select(type => new ChanceCard(type)));
		ShuffleCard(chanceDeck);
	}

	public bool ShuffleCard<T>(Stack<T> deck)
	{
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
}
