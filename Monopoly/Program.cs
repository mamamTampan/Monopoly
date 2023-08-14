using MonopolyProjectInterface;
using MonopolyProjectSource;

namespace Monopoly
{
	class Program
	{
		static void Main()
		{
			//Console.Clear();
			Console.WriteLine("------------------------------------");
			Console.WriteLine("-+++--Welcome to Monopoly Game--+++-");
			Console.WriteLine("------------------------------------\n");

			MonopolyGame game = new();
			IPlayer player1 = new HumanPlayer(1, "Dadang");
			game.AddPlayer(player1);

			IPlayer player2 = new HumanPlayer(2, "Tatang");
			game.AddPlayer(player2);

			IPlayer player3 = new HumanPlayer(3, "Babang");
			game.AddPlayer(player3);
			
			game.SetInitialState();
			Console.WriteLine("    Press any key to Continue");
			Console.ReadKey();
			
			Dictionary<IPlayer, bool> hasRolledDictionary = new Dictionary<IPlayer, bool>();
			Console.Clear();

			while (game.CheckGameStatus() == GameStatus.ONGOING)
			{
				
				foreach (IPlayer currentPlayer in game.GetPlayers())
				{
					
					var turn = true;
					while (turn)
					{
						Console.WriteLine($" === {currentPlayer.GetName()}'s turn ===");
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
									Console.Clear();
									int diceValue1 = game.ThrowDices(0);
									int diceValue2 = game.ThrowDices(1);
									int diceRoll = diceValue1 + diceValue2;
									Console.WriteLine($"{currentPlayer.GetName()} Rolled {diceRoll}");
									Console.WriteLine($"Your dice values: {diceValue1} and {diceValue2}");

									int currentPosition = game.CheckPlayerPosition(currentPlayer);
									var from = game.TileName(currentPosition);
									Console.WriteLine($"Position before move: Tile {currentPosition}. {from}");

									game.Move(currentPlayer, diceRoll);
									int newPosition = game.CheckPlayerPosition(currentPlayer);
									var to = game.TileName(newPosition);
									Console.WriteLine($"Position after move: Tile {newPosition}. {to}\n");

									game.GrantRegular(currentPlayer);
									hasRolledDictionary[currentPlayer] = true;
								}
								else
								{
									Console.Clear();
									Console.WriteLine("You have already rolled the dice this turn.\n");
								}
								break;

							case "2":
								Console.Clear();
								int balance = game.CheckPlayerBalance(currentPlayer);
								Console.WriteLine($"{currentPlayer.GetName()}'s balance: {balance}\n");
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
								turn = false;
								break;

							case "4":
								Console.Clear();
								Console.WriteLine("......Exiting the game......");
								Console.WriteLine("Thanks for playing Monopoly!\n");
								Environment.Exit(0);
								break;

							default:
								Console.Clear();
								Console.WriteLine("Invalid choice. Please choose again.\n");
								break;
						}
					}
				} game.SetNextTurn();
				
			}
		}
	}
}