using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework
{
    struct GraphicsPrimitive
    {
        private char Symbol {get;set;}
        int Deep {get;set;}
        ConsoleColor Color;
        public bool Compare (GraphicsPrimitive that)
        {
            if (this.Symbol==that.Symbol&&
                this.Deep==that.Deep&&
                this.Color==that.Color)
                return true;
            return false;
        }
    }
}
