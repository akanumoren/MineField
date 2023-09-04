namespace MineField.Core
{
    public interface IMineBuilder
    {
        IList<IGridPostition> CreateMines(int totalMines);
    }
}

