using MonopolyProjectInterface;
using MonopolyProjectSource;
using MonopolyLog;

namespace MonopolyApp
{
	public class Program
	{
		static void HandleGameInitialization()
		{
			Console.WriteLine("\n   Game initialization completed");
			Log.Instance.Info(" Delegate ");
		}
		static async Task Main()
		{
			MonopolyGame game = new();
			
			Console.Clear();
			Console.WriteLine("------------------------------------");
			Console.WriteLine("-+++--Welcome to Monopoly Game--+++-");
			Console.WriteLine("------------------------------------\n");
			await Task.Delay(500); Console.Write("  ===========");
			await Task.Delay(500); Console.Write("==========");
			await Task.Delay(500); Console.Write("==========\n");
			Log.Instance.Info(" UI Started ");
			
			IPlayer player1 = new HumanPlayer(1, "Babang");
			game.AddPlayer(player1);			
			IPlayer player2 = new HumanPlayer(2, "Kakang");
			game.AddPlayer(player2);
			IPlayer player3 = new HumanPlayer(3, "Mas");
			game.AddPlayer(player3);
			Log.Instance.Info(" Player Created ");			

			game.GameInitialized += HandleGameInitialization;
			game.SetInitialState();
			
			Console.WriteLine("    Press any key to Continue");
			Console.ReadKey();

			Dictionary<IPlayer, bool> hasRolledDictionary = new();
			Console.Clear();
			
			Log.Instance.Info(" Choose Menu for every Player ");
			
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
											Console.WriteLine($"{currentPlayer.GetName()} Rolled {diceRoll}     T: {twinDice}");
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
											var currentPosition = game.CheckPlayerPosition(currentPlayer);
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
											Console.WriteLine($"{currentPlayer.GetName()} Rolled {diceRoll}     T: {twinDice}");
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
								Log.Instance.Info(" ThrowDice Menu ");
								break;

							case "2":
								Console.Clear();
								int balance = game.CheckPlayerBalance(currentPlayer);
								Console.WriteLine($"{currentPlayer.GetName()}'s balance: {balance}\n");
								Log.Instance.Info(" Check All Item Menu ");
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
								Log.Instance.Info(" Choose to End Turn ");
								break;

							case "0":
								Console.Clear();
								Console.WriteLine("......Exiting the game......");
								Console.WriteLine("Thanks for playing Monopoly!\n");
								Environment.Exit(0);
								Log.Instance.Info(" Exit Game ");
								break;

							default:
								Console.Clear();
								Console.WriteLine("Invalid choice. Please choose again.\n");
								Log.Instance.Info(" Wrong Input ");
								break;
						}
					}
				}
				game.SetNextTurn();
				Log.Instance.Info(" ChangeTurn ");
			}
		}
	}
}