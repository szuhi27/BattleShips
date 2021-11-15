using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Customs
{
    [Serializable]
    internal class WrongCoordinateException : Exception
    {

        public WrongCoordinateException()
        {
        }

        public WrongCoordinateException(string message)
            : base(message)
        {
        }

        public WrongCoordinateException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
