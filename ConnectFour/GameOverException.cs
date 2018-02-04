using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ConnectFour
{
    [Serializable]
    public class GameOverException : Exception
    {
        public GameOverException()
        {
        }

        public GameOverException(string message) : base(message)
        {
        }

        public GameOverException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameOverException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
