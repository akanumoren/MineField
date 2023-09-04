using System;
namespace MineField.Tests.UnitTests
{
	public class MineBuilderTests
	{
		public MineBuilderTests()
		{
		}

		[Theory]
		[InlineData(4)]
		[InlineData(20)]
		[InlineData(64)]
		public void Given_NumberOfMines_When_CreateMines_Then_MinesTotalShouldMatch(int totalMines)
		{
			// Arrange
			var mineBuider = new MineBuilder();

			// Act
			var mines = mineBuider.CreateMines(totalMines);

			// Assert
			Assert.Equal(totalMines, mines.Count());
		}

        [Theory]
        [InlineData(4)]
        [InlineData(20)]
        [InlineData(64)]
        public void When_MinesCreated_Expect_MinesAreUnique(int totalMines)
		{
            // Arrange
            var mineBuider = new MineBuilder();

            // Act
            var mines = mineBuider.CreateMines(totalMines);

            // Assert
            Assert.Equal(totalMines, mines.Distinct().Count());
        }
	}
}

