namespace MonopolyProjectInterface;

public interface IPlayerCard : ICard
{
    bool SetKeptCard(ICard card);
    bool UseCard(ICard card);
}