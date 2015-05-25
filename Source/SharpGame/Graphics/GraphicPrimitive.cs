using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGame
{
// We don't need Equals(o) and GetHashCode() in this structure at the moment, so disable those warnings
#pragma warning disable CS0660, CS0661
    public struct GraphicPrimitive
#pragma warning restore CS0660, CS0661
    {
        public char symbol;
        public ConsoleColor foregroundColor;
        public ConsoleColor backgroundColor;
        public float depth;

        public GraphicPrimitive(char symbol, ConsoleColor foregroundColor, ConsoleColor backgroundColor, float depth)
        {
            this.symbol = symbol;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
            this.depth = depth;
        }

        public static bool operator ==(GraphicPrimitive a, GraphicPrimitive b)
        {
            return a.symbol == b.symbol &&
                   a.foregroundColor == b.foregroundColor &&
                   a.backgroundColor == b.backgroundColor &&
                   a.depth == b.depth;
        }

        public static bool operator !=(GraphicPrimitive a, GraphicPrimitive b)
        {
            return !(a == b);
        }
    }
}
