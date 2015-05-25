﻿using System;

namespace SharpGame
{ 
    public class Graphics
    {
        private GraphicsPrimitive [,] Buffer;
      //private GraphicsPrimitive [,] Copy;
        private Resolution Resolution;
        public GraphicsPrimitive[,] this[int x, int y]
        {
            get
            {
                return this[y, x];
            }
            private set
            {
                this[y, x] = value;
            }
        }
        public Graphics (Resolution Resolution)
        {
            this.Resolution = Resolution;
            Buffer = new GraphicsPrimitive [Resolution.x,Resolution.y];
            Console.CursorVisible = false;
            //Copy = new GraphicsPrimitive [Resolution.Y, Resolution.X];
        }
       
        public void PointChange (int x,int y, GraphicsPrimitive Symbol)
        {
            if (Symbol.Deep>=Buffer[x,y].Deep)
            Buffer[x, y] = Symbol;
        }
        public void DrawFrame ()
        {
            for (int y=0;y<Resolution.y;y++)
            {
                for (int x = 0; x < Resolution.x;x++)
                {
                    Buffer[x, y].PrintTo(x, y);
                }
            }
        }
        public void BufferClear()
        {
            for (int y = 0; y < Resolution.y; y++)
            {
                for (int x = 0; x < Resolution.x; x++)
                {
                    Buffer[x, y] = GraphicsPrimitive.Empty;
                }
            }
        }
    }
}