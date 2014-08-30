using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tick_Tac_Toe
{
    class Point
    {
        public int row {get ; set;}
        public int column {get; set; }

        public Point(int r, int c)
        {
            // TODO: Complete member initialization
            this.row = r;
            this.column = c;
        }
    }
}
