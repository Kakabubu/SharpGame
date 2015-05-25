using System;
using System.Collections.Generic;

namespace SharpGame
{
    public class Physics
    {
        private List<Actor>[,] Map;
        private List<Actor> this [int x,int y]
        {
            get
            {
                return this [y,x];
            }
            set
            {
                this[y, x] = value;
            }
        }

        public Physics(Resolution Resolution)
        {
            Map = new List<Actor>[(int)Resolution.x, (int)Resolution.y];
            for (int y = 0; y < (int)Resolution.y; y++)
                for (int x = 0; x < (int)Resolution.x; x++)
                    Map[x, y] = new List<Actor>(0);
        }

        public void Place(Vector3 Position, Actor Actor)
        {
            if (IsInBounds(Position))
            {
                int y = (int)Position.y;
                int x = (int)Position.x;

                Map[x,y].Add(Actor);
                if (Map[x,y].Count > 1)
                    foreach (Actor Each in Map[x,y])
                        foreach (Actor that in Map[x, y])
                            if (that!=Each)
                                Each.OnCollide(that);
            }
        }
        public void BufferClear()
        {
            for (int y = 0; y < Map.GetLength(1); y++)
                for (int x = 0; x < Map.GetLength(0); x++)
                    Map[x, y].Clear();
        }

        private bool IsInBounds(Vector3 that)
        {
            return that.x < Map.GetLength(0) && 
                   that.y < Map.GetLength(1) && 
                   that.x >= 0 && that.y >= 0;
        }
    }
}