using System;
using System.Collections.Generic;
using GameFramework.Internal;

namespace GameFramework
{
    public class Actor : GameEntityContainer<Actor>
    {
        private Scene scene;

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

        public Actor Parent { get; set; }

        public string Name { get; set; }

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

        private ComponentContainer Components;

        public Actor(string name = "Actor")
        {
            this.Name = name;
            Components = new ComponentContainer(this);
        }

        public override void AddChild(Actor actor)
        {
            actor.Parent = this;
            actor.Scene = Scene;

            base.AddChild(actor);
        }

        public List<Actor> GetAllChildren()
        {
            return new List<Actor>(children);
        }

        public override void Start()
        {
            Components.Start();

            base.Start();
        }

        public override void Update(float deltaTime)
        {
            Components.Update(deltaTime);

            base.Update(deltaTime);
        }

        public override void Draw(float deltaTime)
        {
            Components.Draw(deltaTime);

            base.Draw(deltaTime);
        }

        public override void OnDestroy()
        {
            Components.OnDestroy();
            Components = null;

            base.OnDestroy();
        }

        public void AddComponent(ActorComponent component)
        {
            Components.AddChild(component);
        }

        public WeakReference<TComponent> GetComponent<TComponent>() where TComponent : class
        {
            return Components.Get<TComponent>();
        }

        public List<WeakReference<TComponent>> GetAllComponents<TComponent>() where TComponent : class
        {
            return Components.GetAll<TComponent>();
        }
        public void OnColide(Actor Exciter)
        {
            this.Components.OnCollide(Exciter);
            foreach (Actor Child in this.children)
                Child.Components.OnCollide(Exciter);
        }

    }
}
