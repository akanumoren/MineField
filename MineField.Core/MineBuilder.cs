namespace MineField.Core
{
    public class MineBuilder : IMineBuilder
    {
        /// <summary>
        /// Create mines
        /// </summary>
        /// <returns>A list of grids with mines</returns>
        public IList<IGridPostition> CreateMines(int totalMines)
        {
            // Create a Random object to generate random numbers.
            var random = new Random();
            int boardDimension = 8;


            // Create a list to store unique random grid positions.
            var mines = new List<IGridPostition>();
            var uniquePositions = new HashSet<Tuple<IGridPostition>>();

            while (uniquePositions.Count < totalMines)
            {
                int xPosition = random.Next(1, boardDimension + 1); 
                int yPosition = random.Next(1, boardDimension + 1);
                var position = Tuple.Create(GridPostition.Create(xPosition, yPosition)); 

                // Ensure the position is unique before adding it to the list.
                if (uniquePositions.Add(position))
                {
                    mines.Add(position.Item1);
                }
            }

            return mines;
        }
    }
}

