using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFramework.Internal
{
    internal class ComponentContainer : GameEntityContainer<ActorComponent>
    {
        public Actor Actor { get; private set; }

        public ComponentContainer(Actor actor)
        {
            this.Actor = actor;
        }

        public override void AddChild(ActorComponent component)
        {
            component.Actor = Actor;

            base.AddChild(component);
        }

        public WeakReference<TComponent> Get<TComponent>() where TComponent : class
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is TComponent)
                    return new WeakReference<TComponent>(children[i] as TComponent);
            }

            return new WeakReference<TComponent>(null);
        }

        public List<WeakReference<TComponent>> GetAll<TComponent>() where TComponent : class
        {
            List<WeakReference<TComponent>> foundComponents = new List<WeakReference<TComponent>>();

            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is TComponent)
                    foundComponents.Add(new WeakReference<TComponent>(children[i] as TComponent));
            }

            return foundComponents;
        }

    }
}
