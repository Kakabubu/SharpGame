﻿using System;
using System.Collections.Generic;
using SharpGame.Internal;
using Newtonsoft.Json;

namespace SharpGame
{
    public class Actor : GameEntityContainer<Actor>
    {
        [JsonIgnore]
        private Scene scene;
        [JsonIgnore]
        public Scene Scene
        {
            get
            {
                return scene;
            }
            set
            {
                scene = value;

                foreach (Actor actor in children)
                {
                    actor.Scene = value;
                }
            }
        }
        [JsonIgnore]
        public Actor Parent { get; set; }
        [JsonIgnore]
        public List<Actor> Children { get { return children; } }
        [JsonIgnore]
        public List<ActorComponent> Components { get { return componentContainer.Components; } }

        public string Name { get; set; }

        [JsonIgnore]
        public Vector3 WorldPosition
        {
            get
            {
                Vector3 offset = Vector3.Zero;
                Actor parent = Parent;
                while (parent != null)
                {
                    offset += parent.LocalPosition;
                    parent = parent.Parent;
                }
                return LocalPosition + offset;
            }
            set
            {
                Vector3 offset = value - WorldPosition;
                LocalPosition += offset;
            }
        }
        
        public Vector3 LocalPosition { get; set; }

        private ComponentContainer componentContainer;

        public Actor(string name = "Actor")
        {
            this.Name = name;
            componentContainer = new ComponentContainer(this);
        }

        #region Children
        
        public override void AddChild(Actor actor)
        {
            actor.Parent = this;
            actor.Scene = Scene;
            base.AddChild(actor);
        }

        public Actor FindChild(Predicate<Actor> predicate)
        {
            Actor foundActor;
            for (int i = 0; i < children.Count; i++)
            {
                foundActor = children[i].FindChildRecursive(predicate);

                if (foundActor != null)
                    return foundActor;
            }
            return null;
        }

        private Actor FindChildRecursive(Predicate<Actor> predicate)
        {
            if (predicate(this))
                return this;
            else
            {
                for (int i = 0; i < children.Count; i++)
                {
                    Actor foundActor = children[i].FindChildRecursive(predicate);

                    if (foundActor != null)
                        return foundActor;
                }
                return null;
            }
        }

        public List<Actor> FindAllChildren(Predicate<Actor> predicate)
        {
            var foundActors = new List<Actor>();
            children.ForEach(child => child.FindAllChildrenRecursive(predicate, foundActors));
            return foundActors;
        }
        
        private void FindAllChildrenRecursive(Predicate<Actor> predicate, List<Actor> outList)
        {
            if (predicate(this))
                outList.Add(this);
            children.ForEach(child => child.FindAllChildrenRecursive(predicate, outList));
        }

        private void DestroyChild(Actor that)
        {
            children.Remove(that);
            that.OnDestroy();
        }
        public void DestroyImmediate()
        {
            Parent.DestroyChild(this);
        }

        #endregion

        #region Components
        public void AddComponent(ActorComponent component)
        {
            componentContainer.AddChild(component);
        }

        public TComponent GetComponent<TComponent>() where TComponent : class
        {
            return componentContainer.GetComponent<TComponent>();
        }

        public List<TComponent> GetAllComponents<TComponent>() where TComponent : class
        {
            return componentContainer.GetAllComponents<TComponent>();
        }
        #endregion

        #region IGameEntity implementation
        public override void Start()
        {
            componentContainer.Start();
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            componentContainer.Update(deltaTime);
            base.Update(deltaTime);
        }

        public override void Draw(float deltaTime)
        {
            componentContainer.Draw(deltaTime);
            base.Draw(deltaTime);
        }

        public override void OnDestroy()
        {
            componentContainer.OnDestroy();
            componentContainer = null;
            base.OnDestroy();
        }

        #endregion
        public void OnCollide (Actor Exciter)
        {
            componentContainer.OnCollide(Exciter);
        }

        public bool InBounds()
        {
            return this.WorldPosition.x < scene.Game.Resolution.x-1 &&
                   this.WorldPosition.y < scene.Game.Resolution.y-1 &&
                   this.WorldPosition.x>1 && this.WorldPosition.y>1;
        }
    }
}
