using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGame
{
    class Morse
    {
    #region var
    private
        const string roman = "abcdefghijklmnopqrstuvwqyz "; //roman array

        string[] morse = new string [27] 
        { //morse array
             ".-",     //a
             "-...",   //b
             "-.-.",   //c
             "-..",    //d
             ".",      //e
             "..-.",   //f
             "--.",    //g
             "....",   //h
             "..",     //i
             ".---",   //j
             "-.-",    //k
             ".-..",   //l
             "--",     //m
             "-.",     //n
             "---",    //o
             ".--.",   //p
             "--.-",   //q
             ".-.",    //r
             "...",    //s
             "-",      //t
             "..-",    //u
             "...-",   //v
             ".--",    //w
             "-..-",   //x
             "-.--",   //y
             "--..",   //z
             ""
        };
    #endregion
        
        public string DecodeMorse(string Code)
        {
            StringBuilder Text = new StringBuilder();
            int end=0;
            int start = 0;
            while (Code.IndexOf(' ', start)>=0)
            {
                end = Code.IndexOf(' ',start);
                string Sym = Code.Substring(start, end - start);
                Text.Append(SymbolFind(Sym));
                start = end + 1;
            }
            Text.Append(SymbolFind(Code.Substring(start)));
            return System.Text.RegularExpressions.Regex.Replace(Text.ToString(), " +", " "); 
        }
        private string SymbolFind(string str)
        {
            for (int i = 0;i < morse.Length; i++)
                if (str.Equals(morse[i]))
                    return roman.Substring(i, 1);
            return "#";
        }
        public void PrintLatin(string str)
        {
            Console.WriteLine(DecodeMorse(str));
        }
    }
}
