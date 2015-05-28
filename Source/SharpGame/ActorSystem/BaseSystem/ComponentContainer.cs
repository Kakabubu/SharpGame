using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SharpGame.Internal
{
    internal class ComponentContainer : GameEntityContainer<ActorComponent>
    {
        [JsonIgnore]
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

        public TComponent GetComponent<TComponent>() where TComponent : class
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is TComponent)
                    return children[i] as TComponent;
            }

            return null;
        }
        public List<ActorComponent> GetAllComponents()
        {
            var foundComponents = new List<ActorComponent>();

            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is TComponent)
                    foundComponents.Add(children[i] as TComponent);
            }

            return foundComponents;
        }
        public List<TComponent> GetAllComponents<TComponent>() where TComponent : class
        {
            var foundComponents = new List<TComponent>();

            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is TComponent)
                    foundComponents.Add(children[i] as TComponent);
            }

            return foundComponents;
        }
        public void OnCollide (Actor Exciter)
        {
            foreach (ActorComponent cmp in children)
                cmp.OnCollide(Exciter);
        }
    }
}
