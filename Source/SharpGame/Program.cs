//using System.Windows; for using wpf
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GameFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            // Pong pong = new Pong();
            var rack = new Racket();
            var ball = new Ball();
            rack.SetControlsDown(ConsoleKey.S);
            List<object>ForExport=new List<object>();
            ForExport.Add(rack); ForExport.Add(ball);

            export(ForExport);
        }
        public static void export(object obj)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter
                  (@"D:\Anton\Education\Unity 3D C#\Repos\ShrpGm\Source\JSON.txt"))
            {
                file.Write("{");
                foreach (var inf in obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                    file.Write(" {0}:{1} ", inf.Name, inf.GetValue(obj));
                file.WriteLine("}");
            }
        }
        public static void export(List <object> lotsOf)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter
                  (@"D:\Anton\Education\Unity 3D C#\Repos\ShrpGm\Source\JSON.txt"))
            {
                foreach (object obj in lotsOf)
                {
                    file.Write("{");
                    foreach (var inf in obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                        file.Write(" {0}:{1} ", inf.Name, inf.GetValue(obj));
                    file.WriteLine("}");
                }
            }
        }
    }
}
