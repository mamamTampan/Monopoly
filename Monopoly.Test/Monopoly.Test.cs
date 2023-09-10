using Xunit;
using MonopolyProjectSource;
using MonopolyProjectInterface;
using FluentAssertions;

namespace Monopoly.Test
{
	public class MonopolyGameTests
	{
		[Fact]
		public void CheckGameStatus_NotStarted_ReturnsNotStarted()
		{
			// Arrange
			var game = new MonopolyGame();

			// Act
			var status = game.CheckGameStatus();

			// Assert
			Assert.Equal(GameStatus.NOT_STARTED, status);
		}

		[Fact]
		public void AddPlayer_ValidPlayer_ReturnsTrue()
		{
			// Arrange
			var game = new MonopolyGame();
			var player = new HumanPlayer(1, "Player 1");

			// Act
			var result = game.AddPlayer(player);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void GetPlayers_ReturnsListOfPlayers()
		{
			// Arrange
			var game = new MonopolyGame();
			var player1 = new HumanPlayer(1, "Player 1");
			var player2 = new HumanPlayer(2, "Player 2");

			game.AddPlayer(player1);
			game.AddPlayer(player2);

			// Act
			var players = game.GetPlayers();

			// Assert
			Assert.Equal(2, players.Count);
			Assert.Contains(player1, players);
			Assert.Contains(player2, players);
		}
		
		
	}
}
