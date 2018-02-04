using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConnectFour.Tests
{
    [TestClass]
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

        [TestMethod]
        [TestCategory("Game state")]
        public void IndicatesGameHasStartedAfterFirstPlay()
        {
            var game = new ConnectFour();

            Assert.IsFalse(game.HasStarted);
            game.Play(Column.Column1);
            Assert.IsTrue(game.HasStarted);
        }

        [TestMethod]
        [TestCategory("Game state")]
        public void IndicatesGameHasNotStartedAfterReset()
        {
            var game = new ConnectFour();

            game.Play(Column.Column1);
            Assert.IsTrue(game.HasStarted);
            game.Reset();
            Assert.IsFalse(game.HasStarted);
        }

        [TestMethod]
        [TestCategory("Game state")]
        public void FilledColumnCanBePlayedAfterReset()
        {
            Column playerRequestedColumn = Column.Column1;
            var game = new ConnectFour();

            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            Assert.ThrowsException<InvalidMoveException>(() => game.Play(playerRequestedColumn));

            game.Reset();

            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            Assert.ThrowsException<InvalidMoveException>(() => game.Play(playerRequestedColumn));
        }

        [TestMethod]
        [TestCategory("Play column")]
        public void CurrentPlayerRotatesAfterEachPlay()
        {
            var game = new ConnectFour();

            Player[] expectedPlayer = new Player[] { Player.Player1, Player.Player2, Player.Player1, Player.Player2 };
            for (int i = 0; i < expectedPlayer.Length; i++)
            {
                Assert.AreEqual(expectedPlayer[i], game.CurrentPlayer);
                game.Play(Column.Column1);
            }
        }

        [TestMethod]
        [TestCategory("Play column")]
        public void ColumnCanOnlyBePlayedSixTimes()
        {
            Column playerRequestedColumn = Column.Column1;
            var game = new ConnectFour();

            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            game.Play(playerRequestedColumn);
            Assert.ThrowsException<InvalidMoveException>(() => game.Play(playerRequestedColumn));
        }

        [TestMethod]
        [TestCategory("Play column")]
        public void PlayerWinsIfFourInARowOnColumn()
        {
            Column player1Column = Column.Column1;
            Column player2Column = Column.Column7;
            var game = new ConnectFour();

            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column + 1); // Player 1 makes mistake
            game.Play(player2Column);     // Player 2 wins

            bool gameOver = game.IsGameOver(out Player winner);

            Assert.IsTrue(gameOver);
            Assert.AreEqual(Player.Player2, winner);
        }

        [TestMethod]
        [TestCategory("Play column")]
        public void PlayerWinsIfFourInARowOnRow()
        {
            var game = new ConnectFour();

            game.Play(Column.Column1);
            game.Play(Column.Column1);
            game.Play(Column.Column2);
            game.Play(Column.Column2);
            game.Play(Column.Column3);
            game.Play(Column.Column3);
            game.Play(Column.Column4);  // Player 1 wins

            bool gameOver = game.IsGameOver(out Player winner);

            Assert.IsTrue(gameOver);
            Assert.AreEqual(Player.Player1, winner);
        }

        [TestMethod]
        [TestCategory("Game state")]
        public void GameIsNotOverWhenColumnHasTwoP1EntriesThenTwoP2Entries()
        {
            var game = new ConnectFour();

            game.Play(Column.Column1);
            game.Play(Column.Column2);
            game.Play(Column.Column1);
            game.Play(Column.Column2);
            game.Play(Column.Column2);
            game.Play(Column.Column1);
            game.Play(Column.Column2);
            game.Play(Column.Column1);

            Assert.IsFalse(game.IsGameOver(out Player winner));
        }

        [TestMethod]
        [TestCategory("Play column")]
        public void PlayerWinsIfFourDiagonal()
        {
            var game = new ConnectFour();

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

            Assert.IsTrue(gameOver);
            Assert.AreEqual(Player.Player1, winner);
        }

        [TestMethod]
        [TestCategory("Game state")]
        public void PlayerCannotPlayAfterGameIsOver()
        {
            Column player1Column = Column.Column1;
            Column player2Column = Column.Column7;
            var game = new ConnectFour();

            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column);
            game.Play(player2Column);
            game.Play(player1Column + 1); // Player 1 makes mistake
            game.Play(player2Column);     // Player 2 wins

            Assert.ThrowsException<GameOverException>(() => game.Play(player1Column));
        }

        [TestMethod]
        [TestCategory("Game state")]
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

            var game = new ConnectFour();
            game.Play(Column.Column1);
            game.Play(Column.Column7);
            game.Play(Column.Column6);

            var board = game.DrawBoard();
            Assert.AreEqual(expectedBoard, board);
        }
    }
}
