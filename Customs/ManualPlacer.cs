using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Customs
{
    internal class ManualPlacer : ShipPlacer
    {

        public bool CarrPlaceCheck(Coordinate start, Coordinate direction)
        {
            return ManualCarrier(start, direction);
        }

        public Coordinate[] Carrier(Coordinate start, Coordinate direction)
        {
            Coordinate[] shipCords = new Coordinate[4];
            Coordinate[][] possibleCords = ManualCarrierCords(start);
            Coordinate[] carrCord = FindChoosen(possibleCords, start, direction,4);
            shipCords[0] = carrCord[0];
            shipCords[1] = carrCord[1];
            shipCords[2] = carrCord[2];
            shipCords[3] = carrCord[3];
            return shipCords;
        }

        private Coordinate[] FindChoosen(Coordinate[][] possibleCords, Coordinate start, Coordinate direction, int ship)
        {
            Coordinate[] carrCord = new Coordinate[ship];
            bool found = false;
            for (int i = 0; i < possibleCords.Length; i++)
            {
                if (found)
                {
                    break;
                }
                for (int j = 0; j < possibleCords[i].Length; j++)
                {
                    if (direction.Equals(possibleCords[i][j]))
                    {
                        found = true;
                        carrCord[0] = possibleCords[i][0];
                        carrCord[1] = possibleCords[i][1];
                        carrCord[2] = possibleCords[i][2];
                        carrCord[3] = possibleCords[i][3];
                        break;
                    }
                }
            }
            return carrCord;
        }

        public bool CollisionCheck(Coordinate[] coordinates, Coordinate[] shipCords)
        {
            bool didCollide = false;
            for (int i = 0; i < coordinates.Length; i++)
            {
                if (didCollide)
                {
                    break;
                }
                for (int j = 0; j < shipCords.Length; j++)
                {
                    if (coordinates[i].Equals(shipCords[j]))
                    {
                        didCollide = true;
                        break;
                    }
                }
            }
            return didCollide;
        }

    }
}
