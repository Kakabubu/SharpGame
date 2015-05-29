using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpGame
{
    class Load
    {
        #region deserialize
        
        public static Scene Scene(string path)
        {
            Scene scene = new Scene();
            var obj = JObject.Parse(File.ReadAllText(path));
            foreach (var Jactor in obj["__actors"])
            {
                scene.AddActor(Load.Actor(Jactor));
            }
            return scene;
        }

        public static Actor Actor(JToken JT)
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
                act.AddComponent(Component(cmp));
            foreach (var ch in jactor["__children"])
                act.AddChild(Actor(ch));
            return act;
        }

        public static ActorComponent Component(JToken JT)
        {
            Type type = Type.GetType((string)JT["__type"]);
            var Cmp = JT.ToObject(type);
            return Cmp as ActorComponent;
        }
        #endregion

        #region serialize
       
        public static JObject ToJson(Scene scene)
        {
            var arr = new JArray();
            foreach (var act in scene.Actors)
                arr.Add(ToJson(act));
            var Jscene = JObject.FromObject(scene);
            Jscene.Add("__actors",arr);
            return Jscene;
        }

        public static JObject ToJson(Actor actor)
        {
            var Jactor = JObject.FromObject(actor);
            if (actor.Components.Count > 0)
            {
                var arr = new JArray();
                foreach (var cmp in actor.Components)
                    arr.Add(ToJson(cmp));
                Jactor.Add("__components",arr);
            }
            if (actor.Children.Count > 0)
            {
                var arr = new JArray();
                foreach (var act in actor.Children)
                    arr.Add(ToJson(act));
                Jactor.Add("__children",arr);
            }
            return Jactor;
        }

        public static JObject ToJson(ActorComponent cmp)
        {
            var Jcmp = new JObject();
            Jcmp.Add("__type",cmp.GetType().FullName);
            Jcmp.Merge(JObject.FromObject(cmp));
            return Jcmp;
        }
#endregion
    }
}
