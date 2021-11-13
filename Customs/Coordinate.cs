using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Customs
{
    struct Coordinate
    {
    
        public int H { get; set; }
        public int W { get; set; }


        public Coordinate(int h, int w)
        {
            this.H = h;
            this.W = w;
        }

    }
}
