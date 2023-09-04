using System;

namespace MineField.Core
{
    public class ChessBoard : IChessBoard
    {
        private IGridPostition currentPostion;
        private int totalMoves;
        private IList<IGridPostition> mines;
        private readonly IMineBuilder mineBuilder;

        /// <summary>
        /// ChessBoard
        /// </summary>
        /// <param name="mineBuilder"></param>
        public ChessBoard(IMineBuilder mineBuilder)
        {
            this.mineBuilder = mineBuilder;
        }

        /// <summary>
        /// Number of lives
        /// </summary>
        public int Lives { get; private set; }

        /// <summary>
        /// Current Grid Name
        /// </summary>
        public string CurrentGrid
        {
            get
            {
                return currentPostion?.GridName ?? "Please start a new game";
            }
        }

        /// <summary>
        /// End of the board reached
        /// </summary>
        public bool ReachedEndOfBoard
        {
            get
            {
                if (Lives == 0)
                {
                    return false;
                }

                return currentPostion.XPosition > 7;
            }
        }

        /// <summary>
        /// Start a new game 
        /// </summary>
        /// <param name="lives">Number of lives</param>
        public void NewGame(int lives)
        {
            Lives = lives;
            currentPostion = GridPostition.Create(1, 1);

            var random = new Random();
            int totalMines = random.Next(10, 15);
            mines = mineBuilder.CreateMines(totalMines);

            // reset moves
            totalMoves = 0;
        }

        /// <summary>
        /// Get user score
        /// </summary>
        /// <returns>User score from total moves</returns>
        public int GetScore()
        {
            return totalMoves;
        }

        /// <summary>
        /// Move to grid position
        /// </summary>
        /// <param name="direction">Direction to move</param>
        /// <returns>Game status</returns>
        public string Move(MoveDirection direction)
        {
            if (Lives <= 0)
            {
                return "Game over. Press N to start a new game.";
            }

            if (ReachedEndOfBoard)
            {
                return GameStatus();
            }

            return direction switch
            {
                MoveDirection.Up => MoveUp(),
                MoveDirection.Down => MoveDown(),
                MoveDirection.Right => MoveRight(),
                MoveDirection.Left => MoveLeft(),
                _ => Resources.INVALID_MOVEMENT_ERROR,
            };
        }

        /// <summary>
        /// Move right
        /// </summary>
        /// <returns>Game status</returns>
        private string MoveRight()
        {
            // can move right
            if (!(currentPostion.XPosition < 8))
            {
                return $"{Resources.INVALID_MOVEMENT_ERROR}\n{GameStatus()}";
            }

            IncrementMoves();

            var newPosition = new GridPostition(currentPostion.XPosition + 1, currentPostion.YPosition);

            return CheckAndUpdatePosition(newPosition, MoveDirection.Right);
        }

        /// <summary>
        /// Move Down
        /// </summary>
        /// <returns>Game status</returns>
        private string MoveDown()
        {
            // can move down
            if (!(currentPostion.YPosition > 1))
            {
                return $"{Resources.INVALID_MOVEMENT_ERROR}\n{GameStatus()}";
            }

            IncrementMoves();

            var newPosition = new GridPostition(currentPostion.XPosition, currentPostion.YPosition - 1);

            return CheckAndUpdatePosition(newPosition, MoveDirection.Down);
        }

        /// <summary>
        /// Move Left
        /// </summary>
        /// <returns>Game status</returns>
        private string MoveLeft()
        {
            // can move left
            if (!(currentPostion.XPosition > 1))
            {
                return $"{Resources.INVALID_MOVEMENT_ERROR}\n{GameStatus()}";
            }

            IncrementMoves();

            var newPosition = new GridPostition(currentPostion.XPosition - 1, currentPostion.YPosition);

            return CheckAndUpdatePosition(newPosition, MoveDirection.Left);
        }

        /// <summary>
        /// Move Up
        /// </summary>
        /// <returns>Game status</returns>
        private string MoveUp()
        {
            // can move up
            if (!(currentPostion.YPosition < 8))
            {
                return $"{Resources.INVALID_MOVEMENT_ERROR}\n{GameStatus()}";
            }

            IncrementMoves();

            var newPosition = new GridPostition(currentPostion.XPosition, currentPostion.YPosition + 1);

            return CheckAndUpdatePosition(newPosition, MoveDirection.Up);
        }

        /// <summary>
        /// Check if user has landed on mine and update grid position
        /// </summary>
        /// <param name="newPosition">New Grid position</param>
        /// <param name="moveDirection">Move direction</param>
        /// <returns>Game status</returns>
        private string CheckAndUpdatePosition(IGridPostition newPosition, MoveDirection moveDirection)
        {
            if (HasLandedOnMine(newPosition))
            {
                return string.Format($"{Resources.DIRECTION_MESSAGE}\n{LostLive()}", moveDirection);
            }

            currentPostion = newPosition;

            return string.Format($"{Resources.DIRECTION_MESSAGE}\n{GameStatus()}", moveDirection);
        }

        /// <summary>
        /// Increment number of moves
        /// </summary>
        private void IncrementMoves()
        {
            totalMoves += 1;
        }

        /// <summary>
        /// Reduce available lives
        /// </summary>
        /// <returns></returns>
        private string LostLive()
        {
            Lives -= 1;

            return GameStatus(true);
        }

        /// <summary>
        /// Game status
        /// </summary>
        /// <param name="mineHit">Flag to indicate if a mine has been hit</param>
        /// <returns>Game status</returns>
        private string GameStatus(bool mineHit = false)
        {
            if (Lives == 0)
            {
                // Game over
                return string.Format($"{Resources.GAME_OVER_MESSAGE}\n{Resources.STATUS_MESSAGE}", CurrentGrid, Lives, GetScore());
            }

            var statusMessage = Resources.STATUS_MESSAGE;

            if (mineHit)
            {
                statusMessage = $"{Resources.MINE_HIT_MESSAGE}\n{statusMessage}";
            }

            if(ReachedEndOfBoard)
            {
                statusMessage = $"{Resources.GAME_COMPLETED_MESSAGE}\n{statusMessage}";
            }

            return string.Format(statusMessage, CurrentGrid, Lives, GetScore());
        }

        /// <summary>
        /// Check if user has landed on a mine
        /// </summary>
        /// <param name="gridPostition">Current grid position</param>
        /// <returns>Flag to indicate if mine has been hit </returns>
        private bool HasLandedOnMine(IGridPostition gridPostition)
        {
            return mines.Contains(gridPostition);
        }
    }
}