using System;

namespace SharpGame
{
    public struct GraphicsPrimitive
    {
        char Symbol;
        public int Deep;
        private ConsoleColor Color;
        public GraphicsPrimitive(char S, int D, ConsoleColor C)
            : this()
        {
            this.Symbol = S;
            this.Deep = D;
            this.Color = C;
        }

        public bool Compare(GraphicsPrimitive that)
        {
            if (this.Symbol == that.Symbol &&
                this.Deep == that.Deep &&
                this.Color == that.Color)
                return true;
            return false;
        }
        public void PrintTo(int X, int Y)
        {
            Console.ForegroundColor = this.Color;
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
        }
        public void InBounds()
        {

        }
        public static readonly GraphicsPrimitive Empty = new GraphicsPrimitive(' ', 0, ConsoleColor.Black);
    }
}
