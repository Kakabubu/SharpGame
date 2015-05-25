using System;
namespace SharpGame
{
    public struct Resolution
    {
        private int X;
        private int Y;
        
        public Resolution(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int x { get { return X; }}
        public int y { get { return Y; }}
    }
}
