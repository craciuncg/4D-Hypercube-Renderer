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
    class Vector4
    {
        public float x, y, z, w;

        public Vector4()
        {
            x = 0;
            y = 0;
            z = 0;
            w = 0;
        }

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        
    }
}
