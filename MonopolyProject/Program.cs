using System;
using MonopolyProjectInterface;
using MonopolyProjectSource; // Sesuaikan dengan namespace Anda

namespace MonopolyConsoleApp
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Welcome to Monopoly Game!");
			Console.WriteLine("--------------------------");

			MonopolyGame game = new MonopolyGame();
			
			Console.Write("Enter Player 1 Name: ");
			string? player1Name = Console.ReadLine();
			IPlayer player1 = new HumanPlayer(1, player1Name);
			game.AddPlayer(player1);

			Console.Write("Enter Player 2 Name: ");
			string? player2Name = Console.ReadLine();
			IPlayer player2 = new HumanPlayer(2, player2Name);
			game.AddPlayer(player2);

			game.SetInitialState();
			
			bool quitGame = false;

			while (!quitGame)
			{
				IPlayer currentPlayer = game.GetCurrentTurn();
				Console.WriteLine($"It's {currentPlayer.GetName()}'s turn.");
				Console.WriteLine("1. Roll the dice");
				Console.WriteLine("2. Check balance");
				Console.WriteLine("3. Quit game");
				Console.Write("Choose an action: ");
				string? choice = Console.ReadLine();

				switch (choice)
				{
					case "1":
						int diceRoll = game.ThrowDice();
						Console.WriteLine($"{currentPlayer.GetName()} rolled {diceRoll}");
						game.Move(currentPlayer, diceRoll);
						game.SetNextTurn();
						break;
					
					case "2":
						int balance = game.CheckPlayerBalance(currentPlayer);
						Console.WriteLine($"{currentPlayer.GetName()}'s balance: {balance}");
						break;

					case "3":
						quitGame = true;
						break;

					default:
						Console.WriteLine("Invalid choice. Please choose again.");
						break;
				}

				Console.WriteLine("--------------------------");
			}

			Console.WriteLine("Thanks for playing Monopoly!");
		}
	}
}
