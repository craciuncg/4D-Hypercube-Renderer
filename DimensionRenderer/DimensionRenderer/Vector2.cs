using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionRenderer
{
    /*
       Author: Craciun Cristian-George
       Version: 1.0
       Description: This is the main code for rendering the 4D Hypercube using .NET library with some additional settings to it.
   */
    class Vector2
    {
        public float x, y;

        public Vector2()
        {
            x = 0;
            y = 0;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void Mult(float val)
        {
            x *= val;
            y *= val;
        }
    }
}
