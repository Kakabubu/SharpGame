//using System.Windows; for using wpf
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GameFramework
{
    class Program
    {
        struct TestStruct
        {
            int intField;
            bool boolField;
            string stringField;

            public TestStruct(int i, bool b, string s) : this()
            {
                intField = i;
                boolField = b;
                stringField = s;
            }
        }

        static void Main(string[] args)
        {
            Racket comp = new Racket();
            Console.WriteLine(export(comp));
            // Pong pong = new Pong();
        }
        public static string export(object obj)
        {
            StringBuilder JsonOut=new StringBuilder ("{");
            foreach (var inf in obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                JsonOut.AppendFormat(" {0}:{1} ", inf.Name, inf.GetValue(obj));
            JsonOut.Append("}");
            return JsonOut.ToString();
        }
    }
}
