using System;
using Moq;

namespace MineField.Tests.UnitTests
{
	public class GridPositionTests
	{
		public GridPositionTests()
		{
		}

        [Theory]
        [InlineData(1, 1,"A1")]
        [InlineData(8, 1, "H1")]
        public void Create_InRange_GridPosition_ShouldHave_GridName(int x, int y, string gridName)
		{
            // Arrange
			var gridPosition = new GridPostition(x, y);

            // Act
            var gridPositionName = gridPosition.GridName;

            // Assert
			Assert.NotNull(gridPosition);
			Assert.Equal(gridName, gridPositionName);
		}

        [Theory]
        [InlineData(0, 0)]
        [InlineData(9, 9)]
        public void Create_OutOfRange_GridPodition_ShouldThrowError(int x, int y)
        {
            // Arrange
            void action()
            {
                _ = new GridPostition(x, y);
            }

            // Assert
            Assert.Throws<ArgumentException>(action);
        }

        [Theory]
        [InlineData(1, 1)]
        public void Given_SimilarGridPoints_When_Compared_Should_ReturnTrue(int x, int y)
        {
            // Arrange
            var gridPosition = new GridPostition(x, y);
            var gridPositionCompare = new GridPostition(x, y);

            var isSame = gridPosition.Equals(gridPositionCompare);

            // Assert
            Assert.True(isSame);
        }

        [Theory]
        [InlineData(1, 1, 2, 3)]
        public void Given_DifferentGridPoints_When_Compared_Should_ReturnFalse(int x, int y, int xCompare, int yCompare)
        {
            // Arrange
            var gridPosition = new GridPostition(x, y);
            var gridPositionCompare = new GridPostition(xCompare, yCompare);

            var isSame = gridPosition.Equals(gridPositionCompare);

            // Assert
            Assert.False(isSame);
        }

        [Theory]
        [InlineData(2, 3)]
        public void Given_ListOfGridPointsWithDuplicates_When_DistinctIsCalled_Then_RemoveDuplicates(int x, int y)
        {
            // Arrange
            var gridPositions = new List<IGridPostition> {
                new GridPostition(x,y),
                new GridPostition(x,y),
                new GridPostition(x,y),
                new GridPostition(x,y)
            };

            // Act
            var distinctPositions = gridPositions.Distinct().Count();

            // Assert
            Assert.Equal(1, distinctPositions);
        }
    }
}

