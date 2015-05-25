using System;
using System.Collections.Generic;
using SharpGame.Internal;

namespace SharpGame
{
    public class Scene : IGameEntity
    {
        public Game Game { get; set; }

        private Actor rootActor;

        public Scene()
        {
            rootActor = new Actor("Root");
            rootActor.Scene = this;
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
