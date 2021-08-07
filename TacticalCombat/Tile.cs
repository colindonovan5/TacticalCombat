using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TacticalCombat
{
    public class Tile
    {

        public Button Button { get; set; }
        private int x { get; set; }
        public int X
        {
            get
            {
                return x;
            }
        }

        private int y { get; set; }
        public int Y
        {
            get
            {
                return y;
            }
        }
        public bool Painted { get; set; }
        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
            Painted = false;
        }
    }
}
