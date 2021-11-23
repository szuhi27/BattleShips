using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
using BattleShips.Customs;

namespace BattleShips.Tests
{
    [TestClass]
    public class AiBehavUnitTests
    {

        [DataRow(new int[] { 1, 1 }, new int[] {2,1,1,2})]
        [DataTestMethod]
        public void FindCoordsAround(int[] inCoord,int[] exptCoords)
        {
            Coordinate input = new(inCoord[0], inCoord[1]);
            List<Coordinate> exp = new();
            for(int i = 0; i < exptCoords.Length; i+=2)
            {
                exp.Add(new Coordinate(exptCoords[i],exptCoords[i+1]));
            }

            var aibehav = new AiBehav();

            List<Coordinate> testList = aibehav.CoordsAround(input);

            Assert.AreEqual(exp, testList);

        }

        [DataRow(new int[] { 1, 1 }, new int[] { 2, 2})]
        [DataTestMethod]
        public void FindCoordsAroundExtended(int[] inCoord, int[] exptCoords)
        {
            Coordinate input = new(inCoord[0], inCoord[1]);
            List<Coordinate> exp = new();
            for (int i = 0; i < exptCoords.Length; i += 2)
            {
                exp.Add(new Coordinate(exptCoords[i], exptCoords[i + 1]));
            }

            var aibehav = new AiBehav();

            List<Coordinate> testList = aibehav.CoordsAround(input);

            Assert.AreEqual(exp, testList);

        }

    }
}
