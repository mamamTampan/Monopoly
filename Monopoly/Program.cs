using MonopolyProjectInterface;
using MonopolyProjectSource;

namespace Monopoly
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("Welcome to Monopoly Game!");
			Console.WriteLine("--------------------------");

			MonopolyGame game = new();
			IPlayer player1 = new HumanPlayer(1, "Dadang");
			game.AddPlayer(player1);

		//	IPlayer player2 = new HumanPlayer(2, "Tatang");
		//	game.AddPlayer(player2);

		//	Console.WriteLine($"Get Ready {player1.GetName()} and {player2.GetName()}");
			game.SetInitialState();
			game.SetTurnsOrder();
			Console.WriteLine("Press any key to Continue");
			//Console.ReadLine();

			Dictionary<IPlayer, bool> hasRolledDictionary = new Dictionary<IPlayer, bool>();
		//	Console.Clear();
			while (game.CheckGameStatus() == GameStatus.ONGOING)
			{
				
				foreach (IPlayer currentPlayer in game.GetPlayers())
				{
					Console.WriteLine($" {currentPlayer.GetName()}'s turn.");
					Console.WriteLine("1. Roll the dice");
					Console.WriteLine("2. Check balance and properties");
					Console.WriteLine("3. End Turn and Continue");
					Console.WriteLine("4. Exit game");
					Console.Write("Choose an action: ");
					string? choice = Console.ReadLine();

					switch (choice)
					{
						case "1":
							if (!hasRolledDictionary.ContainsKey(currentPlayer) || !hasRolledDictionary[currentPlayer])
							{
								
								int diceValue1 = game.ThrowDices(0);
								int diceValue2 = game.ThrowDices(1);
								int diceRoll = diceValue1 + diceValue2;
								Console.WriteLine($"{currentPlayer.GetName()} Rolled {diceRoll}");
								Console.WriteLine($"Your dice values: {diceValue1} and {diceValue2}");

								int currentPosition = game.CheckPlayerPosition(currentPlayer);
								Console.WriteLine("Position before move: " + currentPosition);

								game.Move(currentPlayer, diceRoll);
								int newPosition = game.CheckPlayerPosition(currentPlayer);
								Console.WriteLine("Position after move: " + newPosition);

								hasRolledDictionary[currentPlayer] = true;
							}
							else
							{
								Console.Clear();
								Console.WriteLine("You have already rolled the dice this turn.");
							}
							break;

						case "2":
							Console.Clear();
							int balance = game.CheckPlayerBalance(currentPlayer);
							Console.WriteLine($"{currentPlayer.GetName()}'s balance: {balance}");
							// Add code to display player's properties here
							break;

						case "3":
							Console.Clear();
							if (hasRolledDictionary.ContainsKey(currentPlayer))
							{
								hasRolledDictionary[currentPlayer] = false;
							}
							else
							{
								hasRolledDictionary.Add(currentPlayer, false);
							}
							game.SetNextTurn();
							break;

						case "4":
							Console.Clear();
							Console.WriteLine("Exiting the game...");
							Environment.Exit(0);
							break;

						default:
							Console.WriteLine("Invalid choice. Please choose again.");
							break;
					}
				}
				
			}

			Console.WriteLine("Thanks for playing Monopoly!");
		}
	}
}