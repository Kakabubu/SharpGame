//using System.Windows; for using wpf
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using GameFramework.Internal;

namespace GameFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Pong pong = new Pong();
            string path="";
            Racket ra = GiveMe<Racket>(path);
            var rack = new Racket();
            var ball = new Ball();
            rack.SetControlsDown(ConsoleKey.S);
            List<object>ForExport=new List<object>();
            ForExport.Add(rack); ForExport.Add(ball);

            toJson(ForExport);
            //new Morse().PrintRoman("-... ... --- ...  --- ... ---           --- ... ---");
        }

        public static TObject GiveMe<TObject>(string path) where TObject: class, new()
        {
            TObject obj = new TObject();
            return obj;
        }

        public static void toJson (object obj)
        {
            File.WriteAllText(PathGenerate(obj), ToJsonString(obj));
        }
        public static void toJson(List<object> lst)
        {
            foreach (object obj in lst)
                toJson(obj);
        }
        public static string ToJsonString(object obj)
        {
            StringBuilder ex = new StringBuilder("{");
                foreach (var inf in obj.GetType().GetFields
                        (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                    ex.AppendFormat(" \"{0}\": \"{1}\",", inf.Name, inf.GetValue(obj));
                ex.Length -= 1;
                ex.Append("}");
            return ex.ToString();
        }
        public static string PathGenerate(object obj)
        {
            StringBuilder Path = new StringBuilder(@"D:\Anton\Education\Unity 3D C#\Repos\ShrpGm\Source\");
            Path.AppendFormat("{0}{1}", obj.GetType().Name.ToString(), ".txt");
            return Path.ToString();
        }
    }
}
