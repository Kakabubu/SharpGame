using System;
using System.Collections.Generic;
using SharpGame.Internal;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace SharpGame
{
    [Serializable]
    public class Scene : IGameEntity
    {
        public Game Game { get; set; }
        [JsonIgnore]
        public List<Actor> Actors{get{return rootActor.Children;}}
        private List<Actor> forDestroy = new List<Actor>();
        private Actor rootActor;

        public Scene()
        {
            rootActor = new Actor("Root");
            rootActor.Scene = this;
        }
        public void ForDestroy(Actor that)
        {
           forDestroy.Add(that);
        }

        public void PostUpdate()
        {
            foreach (Actor act in forDestroy)
                act.DestroyImmediate();

            forDestroy.Clear();
        }
        
        #region Actors
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
