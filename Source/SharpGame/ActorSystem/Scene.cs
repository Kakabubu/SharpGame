using System;
using System.Collections.Generic;
using SharpGame.Internal;
using Newtonsoft.Json.Linq;
using System.IO;

namespace SharpGame
{
    [Serializable]
    public class Scene : IGameEntity
    {
        public Game Game { get; set; }
        public List<Actor> Actors{get{return rootActor.Children;}}
       
        private Actor rootActor;

        public Scene()
        {
            rootActor = new Actor("Root");
            rootActor.Scene = this;
        }
        
        #region Actors
        public void LoadFrom(string path)
        {
            var obj = JObject.Parse(File.ReadAllText(path));
            foreach (var Jactor in obj["__actors"])
            {
                this.AddActor(Load.Actor(Jactor));
            }
        }
        public void AddActor(Actor actor)
        {
            rootActor.AddChild(actor);
        }

        public Actor FindActor(Predicate<Actor> predicate)
        {
            return rootActor.FindChild(predicate);
        }

        public List<Actor> FindAllActors(Predicate<Actor> predicate)
        {
            return rootActor.FindAllChildren(predicate);
        }
        #endregion

        #region IGameEntity implementation
        public void Awake()
        {
            rootActor.Awake();
        }

        public void Start()
        {
            rootActor.Start();
        }

        public void Update(float deltaTime)
        {
            rootActor.Update(deltaTime);
        }

        public void Draw(float deltaTime)
        {
            rootActor.Draw(deltaTime);
        }

        public void OnDestroy()
        {
            rootActor.OnDestroy();
            rootActor = null;
        }
        #endregion
    }
}
