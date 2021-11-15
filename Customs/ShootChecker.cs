using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Customs
{
    internal class ShootChecker
    {
        internal bool ShotMatch(Coordinate shot, Coordinate[] prevShots)
        {
            bool match = false;
            for (int i = 0; i < prevShots.Length; i++)
            {
                if (shot.Equals(prevShots[i]))
                {
                    match = true;
                    break;
                }
            }
            return match;
        }

        internal string HitCheck(Coordinate coordinate, Coordinate[] p1Hits, Coordinate[] p2Ships)
        {
            return "";
        }
    }
}
