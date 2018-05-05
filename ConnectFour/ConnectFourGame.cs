using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public enum Column
    {
        Column1 = 1,
        Column2 = 2,
        Column3 = 3,
        Column4 = 4,
        Column5 = 5,
        Column6 = 6,
        Column7 = 7
    }

    public enum Entry
    {
        Empty,
        Player1,
        Player2
    }

    public enum Player
    {
        Player1,
        Player2
    }

    public class ConnectFourGame
    {
        private readonly Board _board = new Board();

        public bool HasStarted { get; private set; }
        public Player CurrentPlayer { get; private set; } = Player.Player1;

        public void Play(Column requestedColumn)
        {
            this.HasStarted = true;

            if (this.IsGameOver(out Player winner))
            {
                throw new GameOverException($"{winner} has already won!");
            }
            
            // Drop entry in column
            switch (this.CurrentPlayer)
            {
                case Player.Player1:
                    _board.Drop(requestedColumn, Entry.Player1);
                    break;
                case Player.Player2:
                    _board.Drop(requestedColumn, Entry.Player2);
                    break;
            }

            // Set current player to next one in turn
            this.CurrentPlayer = this.CurrentPlayer == Player.Player1 ? Player.Player2 : Player.Player1;
        }

        public string DrawBoard()
        {
            return this._board.Draw();
        }

        public bool IsGameOver(out Player winner)
        {
            // Initialize winner
            winner = Player.Player1;

            // Determine if an entry has four in a row
            bool hasFour = _board.HasFourInARow(out Entry entry);

            if (!hasFour)
            {
                return false;
            }

            switch (entry)
            {
                case Entry.Player1:
                    winner = Player.Player1;
                    return true;
                case Entry.Player2:
                    winner = Player.Player2;
                    return true;
                default:
                    return false;
            }
        }

        public void Reset()
        {
            // Empty board
            _board.Reset();

            // Reset started flag
            this.HasStarted = false;
        }
    }
}
