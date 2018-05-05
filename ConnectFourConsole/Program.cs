using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFour;

namespace ConnectFour
{
    class Program
    {
        private static readonly ConnectFourGame _game = new ConnectFourGame();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Connect Four!");

            while (true)
            {
                if (_game.IsGameOver(out Player winner))
                {
                    switch (winner)
                    {
                        case Player.Player1:
                            Console.WriteLine("Player 1 has won!");
                            break;
                        case Player.Player2:
                            Console.WriteLine("Player 2 has won!");
                            break;
                    }
                    string winBoard = _game.DrawBoard();
                    Console.WriteLine(winBoard);
                    Console.WriteLine();
                    _game.Reset();
                    Console.WriteLine("The game has been reset!");
                }

                Console.WriteLine();
                string board = _game.DrawBoard();
                Console.WriteLine(board);
                switch (_game.CurrentPlayer)
                {
                    case Player.Player1:
                        Console.Write("Player 1 choose your column (1-7): ");
                        Column p1Column = TryParseColumn();
                        _game.Play(p1Column);
                        break;
                    case Player.Player2:
                        Console.Write("Player 2 choose your column (1-7): ");
                        Column p2Column = TryParseColumn();
                        _game.Play(p2Column);
                        break;
                }
            }
        }

        static Column TryParseColumn()
        {
            Column col = Column.Column1;
            string column = Console.ReadLine();
            while (!Enum.TryParse(column, out col))
            {
                Console.WriteLine("Enter 1 thru 7 to choose a column: ");
                column = Console.ReadLine();
            }

            return col;
        }
    }
}
