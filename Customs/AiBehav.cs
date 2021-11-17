using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Customs
{
    internal class AiBehav : ShootChecker
    {

        private ShipPlacer shipPlacerAi = new();
        private ShipPlacer shipPlacerP1 = new();

        public Coordinate[] GenerateShipsAi()
        {
            return shipPlacerAi.SetupShips();
        }

        public Coordinate[] GenerateShipsP1()
        {
            return shipPlacerP1.SetupShips();
        }

        //BORDER BETWEEN SETUP AND GAMEPLAY
        //=================================

        public Coordinate Attack(Coordinate prev, Coordinate[] hits, Coordinate[] prevShots)
        {
            Coordinate currentShot = new(0,0);
            if (!ShotMatch(prev, hits) && !HitAroundLastMiss(prev, hits))
            {
                currentShot = RandomAttack(prevShots);
            }
            else if (ShotMatch(prev, hits))
            {
                currentShot = RandomAroundLastHit(prev, prevShots, false);
            } else if(!ShotMatch(prev, hits) && HitAroundLastMiss(prev, hits))
            {
                currentShot = RandomAroundLastMiss(prev, hits, prevShots);
            }
            return currentShot;
        }

        //Check if there was a hit around the last miss
        private bool HitAroundLastMiss(Coordinate prev, Coordinate[] hits)
        {
            bool hit = false;
            List<Coordinate> cordsAround = CoordsAround(prev);
            for (int i = 0; i < cordsAround.Count; i++)
            {
                if (ShotMatch(cordsAround[i], hits))
                {
                    hit = true;
                    break;
                }
            }
            return hit;
        }

        private Coordinate RandomAttack(Coordinate[] previous)
        {
            Coordinate curr = new();
            Random random = new Random();
            do
            {
                curr.R = random.Next(1, 7);
                curr.C = random.Next(1, 7);
            } while (ShotMatch(curr, previous));
            return curr;
        }

        private List<Coordinate> CoordsAround(Coordinate prev)
        {
            List<Coordinate> cordsAround = new();
            if (prev.R < 6) { cordsAround.Add(new Coordinate(prev.R + 1, prev.C)); }
            if (prev.R > 1) { cordsAround.Add(new Coordinate(prev.R - 1, prev.C)); }
            if (prev.C < 6) { cordsAround.Add(new Coordinate(prev.R, prev.C + 1)); }
            if (prev.C > 1) { cordsAround.Add(new Coordinate(prev.R, prev.C - 1)); }
            return cordsAround;
        }

        private List<Coordinate> CoordsAroundExtended(Coordinate prev)
        {
            List<Coordinate> coordsAE = new();
            if (prev.R < 6 && prev.C < 6) { coordsAE.Add(new Coordinate(prev.R + 1, prev.C + 1)); }
            if (prev.R > 1 && prev.C > 1) { coordsAE.Add(new Coordinate(prev.R - 1, prev.C - 1)); }
            if (prev.C < 6 && prev.R > 1) { coordsAE.Add(new Coordinate(prev.R - 1, prev.C + 1)); }
            if (prev.C > 1 && prev.R < 6) { coordsAE.Add(new Coordinate(prev.R + 1, prev.C - 1)); }
            return coordsAE;
        }

        //TARGETED ATTACK ON HIT
        private Coordinate RandomAroundLastHit(Coordinate prev, Coordinate[] prevShots, bool searchPrevHit)
        {
            List<Coordinate> coordsAround = CoordsAround(prev);
            List<Coordinate> goodCords = FindGoodCords(prev, prevShots, searchPrevHit, coordsAround);
            return TargetedShot(goodCords, prevShots);
        }

        private List<Coordinate> FindGoodCords(Coordinate prev, Coordinate[] prevShots, bool searchPrevHit, List<Coordinate> coordsAround)
        {
            List<Coordinate> goodCords = new List<Coordinate>();
            for (int j = 0; j < coordsAround.Count; j++)
            {
                if (!ShotMatch(coordsAround[j], prevShots))
                {
                    goodCords.Add(coordsAround[j]);

                }
            }

            if (goodCords.Count == 0 && searchPrevHit)
            {
                List<Coordinate> coordsAroundExtended = CoordsAroundExtended(prev);
                goodCords = FindGoodCords(prev, prevShots, false, coordsAroundExtended);
            }
            return goodCords;
        }


        private Coordinate TargetedShot(List<Coordinate> goodCoords, Coordinate[] prevShots)
        {
            Coordinate coordinates = new();
            Random random = new Random();
            if (goodCoords.Count > 0)
            {
                coordinates = goodCoords[random.Next(0, goodCoords.Count)];
            }
            else
            {
                coordinates = RandomAttack(prevShots);
            }
            return coordinates;
        }

        //TARGETED ATTACK ON MISS
        private Coordinate RandomAroundLastMiss(Coordinate prev, Coordinate[] hits, Coordinate[] prevShots)
        {
            List<Coordinate> goodCords = FindHitCoords(prev, hits);
            Random random = new Random();
            Coordinate randomHit = goodCords[random.Next(0,goodCords.Count)];
            return RandomAroundLastHit(randomHit,prevShots, true);
        }

        private List<Coordinate> FindHitCoords(Coordinate prev, Coordinate[] prevHits)
        {
            List<Coordinate> goodCords = new List<Coordinate>();
            List<Coordinate> coordsAround = CoordsAround(prev);
            for (int j = 0; j < coordsAround.Count; j++)
            {
                if (ShotMatch(coordsAround[j], prevHits))
                {
                    goodCords.Add(coordsAround[j]);

                }
            }
            return goodCords;
        }
    }
}
