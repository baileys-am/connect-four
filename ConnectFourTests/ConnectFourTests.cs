using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ConnectFour;

namespace ConnectFourTests
{
    public class ConnectFourTests
    {
        /* 
         * [X] Draw board
         * [X] Get current player
         * [X] Play column
         * [X] Is Game over
         * [X] Has started
         * [X] Reset game
         */

        [Fact]
        public void IndicatesGameHasStartedAfterFirstPlay()
        {
            var game = new ConnectFourGame();

            Assert.False(game.HasStarted);
            game.Play(Column.Column1);
            Assert.True(game.HasStarted);
        }

        [Fact]
        public void IndicatesGameHasNotStartedAfterReset()
        {
            var game = new ConnectFourGame();

            game.Play(Column.Column1);
            Assert.True(game.HasStarted);
            game.Reset();
            Assert.False(game.HasStarted);
        }

        [Fact]
        public void FilledColumnCanBePlayedAfterReset()
        {
            Column playerRequestedColumn = Column.Column1;
            var game = new ConnectFourGame();

            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            Assert.Throws<InvalidMoveException>(() => game.Play(playerRequestedColumn));

            game.Reset();

            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            Assert.Throws<InvalidMoveException>(() => game.Play(playerRequestedColumn));
        }

        [Fact]
        public void CurrentPlayerRotatesAfterEachPlay()
        {
            var game = new ConnectFourGame();

            Player[] expectedPlayer = new Player[] { Player.Player1, Player.Player2, Player.Player1, Player.Player2 };
            for (int i = 0; i < expectedPlayer.Length; i++)
            {
                Assert.Equal(expectedPlayer[i], game.CurrentPlayer);
                game.Play(Column.Column1);
            }
        }

        [Fact]
        public void ColumnCanOnlyBePlayedSixTimes()
        {
            Column playerRequestedColumn = Column.Column1;
            var game = new ConnectFourGame();

            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            Assert.Throws<InvalidMoveException>(() => game.Play(playerRequestedColumn));
        }

        [Fact]
        public void PlayerWinsIfFourInARowOnColumn()
        {
            Column player1Column = Column.Column1;
            Column player2Column = Column.Column7;
            var game = new ConnectFourGame();

            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column + 1); // Player 1 makes mistake
            game.Play(player2Column);     // Player 2 wins

            bool gameOver = game.IsGameOver(out Player winner);

            Assert.True(gameOver);
            Assert.Equal(Player.Player2, winner);
        }

        [Fact]
        public void PlayerWinsIfFourInARowOnRow()
        {
            var game = new ConnectFourGame();

            game.Play(Column.Column1);
            game.Play(Column.Column1);
            game.Play(Column.Column2);
            game.Play(Column.Column2);
            game.Play(Column.Column3);
            game.Play(Column.Column3);
            game.Play(Column.Column4);  // Player 1 wins

            bool gameOver = game.IsGameOver(out Player winner);

            Assert.True(gameOver);
            Assert.Equal(Player.Player1, winner);
        }

        [Fact]
        public void GameIsNotOverWhenColumnHasTwoP1EntriesThenTwoP2Entries()
        {
            var game = new ConnectFourGame();

            game.Play(Column.Column1);
            game.Play(Column.Column2);
            game.Play(Column.Column1);
            game.Play(Column.Column2);
            game.Play(Column.Column2);
            game.Play(Column.Column1);
            game.Play(Column.Column2);
            game.Play(Column.Column1);

            Assert.False(game.IsGameOver(out Player winner));
        }

        [Fact]
        public void PlayerWinsIfFourDiagonal()
        {
            var game = new ConnectFourGame();

            game.Play(Column.Column1);

            game.Play(Column.Column2);
            game.Play(Column.Column2);

            game.Play(Column.Column3);
            game.Play(Column.Column3);
            game.Play(Column.Column4);
            game.Play(Column.Column3);

            game.Play(Column.Column4);
            game.Play(Column.Column4);
            game.Play(Column.Column6);
            game.Play(Column.Column4);

            bool gameOver = game.IsGameOver(out Player winner);

            Assert.True(gameOver);
            Assert.Equal(Player.Player1, winner);
        }

        [Fact]
        public void PlayerCannotPlayAfterGameIsOver()
        {
            Column player1Column = Column.Column1;
            Column player2Column = Column.Column7;
            var game = new ConnectFourGame();

            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column + 1); // Player 1 makes mistake
            game.Play(player2Column);     // Player 2 wins

            Assert.Throws<GameOverException>(() => game.Play(player1Column));
        }

        [Fact]
        public void BoardDrawingShowsP1AndP2Plays()
        {
            string expectedBoard = String.Join(Environment.NewLine,
                                               "[   ][   ][   ][   ][   ][   ][   ]",
                                               "[   ][   ][   ][   ][   ][   ][   ]",
                                               "[   ][   ][   ][   ][   ][   ][   ]",
                                               "[   ][   ][   ][   ][   ][   ][   ]",
                                               "[   ][   ][   ][   ][   ][   ][   ]",
                                               "[(1)][   ][   ][   ][   ][(1)][(2)]"
                                              );

            var game = new ConnectFourGame();
            game.Play(Column.Column1);
            game.Play(Column.Column7);
            game.Play(Column.Column6);

            var board = game.DrawBoard();
            Assert.Equal(expectedBoard, board);
        }
    }
}
