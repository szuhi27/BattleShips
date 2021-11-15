using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Customs
{
    internal class AiBehav
    {

        private ShipPlacer shipPlacerAi = new();
        private ShipPlacer shipPlacerP1 = new();

        private int shootNum;
        private Coordinate[] prevShots = new Coordinate[36], prevHits = new Coordinate[12]; 
        private Coordinate currentShoot, prevHit;

        public Coordinate[] GenerateShipsAi() {
            return shipPlacerAi.SetupShips();
        }

        public Coordinate[] GenerateShipsP1()
        {
            return shipPlacerP1.SetupShips();
        }

        public Coordinate Attack()
        {
            currentShoot = new Coordinate(0,0);
            if(prevHit.R == 0 && prevHit.C == 0)
            {
                RandomAttack();
            }
            else
            {
                TargetedAttack();
            }
            prevShots[shootNum] = currentShoot;
            shootNum++;
            return currentShoot;
        }

        private void RandomAttack()
        {
            Coordinate curr = new();
            Random random = new Random();
            curr.R = random.Next(1,7);
            curr.C = random.Next(1,7);
            if (!AlreadyShot(curr))
            {
                currentShoot = curr;
            }
            else
            {
                RandomAttack();
            }


        }

        private bool AlreadyShot(Coordinate cord)
        {
            bool shotThere = false;
            for(int i=0; i<37; i++)
            {
                if(prevShots[i].Equals(cord))
                {
                    shotThere = true;
                    break;
                }
            } 
            return shotThere;
        }

        private void TargetedAttack()
        {
            throw new NotImplementedException();
        }

        private void Save()
        {
            /*string path = ;
            PlantLevels newpLvl = new PlantLevels { fieldLvl = 1, fieldReq = 50, gardenLvl = 1, gardenReq = 50, orchardLvl = 1, orcahrdReq = 50 };
            string newLevelJson = JsonUtility.ToJson(newpLvl);
            File.WriteAllText(linkXP, newLevelJson);*/
            string str = File.ReadAllText(@"Resources");

        }
    }
}
