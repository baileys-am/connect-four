using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    internal class Board
    {
        private const int _winCount = 4;
        private const int _rowLength = 6;
        private const int _columnLength = 7;
        private Entry[,] _board = new Entry[_rowLength, _columnLength];

        internal void Drop(Column column, Entry entry)
        {
            // Convert column to indice
            int columnIndice = this.AsIndice(column);

            // Invalid move if column is full
            if (!_board.GetColumn(columnIndice).Contains(Entry.Empty))
            {
                throw new InvalidMoveException();
            }

            // Determine what row player entry lands on
            for (int r = 0; r < _board.Length; r++)
            {
                if (_board.GetColumn(columnIndice)[r] == Entry.Empty)
                {
                    _board[r, columnIndice] = entry;
                    break;
                }
            }
        }

        internal string Draw()
        {
            List<string> entries = new List<string>();
            for (int r = _rowLength - 1; r >= 0; r--)
            {
                for (int c = 0; c < _columnLength; c++)
                {
                    switch (_board[r, c])
                    {
                        case Entry.Empty:
                            entries.Add("[   ]");
                            break;
                        case Entry.Player1:
                            entries.Add("[(1)]");
                            break;
                        case Entry.Player2:
                            entries.Add("[(2)]");
                            break;
                    }
                }

                if (r > 0)
                {
                    entries.Add(Environment.NewLine);
                }
            }

            return entries.Aggregate((i, j) => i + j);
        }

        internal bool HasFourInARow(out Entry entry)
        {
            // Initialize entry
            entry = Entry.Empty;

            // Scan board for four in a row
            for (int r = 0; r <= _rowLength - _winCount; r++)
            {
                for (int c = 0; c <= _columnLength - _winCount; c++)
                {
                    Entry[,] subset = _board.GetSubset(r, c, _winCount, _winCount);
                    bool hasFour = this.HasFourInARow(subset, out entry);
                    if (hasFour && entry != Entry.Empty)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal void Reset()
        {
            _board.SetAllElements(Entry.Empty);
        }

        private bool HasFourInARow(Entry[,] entries, out Entry entry)
        {
            // Ensure entries is 4x4
            if (entries.GetLength(0) != _winCount || entries.GetLength(1) != _winCount)
            {
                throw new InvalidOperationException();
            }

            // Initialize entry
            entry = Entry.Empty;

            // Check rows
            for (int r = 0; r < entries.GetLength(0); r++)
            {
                bool hasFour = this.HasFourInARow(entries.GetRow(r), out entry);
                if (hasFour)
                {
                    return true;
                }
            }

            // Check columns
            for (int c = 0; c < entries.GetLength(1); c++)
            {
                bool hasFour = this.HasFourInARow(entries.GetColumn(c), out entry);
                if (hasFour)
                {
                    return true;
                }
            }

            // Check diagonals
            bool primaryHasFull = this.HasFourInARow(entries.GetPrimaryDiagonal(), out entry);
            if (primaryHasFull)
            {
                return true;
            }
            bool secondaryHasFull = this.HasFourInARow(entries.GetSecondaryDiagonal(), out entry);
            return secondaryHasFull;
        }

        private bool HasFourInARow(Entry[] entries, out Entry entry)
        {
            // Ensure entries has 4 elements
            if (entries.Length != _winCount)
            {
                throw new InvalidOperationException();
            }

            entry = Entry.Empty;

            if (entries.Distinct().Count() == 1)
            {
                entry = entries.First();
                return entry != Entry.Empty;
            }

            return false;
        }

        private int AsIndice(Column column)
        {
            switch (column)
            {
                case Column.Column1:
                    return 0;
                case Column.Column2:
                    return 1;
                case Column.Column3:
                    return 2;
                case Column.Column4:
                    return 3;
                case Column.Column5:
                    return 4;
                case Column.Column6:
                    return 5;
                case Column.Column7:
                    return 6;
                default:
                    throw new InvalidCastException();
            }
        }
    }
}
