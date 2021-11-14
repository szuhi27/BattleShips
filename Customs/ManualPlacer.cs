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
            Coordinate[] coordinates = FindChoosen(possibleCords, direction,4);
            shipCords[0] = coordinates[0];
            shipCords[1] = coordinates[1];
            shipCords[2] = coordinates[2];
            shipCords[3] = coordinates[3];
            return shipCords;
        }

        public bool DestrPlaceCheck(Coordinate start, Coordinate direction)
        {
            return ManualDestroyer(start, direction);
        }

        public Coordinate[] Destroyer(Coordinate start, Coordinate direction, Coordinate[] allShips, int ships)
        {
            Coordinate[] shipCords = new Coordinate[3];
            Coordinate[][] possibleCords = ManualDestroyerCords(start);
            Coordinate[] coordinates = FindChoosen(possibleCords, direction, 3);
            bool didCollide = CollisionCheck(shipCords, allShips, ships);
            if (!didCollide)
            {
                shipCords[0] = coordinates[0];
                shipCords[1] = coordinates[1];
                shipCords[2] = coordinates[2];
            }
            return shipCords;
        }

        public bool HuntPlaceCheck(Coordinate start, Coordinate direction)
        {
            return ManualHunter(start, direction);
        }

        public Coordinate[] Hunter(Coordinate start, Coordinate direction, Coordinate[] allShips, int ships)
        {
            Coordinate[] shipCords = new Coordinate[2];
            Coordinate[][] possibleCords = ManualHunterCords(start);
            Coordinate[] coordinates = FindChoosen(possibleCords, direction, 2);
            if (!CollisionCheck(shipCords, allShips, ships))
            {
                shipCords[0] = coordinates[0];
                shipCords[1] = coordinates[1];
            }
            return shipCords;
        }

        private static Coordinate[] FindChoosen(Coordinate[][] possibleCords, Coordinate direction, int ship)
        {
            Coordinate[] cords = new Coordinate[ship];
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
                        for(int k = 0; k < cords.Length; k++)
                        {
                            cords[k] = possibleCords[i][k];
                        }
                        /*cords[0] = possibleCords[i][0];
                        cords[1] = possibleCords[i][1];
                        cords[2] = possibleCords[i][2];
                        cords[3] = possibleCords[i][3];*/
                        break;
                    }
                }
            }
            return cords;
        }

        public static bool CollisionCheck(Coordinate[] coordinates, Coordinate[] shipCords, int ships)
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
