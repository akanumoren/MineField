using System;
using Moq;

namespace MineField.Tests.UnitTests
{
	public class ChessBoardTests
	{
		public ChessBoardTests()
		{
		}

		[Fact]
		public void Start_New_Game_GridPosition_ShouldDefaultToA1()
		{
            // Arrange
            Mock<IMineBuilder> mineBuilderMock = new Mock<IMineBuilder>();
            var chessBoard = new ChessBoard(mineBuilderMock.Object);
            mineBuilderMock.Setup(x => x.CreateMines(It.IsAny<int>())).Returns(new List<IGridPostition>());

            // Act
            chessBoard.NewGame(5);

			// Assert
			Assert.Equal("A1", chessBoard.CurrentGrid);
		}

        [Fact]
        public void When_NewGameStart_Expect_DefaultValues()
        {
            // Arrange
            Mock<IMineBuilder> mineBuilderMock = new Mock<IMineBuilder>();
            var chessBoard = new ChessBoard(mineBuilderMock.Object);
            mineBuilderMock.Setup(x => x.CreateMines(It.IsAny<int>())).Returns(new List<IGridPostition>());

            // Act
            chessBoard.NewGame(5);

            // Assert
            Assert.False(chessBoard.ReachedEndOfBoard);
            Assert.Equal(5, chessBoard.Lives);
            Assert.Equal(0, chessBoard.GetScore());
            Assert.Equal("A1", chessBoard.CurrentGrid);
        }

        [Theory]
		[InlineData(MoveDirection.Up, 3, "A4", 3)]
        [InlineData(MoveDirection.Right, 5, "F1", 5)]
        [InlineData(MoveDirection.Right, 10, "H1", 7)]
        [InlineData(MoveDirection.Down, 2, "A1", 0)]
        [InlineData(MoveDirection.Left, 2, "A1", 0)]
        public void Start_New_Game_Move_Indirection_ShouldReturnGridName(MoveDirection direction, int movementCount, string currentGridName, int score)
        {
            // Arrange
            Mock<IMineBuilder> mineBuilderMock = new Mock<IMineBuilder>();
            var chessBoard = new ChessBoard(mineBuilderMock.Object);
            mineBuilderMock.Setup(x => x.CreateMines(It.IsAny<int>())).Returns(new List<IGridPostition>());
            chessBoard.NewGame(5);

			// Act
			for (int x = 1; x <= movementCount; x++)
			{
				chessBoard.Move(direction);
			}

            // Assert
            Assert.Equal(currentGridName, chessBoard.CurrentGrid);
            Assert.Equal(score, chessBoard.GetScore());
        }

        [Fact]
        public void When_UserMoveToMine_Expect_LiveLost_ScoreChanges_UserAtCurrentPosition()
        {
            // Arrange
            Mock<IMineBuilder> mineBuilderMock = new Mock<IMineBuilder>();
            var chessBoard = new ChessBoard(mineBuilderMock.Object);
            mineBuilderMock.Setup(x => x.CreateMines(It.IsAny<int>())).Returns(new List<IGridPostition>() { new GridPostition(2,1) });
            chessBoard.NewGame(5);

            // Act
            chessBoard.Move(MoveDirection.Right);

            // Assert
            Assert.Equal(4, chessBoard.Lives);
            Assert.Equal(1, chessBoard.GetScore());
            Assert.Equal("A1", chessBoard.CurrentGrid);
        }

        [Fact]
        public void When_UserMovesToEdge_Expect_GameEnds_ScoreChanges_UserAtH1()
        {
            // Arrange
            Mock<IMineBuilder> mineBuilderMock = new Mock<IMineBuilder>();
            var chessBoard = new ChessBoard(mineBuilderMock.Object);
            mineBuilderMock.Setup(x => x.CreateMines(It.IsAny<int>())).Returns(new List<IGridPostition>() { });
            chessBoard.NewGame(5);

            // Act
            for (int x = 1; x <= 7; x++)
            {
                chessBoard.Move(MoveDirection.Right);
            }

            // Assert
            Assert.True(chessBoard.ReachedEndOfBoard);
            Assert.Equal(7, chessBoard.GetScore());
            Assert.Equal("H1", chessBoard.CurrentGrid);
        }

        [Fact]
        public void When_UserMoveTo_Non_MineGrid_Expect_NoLiveLost_ScoreChanges_UserAtNewPosition()
        {
            // Arrange
            Mock<IMineBuilder> mineBuilderMock = new Mock<IMineBuilder>();
            var chessBoard = new ChessBoard(mineBuilderMock.Object);
            mineBuilderMock.Setup(x => x.CreateMines(It.IsAny<int>())).Returns(new List<IGridPostition>() { });
            chessBoard.NewGame(5);

            // Act
            chessBoard.Move(MoveDirection.Right);

            // Assert
            Assert.False(chessBoard.ReachedEndOfBoard);
            Assert.Equal(5, chessBoard.Lives);
            Assert.Equal(1, chessBoard.GetScore());
            Assert.Equal("B1", chessBoard.CurrentGrid);
        }

        [Theory]
        [InlineData(MoveDirection.Left, "A1", 0)]
        [InlineData(MoveDirection.Down,  "A1", 0)]
        [InlineData(MoveDirection.Right,  "H1", 7)]
        [InlineData(MoveDirection.Up,  "A8", 7)]
        public void When_UserAttemptMove_BeyoundBoardEdge_Expect_NoScoreChange_UserAtCurrentPosition(MoveDirection moveDirection, string currentPosition, int score)
        {
            // Arrange
            Mock<IMineBuilder> mineBuilderMock = new Mock<IMineBuilder>();
            var chessBoard = new ChessBoard(mineBuilderMock.Object);
            mineBuilderMock.Setup(x => x.CreateMines(It.IsAny<int>())).Returns(new List<IGridPostition>() { });
            chessBoard.NewGame(5);

            // Act
            for (int x = 1; x <= 9; x++)
            {
                chessBoard.Move(moveDirection);
            }

            // Assert
            Assert.Equal(5, chessBoard.Lives);
            Assert.Equal(score, chessBoard.GetScore());
            Assert.Equal(currentPosition, chessBoard.CurrentGrid);
        }

        [Fact]
        public void When_UserMove_Expect_Status()
        {
            // Arrange
            Mock<IMineBuilder> mineBuilderMock = new Mock<IMineBuilder>();
            var chessBoard = new ChessBoard(mineBuilderMock.Object);
            mineBuilderMock.Setup(x => x.CreateMines(It.IsAny<int>())).Returns(new List<IGridPostition>() { });
            chessBoard.NewGame(5);

            // Act
            var status = chessBoard.Move(MoveDirection.Right);

            // Assert
            Assert.Contains(string.Format(Resources.STATUS_MESSAGE, chessBoard.CurrentGrid, chessBoard.Lives, chessBoard.GetScore()),status);
        }
    }
}

