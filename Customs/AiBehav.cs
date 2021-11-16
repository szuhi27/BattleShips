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

        public Coordinate[] GenerateShipsAi() {
            return shipPlacerAi.SetupShips();
        }

        public Coordinate[] GenerateShipsP1()
        {
            return shipPlacerP1.SetupShips();
        }

        //BORDER BETWEEN SETUP AND GAMEPLAY

        public Coordinate Attack(Coordinate prev, Coordinate[] hits, Coordinate[] prevShots)
        {
            Coordinate currentShot = new Coordinate(0,0);
            //if(!ShotMatch(prev,hits))
            //{
                currentShot = RandomAttack(prevShots);
            /*}
            else
            {
                TargetedAttack();
            }*/
            return currentShot;
        }

        private bool PrevDidHit(Coordinate prev, Coordinate[] hits)
        {
            bool didHit = false;
            return didHit;
        }

        private Coordinate RandomAttack(Coordinate[] previous)
        {
            Coordinate curr = new();
            Random random = new Random();
            do
            {
                curr.R = random.Next(1, 7);
                curr.C = random.Next(1, 7);
            }while(ShotMatch(curr, previous));
            return curr;
        }

        private void TargetedAttack()
        {
            throw new NotImplementedException();
        }
    }
}
