using System;
using MineField.Core;

namespace MineField
{
    public class GameConsole
    {
        private readonly IChessBoard chessBoard;

        public GameConsole(IChessBoard chessBoard)
        {
            this.chessBoard = chessBoard;
        }

        public void RunGame()
        {
            Console.WriteLine($"Welcome{Environment.NewLine}");

            chessBoard.NewGame(5);

            MoveDirection currentDirection;
            InputKeyType inputKeyType;

            do
            {
                if (chessBoard.ReachedEndOfBoard || chessBoard.Lives == 0)
                {
                    Console.WriteLine($"{Environment.NewLine}Press N to start a new game or Q to quit...");
                }
                else
                {
                    Console.WriteLine($"{Environment.NewLine}Move in a direction using the arrow keys, or Q to quit...");
                }

                var input = Console.ReadKey(true);

                inputKeyType = GetInputKeyType(input);

                if (inputKeyType == InputKeyType.Direction)
                {
                    currentDirection = GetDirection(input);

                    Console.Clear();

                    var status = chessBoard.Move(currentDirection);

                    Console.WriteLine(status);
                }
                else if(inputKeyType == InputKeyType.NewGame)
                {
                    Console.Clear();
                    chessBoard.NewGame(5);
                }


            } while (inputKeyType != InputKeyType.Quit);

            Console.Clear();
        }

        static InputKeyType GetInputKeyType(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key switch
            {
                ConsoleKey.DownArrow or ConsoleKey.LeftArrow or ConsoleKey.RightArrow or ConsoleKey.UpArrow => InputKeyType.Direction,
                ConsoleKey.Q => InputKeyType.Quit,
                ConsoleKey.N => InputKeyType.NewGame,
                _ => InputKeyType.Unknown,
            };
        }

        public MoveDirection GetDirection(ConsoleKeyInfo keyInfo)
        {
            return keyInfo.Key switch
            {
                ConsoleKey.UpArrow => MoveDirection.Up,
                ConsoleKey.RightArrow => MoveDirection.Right,
                ConsoleKey.DownArrow => MoveDirection.Down,
                ConsoleKey.LeftArrow => MoveDirection.Left,
                _ => MoveDirection.None,
            };
        }
    }
}

