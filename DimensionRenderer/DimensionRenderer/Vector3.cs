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
    class Vector3
    {
        public float x, y, z;

        public Vector3()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Mult(float val)
        {
            x *= val;
            y *= val;
            z *= val;
        }
    }
}
