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

        private int shootNum, saves, trys;
        private Coordinate[] prevShots = new Coordinate[36], prevHits = new Coordinate[12], shipCords = new Coordinate[12]; 
        private Coordinate currentShoot, prevHit;

        public Coordinate[] GenerateShips() {
            shipCords = new Coordinate[12];
            saves = 0;
            trys = 0;
            Carrier();
            DestroyerCordCalc();
            //Destroyer1();
            //Destroyer2();
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
                saves++;
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
            int place = rand.Next(0,2);
            Coordinate[] carrCard = new Coordinate[] {possibleCords[place][0], possibleCords[place][1], possibleCords[place][2], possibleCords[place][3]};
            return carrCard;
        }

        /*private void Destroyer1()
        {
            Coordinate start = new();
            Random random = new();
            start.R = random.Next(1,7);
            start.C = random.Next(1,7);
            Coordinate[] destrCords = DestroyerCordCalc(start);
            int place = 0;
            for (int i = saves; i < destrCords.Length; i++)
            {
                shipCords[i] = destrCords[place];
                place++;
                saves++;
            }
        }*/

        /*private void Destroyer2()
        {
            Coordinate start = new();
            Random random = new();
            start.R = random.Next(1,7);
            start.C = random.Next(1,7);
            Coordinate[] destrCords = DestroyerCordCalc(start);
            int place = 0;
            if(destrCords[0].R == 0)
            {
                Destroyer2();
            }
            else
            {
                for (int i = saves; i < destrCords.Length; i++)
                {
                    shipCords[i] = destrCords[place];
                    place++;
                    saves++;
                }
            }
            
        }*/

        //private Coordinate[] DestroyerCordCalc(Coordinate start)
        private void DestroyerCordCalc()
        {
            Coordinate start = new();
            Random random = new();
            start.R = random.Next(1, 7);
            start.C = random.Next(1, 7);
            int posibilities = 0;
            //Coordinate[] destrCard = new Coordinate[3];
            //Coordinate[] tempCord = new Coordinate[3];
            Coordinate[][] possibleCords = new Coordinate[4][];
            int goodCord = 0;

           // possibleCords[0] = new Coordinate[] {start, new Coordinate(start.R - 1, start.C), new Coordinate(start.R - 2, start.C)};
            //Console.WriteLine("asd: "+possibleCords[0][0].R);

            if (start.R - 2 >= 1)
            {
                possibleCords[goodCord] = new Coordinate[] {start,new Coordinate(start.R-1,start.C),new Coordinate(start.R-2,start.C)};
                if(!CollisionCheck(possibleCords[goodCord]))
                {
                    //possibleCords[goodCord] = new Coordinate[] { tempCord[0], tempCord[1], tempCord[2]};
                    posibilities++;
                    goodCord++;
                }
            }
            if (start.R + 2 <= 6)
            {
                possibleCords[goodCord] = new Coordinate[] {start, new Coordinate(start.R+1,start.C), new Coordinate(start.R+2,start.C)};
                if (!CollisionCheck(possibleCords[goodCord]))
                {
                    //possibleCords[goodCord] = new Coordinate[] { tempCord[0], tempCord[1], tempCord[2] };
                    posibilities++;
                    goodCord++;
                }
            }
            if (start.C - 2 >= 1)
            {
                possibleCords[goodCord] = new Coordinate[] {start, new Coordinate(start.R,start.C-1), new Coordinate(start.R,start.C-2)};
                if (!CollisionCheck(possibleCords[goodCord]))
                {
                    //possibleCords[goodCord] = new Coordinate[] { tempCord[0], tempCord[1], tempCord[2] };
                    posibilities++;
                    goodCord++;
                }
            }
            if (start.C + 2 <= 6)
            {
                possibleCords[goodCord] = new Coordinate[] {start, new Coordinate(start.R,start.C+1), new Coordinate(start.R,start.C+2)};
                if (!CollisionCheck(possibleCords[goodCord]))
                {
                    //possibleCords[goodCord] = new Coordinate[] { tempCord[0], tempCord[1], tempCord[2] };
                    posibilities++;
                    goodCord++;
                }
            }


            if (posibilities > 0)
            {
                Random rand = new Random();
                int place = rand.Next(0,goodCord);
                //destrCard = new Coordinate[] { possibleCords[place][0], possibleCords[place][1], possibleCords[place][2]};
                /*destrCard[0] = possibleCords[place][0];
                destrCard[1] = possibleCords[place][1];
                destrCard[2] = possibleCords[place][2];*/
                Coordinate[] destrCords = new Coordinate[] { possibleCords[place][0], possibleCords[place][1], possibleCords[place][2] };

                int place2 = 0;
                for (int i = saves; i < destrCords.Length; i++)
                {
                    shipCords[i] = destrCords[place];
                    place2++;
                    saves++;
                }
            }
            else if(trys < 10)
            {
                DestroyerCordCalc();
                trys++;
            }
            //return destrCard;
        }

        private void Hunter()
        {
            Coordinate start = new();
            Random random = new();
            start.R = random.Next(1,7);
            start.C = random.Next(1,7);
            Coordinate[] huntCords = HunterCordCalc(start, 10);
            int place = 0;
            if (huntCords[0].R == 0)
            {
                Hunter();
            }
            else
            {
                for (int i = saves; i < huntCords.Length; i++)
                {
                    shipCords[i] = huntCords[place];
                    place++;
                    saves++;
                }
            }
        }

        private Coordinate[] HunterCordCalc(Coordinate start, int placedNum)
        {
            int posibilities = 0;
            Coordinate[] destrCord = new Coordinate[2];
            Coordinate[] tempCord = new Coordinate[2];
            Coordinate[][] possibleCords = new Coordinate[4][];
            int goodCord = 0;

            if (start.R - 1 >= 1)
            {
                tempCord = new Coordinate[] { start, new Coordinate(start.R - 1, start.C)};
                if (!CollisionCheck(tempCord))
                {
                    possibleCords[goodCord] = tempCord;
                    posibilities++;
                    goodCord++;
                }
            }
            if (start.R + 1 <= 6)
            {
                tempCord = new Coordinate[] { start, new Coordinate(start.R + 1, start.C)};
                if (!CollisionCheck(tempCord))
                {
                    possibleCords[goodCord] = tempCord;
                    posibilities++;
                    goodCord++;
                }
            }
            if (start.C - 1 >= 1)
            {
                tempCord = new Coordinate[] { start, new Coordinate(start.R, start.C - 1)};
                if (!CollisionCheck(tempCord))
                {
                    possibleCords[goodCord] = tempCord;
                    posibilities++;
                    goodCord++;
                }
            }
            if (start.C + 1 <= 6)
            {
                tempCord = new Coordinate[] { start, new Coordinate(start.R, start.C + 1)};
                if (!CollisionCheck(tempCord))
                {
                    possibleCords[goodCord] = tempCord;
                    posibilities++;
                    goodCord++;
                }
            }


            if (posibilities != 0)
            {
                Random rand = new Random();
                int place = rand.Next(0,goodCord);
                destrCord = new Coordinate[] { possibleCords[place][0], possibleCords[place][1]};
            }
            return destrCord;
        }

        private bool CollisionCheck(Coordinate[] coordinates)
        {
            bool didCollide = false;
            for(int i=0; i<coordinates.Length; i++)
            {
                if (didCollide)
                {
                    break;
                }
                for(int j=0; j < saves; j++)
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
