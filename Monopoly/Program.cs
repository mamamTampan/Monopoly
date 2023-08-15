using MonopolyProjectInterface;
using MonopolyProjectSource;

namespace Monopoly
{
	class Program
	{
		static void Main()
		{
			Console.Clear();
			Console.WriteLine("------------------------------------");
			Console.WriteLine("-+++--Welcome to Monopoly Game--+++-");
			Console.WriteLine("------------------------------------\n");

			MonopolyGame game = new();
			IPlayer player1 = new HumanPlayer(1, "Bang");
			game.AddPlayer(player1);

			IPlayer player2 = new HumanPlayer(2, "Kang");
			game.AddPlayer(player2);

			IPlayer player3 = new HumanPlayer(3, "Mas");
			game.AddPlayer(player3);

			game.SetInitialState();
			Console.WriteLine("    Press any key to Continue");
			Console.ReadKey();

			Dictionary<IPlayer, bool> hasRolledDictionary = new();
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
						Console.WriteLine("3. -----------");
						Console.WriteLine("4. End Turn");
						Console.WriteLine("0. Exit game");
						Console.Write("Choose an action: ");
						string? choice = Console.ReadLine();

						switch (choice)
						{
							case "1":
								if (!hasRolledDictionary.ContainsKey(currentPlayer) || !hasRolledDictionary[currentPlayer])
								{
									Console.Clear();
									game.ThrowDice();
									int diceValue1 = game.ThrowDices(0);
									int diceValue2 = game.ThrowDices(1);
									int twinDice = game.TwinDice();
									int diceRoll = diceValue1 + diceValue2;
									do
									{
										if (twinDice == 1 || twinDice == 2)
										{
											Console.Clear();
											Console.WriteLine(" +++++  Congrats, U can roll again  +++++");
											Console.WriteLine($"{currentPlayer.GetName()} Rolled {diceRoll}		t{twinDice}");
											Console.WriteLine($"Your dice values: {diceValue1} and {diceValue2}");

											int currentPosition = game.CheckPlayerPosition(currentPlayer);
											var from = game.TileName(currentPosition);
											Console.WriteLine($"Position before move: Tile {currentPosition}. {from}");

											game.Move(currentPlayer, diceRoll);
											int newPosition = game.CheckPlayerPosition(currentPlayer);
											var to = game.TileName(newPosition);
											Console.WriteLine($"Position after move: Tile {newPosition}. {to}\n");
											hasRolledDictionary[currentPlayer] = false;
											break;
										}
										else if (twinDice == 3)
										{
											Console.WriteLine(" ---- are U cheating ? ---- ");
											Console.WriteLine(" 	Go to Jail now !!! ");
											int currentPosition = game.CheckPlayerPosition(currentPlayer);
											var from = game.TileName(currentPosition);
											Console.WriteLine($"Position before move: Tile {currentPosition}. {from}");

											game.Move(currentPlayer, diceRoll);
											int newPosition = game.CheckPlayerPosition(currentPlayer);
											var to = game.TileName(newPosition);
											Console.WriteLine($"Position after move: Tile {newPosition}. {to}\n");
											hasRolledDictionary[currentPlayer] = true;
											break;
										}
										else
										{
											Console.WriteLine($"{currentPlayer.GetName()} Rolled {diceRoll}		t{twinDice}");
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
											break;
										}

									} while (true);
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
								int twinDice2 = game.TwinDice();
								if (twinDice2 == 1 || twinDice2 == 2)
								{
									Console.WriteLine("You must roll again.\n");
									hasRolledDictionary[currentPlayer] = false;
									turn = true;
								}
								break;

							case "0":
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
				}
				game.SetNextTurn();

			}
		}
	}
}