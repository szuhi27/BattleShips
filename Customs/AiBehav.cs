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

        private int shootNum;
        private Coordinate[] prevShots = new Coordinate[36], prevHits = new Coordinate[12], shipCords = new Coordinate[12]; 
        private Coordinate currentShoot, prevHit;

        public Coordinate[] GenerateShips() {
            Carrier();
            Destroyer1();
            Destroyer2();
            Hunter();
            return shipCords;
        }

        private void Carrier()
        {
            Coordinate start = new();
            Random random = new();
            start.R = random.Next(1,7);
            start.C = random.Next(1,7);           
            Coordinate[] carrierCords = CarrierCordCalc(start);
            for(int i = 0; i < carrierCords.Length; i++)
            {
                shipCords[i] = carrierCords[i];
            }
        }

        private Coordinate[] CarrierCordCalc(Coordinate start)
        {
            Coordinate[][] possibleCords = new Coordinate[2][];
            int goodCord = 0;
            if (start.R - 3 >= 1)
            {
                possibleCords[goodCord] = new Coordinate[]
                    {start,new Coordinate(start.R-1,start.C),new Coordinate(start.R-2,start.C), new Coordinate(start.R-3,start.C)};
                goodCord++;
            }
            if (start.R + 3 <= 6)
            {
                possibleCords[goodCord] = new Coordinate[]
                    {start, new Coordinate(start.R+1,start.C), new Coordinate(start.R+2,start.C), new Coordinate(start.R+3,start.C)};
                goodCord++;
            }
            if (goodCord < 2 && start.C - 3 >= 1)
            {
                possibleCords[goodCord] = new Coordinate[]
                        {start, new Coordinate(start.R,start.C-1), new Coordinate(start.R,start.C-2), new Coordinate(start.R,start.C-3)};
                goodCord++;
            }
            if (goodCord < 2 && start.C + 3 <= 6)
            {
                possibleCords[goodCord] = new Coordinate[]
                    {start, new Coordinate(start.R,start.C+1), new Coordinate(start.R,start.C+2), new Coordinate(start.R,start.C+3)};
            }

            Random rand = new Random();
            int place = rand.Next(0, 2);
            Coordinate[] carrCard = new Coordinate[] {possibleCords[place][0], possibleCords[place][1], possibleCords[place][2], possibleCords[place][3]};
            return carrCard;
        }

        private void Destroyer1()
        {
            throw new NotImplementedException();
        }

        private void Destroyer2()
        {
            throw new NotImplementedException();
        }

        private void Hunter()
        {
            throw new NotImplementedException();
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
