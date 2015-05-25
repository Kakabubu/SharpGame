using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SharpGame
{
    public class SceneLoader : IResourceLoader
    {
        public object LoadResource(string path)
        {
            if (!File.Exists(path))
                return null;

            Scene scene = new Scene();

            JObject contents = JObject.Parse(File.ReadAllText(path));
            var actorsQuery = contents["__actors"].Select(DeserializeActor);
            actorsQuery.ToList().ForEach(scene.AddActor);

            return scene;
        }

        private Actor DeserializeActor(JToken jactor)
        {
            Actor actor = jactor.ToObject<Actor>();

            var componentsQuery = jactor["__components"].Select(DeserializeComponent);
            componentsQuery.ToList().ForEach(actor.AddComponent);

            var childrenQuery = jactor["__children"].Select(DeserializeActor);
            childrenQuery.ToList().ForEach(actor.AddChild);

            return actor;
        }

        private ActorComponent DeserializeComponent(JToken jcomponent)
        {
            Type componentType = Type.GetType((string)jcomponent["__type"]);
            return jcomponent.ToObject(componentType) as ActorComponent;
        }
    }
}
