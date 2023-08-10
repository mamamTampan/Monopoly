using MonopolyProjectInterface;
using MonopolyProjectSource;

namespace MonopolyProject;

static class Program
{
    static void Main()
    {
        MonopolyGame game = new();

        IPlayer player1 = new HumanPlayer(111, "Kojiro");
        IPlayer player2 = new HumanPlayer(222, "Asep");
        game.AddPlayer(player1);
        game.AddPlayer(player2);
        game.SetInitialState();

        while (game.CheckGameStatus() == GameStatus.ONGOING)
        {
            IPlayer currentPlayer = game.GetCurrentTurn();

            Console.WriteLine($"Sekarang giliran {currentPlayer.GetName()} untuk bermain.");

            int diceValue = game.ThrowDice();
            Console.WriteLine($"Hasil lemparan dadu: {diceValue}");

            game.Move(currentPlayer, diceValue);

            game.SetNextTurn();
        }

        IPlayer winner = game.CheckWinner();
        Console.WriteLine($"Pemenang permainan: {winner.GetName()}");
    }
}