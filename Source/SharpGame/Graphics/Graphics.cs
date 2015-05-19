using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework
{
    class Graphics
    {
        private GraphicsPrimitive [,] Buffer;
        GraphicsPrimitive[,] Copy;
        public Graphics ()
        {
            Buffer = new GraphicsPrimitive [20,20];
            Copy = new GraphicsPrimitive[Buffer.GetLength(0), Buffer.GetLength(1)];
        }
        public void NextScreen ()
        {

            if (!Buffer[1, 1].Compare(Copy[1, 1]))
            {;}
            var tmp=Buffer;
            Buffer=Copy;
            Copy = tmp;
        }
        
    }
}
