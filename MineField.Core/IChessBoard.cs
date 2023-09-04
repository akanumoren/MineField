using System;
using System.Diagnostics.CodeAnalysis;

namespace MineField.Core
{
    public interface IChessBoard
    {
        bool ReachedEndOfBoard { get; }
        int Lives { get; }
        string CurrentGrid { get; }
        void NewGame(int lives);
        string Move(MoveDirection direction);
        int GetScore();
    }
}