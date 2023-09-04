namespace MineField.Core
{
    /// <summary>
    /// Grid position
    /// </summary>
    public class GridPostition : IGridPostition, IEquatable<GridPostition>
    {
        public int YPosition { get; private set; }
        public int XPosition { get; private set; }

        /// <summary>
        /// Grid position instance
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <exception cref="ArgumentException"></exception>
        public GridPostition(int x, int y)
        {
            if (x is  < 1 or > 8 || y is < 1 or > 8)
            {
                throw new ArgumentException("Grid position must be within the dimension of the board");
            }

            YPosition = y;
            XPosition = x;
        }

        public static IGridPostition Create(int x, int y)
        {
            return new GridPostition(x, y);
        }

        /// <summary>
        /// Gets grid name e.g A1,B1 A5
        /// </summary>
        public string GridName
        {
            get
            {
                var gridName = $"{(char)(XPosition + 64)}{YPosition}";
                return gridName;
            }
        }

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as GridPostition);
        }

        public bool Equals(GridPostition? other)
        {
            if (other == null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return XPosition == other.XPosition && YPosition == other.YPosition;
        }

        public override int GetHashCode()
        {
            return (XPosition, YPosition).GetHashCode();
        }
    }
}

