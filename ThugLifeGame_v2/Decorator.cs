using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ThugLifeGame_v2
{
    abstract class Decorator : Terrain
    {
        protected Terrain terrain;
        public Decorator(Terrain terrain, Canvas canvas) : base (canvas)
        {
            this.terrain = terrain;
        }

        protected void SetTerrain(Terrain terrain)
        {
            this.terrain = terrain;  
        }
    }
}
