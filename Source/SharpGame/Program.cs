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
            /*  
             * Good Old Games
             * Humble Bundle 
             */
            //Pong pong = new Pong();
            //string path = "";
            //Racket ra = GiveMe<Racket>(path);
            //var rack = new Racket();
            //var ball = new Ball();
            //rack.SetControlsDown(ConsoleKey.S);
            //List<object> forexport = new List<object>();
            //forexport.Add(rack); forexport.Add(ball);

            //toJson(forexport);
            //new Morse().PrintLatin("-... ... --- ...  --- ... ---           --- ... ---");


            var obj = JObject.Parse(File.ReadAllText(@"test.scene"));
            Scene test = new Scene();
            foreach (var actor in obj["__actors"])
            {
                test.AddActor(ParseActor(actor));
            }
            Game game = new Game();
            game.TargetFPS = 30;
            game.Initialize("");

            game.Run(test);

        }
        public static Actor ParseActor(JToken JT)
        {
            JToken jactor = JT;

            string draft = (string)JT["__draft"];
            if (draft != null)
            {
                var obj = JObject.Parse(File.ReadAllText(draft));
                obj.Merge(JT);
                jactor = obj;
            }
            Actor act = JT.ToObject<Actor>();
            foreach (var cmp in jactor["__components"])
                act.AddComponent(ParseComponent(cmp));
            foreach (var ch in jactor["__children"])
                act.AddChild(ParseActor(ch));
            return act;
        }
        public static ActorComponent ParseComponent(JToken JT)
        {
            Type type = Type.GetType((string)JT["__type"]);
            var Cmp = JT.ToObject(type);
            return Cmp as ActorComponent;
        }

        public static TObject GiveMe<TObject>(string path) where TObject : class, new()
        {
            TObject obj = new TObject();
            return obj;
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
