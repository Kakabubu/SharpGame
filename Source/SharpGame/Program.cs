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
            //Pong pong = new Pong();


            Actor Ship = new Actor();
            Ship.AddComponent(new Ship());
            Ship.AddComponent(new ASCIIPainter());
            Scene Scene = new Scene();
            Game Game = new Game();
            Scene.AddActor(Ship);
            Game.Initialize("space");
            Game.TargetFPS = 60;
            Game.Run(Scene);


            //Scene test = Load.Scene(@"..\..\.scene\test.scene");
            //Game game = new Game();
            //game.TargetFPS = 30;
            //game.Initialize("");
            //game.Run(test);
        }

        #region vitalii
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
        #endregion
    }
}
