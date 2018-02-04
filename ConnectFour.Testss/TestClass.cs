using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour.Tests
{
    [TestFixture]
    public class TestClass
    {
        /* 
         * [ ] Draw board
         * [X] Get current player
         * [ ] Play row
         * [ ] Is Game over
         * [ ] Save game
         * [ ] Load game
         * [X] Has started
         * [ ] Reset game
         */

        [Test]
        public void IndicatesGameHasStartedAfterFirstPlay()
        {
            var game = new ConnectFour();

            Assert.IsFalse(game.HasStarted);
            game.Play();
            Assert.IsTrue(game.HasStarted);
        }

        [Test]
        public void IndicatesGameHasNotStartedAfterReset()
        {
            var game = new ConnectFour();

            game.Play();
            Assert.IsTrue(game.HasStarted);
            game.Reset();
            Assert.IsFalse(game.HasStarted);
        }

        [Test]
        public void CurrentPlayerRotatesAfterEachPlay()
        {
            var game = new ConnectFour();
            
            int[] expectedPlayer = new int[] { 1, 2, 1, 2 };
            for (int i = 0; i < expectedPlayer.Length; i++)
            {
                Assert.AreEqual(expectedPlayer[i], game.CurrentPlayer);
                game.Play();
            }
        }

        [Test]
        [ExpectedException]
        public void ColumnCanOnlyBePlayedSixTimes()
        {
            int playerRequestedColumn = 3;
            int boardRowLength = 6;
            int boardColumnLength = 7;
            BoardEntryState[,] board = new BoardEntryState[boardRowLength, boardColumnLength];

            for (int player1Incr = 0; player1Incr < boardRowLength + 1; player1Incr++) // player 1 exceeds plays on column
            {
                try
                {
                    // Determine what row player entry lands on
                    int playedRow = 0;
                    for (; playedRow < boardRowLength; playedRow++)
                    {
                        BoardEntryState columnState = (BoardEntryState)board.GetValue(playedRow, playerRequestedColumn);
                        if (columnState == BoardEntryState.Empty)
                        {
                            break;
                        }
                    }

                    board[playedRow, playerRequestedColumn] = BoardEntryState.Player1; // player 1 plays same column
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new InvalidPlayerMove("Board column is full.", ex);
                }
            }
        }
    }

    [Serializable]
    internal class InvalidPlayerMove : Exception
    {
        public InvalidPlayerMove()
        {
        }

        public InvalidPlayerMove(string message) : base(message)
        {
        }

        public InvalidPlayerMove(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPlayerMove(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    public enum BoardEntryState
    {
        Empty, Player1, Player2
    }

    public class ConnectFour
    {
        public bool HasStarted { get; private set; }
        public int CurrentPlayer { get; private set; } = 1;

        public void Play()
        {
            this.HasStarted = true;
            this.CurrentPlayer = this.CurrentPlayer == 1 ? 2 : 1;
        }

        public void Reset()
        {
            this.HasStarted = false;
        }
    }
}
