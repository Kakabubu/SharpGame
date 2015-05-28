//using System.Windows; for using wpf
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using SharpGame.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharpGame
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Pong pong = new Pong();
            //new Morse().PrintLatin("-... ... --- ...  --- ... ---           --- ... ---");
            
            //Scene test = Load.Scene(@"..\..\.scene\test.scene");
            //Game game = new Game();
            //game.TargetFPS = 30;
            //game.Initialize("");

            //game.Run(test);
        }

        public static void toJson(object obj)
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
        //{
        ////    Pong Pong = new Pong();
        //    TestActorsSearching();

        //    Resources res = new Resources();
        //    res.RegisterLoader(".scene", new SceneLoader());
        //    Scene testScene = res.Load<Scene>("test.scene");

        //    Game game = new Game();
        //    game.TargetFPS = 60;
        //    game.Initialize("some");
        //    game.Run(testScene);
        //}

        //public static void TestActorsSearching()
        //{
        //    const int TEST_ACTORS_CNT = 5;
        //    Actor nodeI = new Actor("Node I");
        //    Actor nodeIIA = new Actor("Node II A");
        //    Actor nodeIIB = new Actor("Node II B");
        //    Actor nodeIIIA = new Actor("Node III A");
        //    Actor nodeIIIB = new Actor("Node III B");

        //    Scene scene = new Scene();

        //    nodeIIB.AddChild(nodeIIIA);
        //    nodeIIB.AddChild(nodeIIIB);
        //    nodeI.AddChild(nodeIIA);
        //    nodeI.AddChild(nodeIIB);
        //    scene.AddActor(nodeI);

        //    Debug.Assert(scene.FindActor(actor => actor.Name == nodeIIIB.Name) == nodeIIIB);
        //    Debug.Assert(scene.FindActor(actor => actor.Name == nodeIIA.Name) == nodeIIA);
        //    Debug.Assert(scene.FindAllActors(actor => actor.Name.Contains("Node")).Count == TEST_ACTORS_CNT);
        //    var foundActors = scene.FindAllActors(actor => actor.Name.Contains("A"));
        //    Debug.Assert(foundActors.Count == 2 && foundActors.Contains(nodeIIA) && foundActors.Contains(nodeIIIA));
        //}
    }
}
