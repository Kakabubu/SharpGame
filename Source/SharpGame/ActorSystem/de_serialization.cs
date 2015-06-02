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
            JToken Jactor = JT;

            string draft = (string)JT["__draft"];
            if (draft != null)
            {
                var obj = JObject.Parse(File.ReadAllText(draft));
                obj.Merge(JT);
                Jactor = obj;
            }
            Actor act = JT.ToObject<Actor>();
            foreach (var cmp in Jactor["__components"])
                act.AddComponent(Component(cmp));
            foreach (var ch in Jactor["__children"])
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
            var ID = 1;
            foreach (var act in scene.Actors)
            {
                JObject Jact = new JObject();
                Jact.Add("__ID",ID.ToString());
                Jact.Merge(ToJson(act, ID.ToString()));
                ID++;
                arr.Add(Jact);
            }
            var Jscene = JObject.FromObject(scene);
            Jscene.Add("__actors",arr);
            return Jscene;
        }

        public static JObject ToJson(Actor actor,string P_ID)
        {
            var Jactor = JObject.FromObject(actor);
            
            if (actor.Components.Count > 0)
            {
                var arr = new JArray();
                var ID = 1;
                foreach (var cmp in actor.Components)
                {
                    JObject Jcmp = new JObject();
                    Jcmp.Add("__ID", P_ID+":"+ID.ToString());
                    ID++;
                    Jcmp.Merge(ToJson(cmp,P_ID));
                    arr.Add(Jcmp);
                }
                Jactor.Add("__components",arr);
            }
            if (actor.Children.Count > 0)
            {
                var arr = new JArray();
                var ID = 1;
                foreach (var act in actor.Children)
                {
                    JObject Jact = new JObject();
                    Jact.Add("__ID",P_ID+ "."+ID.ToString());
                    ID++;
                    Jact.Merge(ToJson(act,P_ID));
                    arr.Add(Jact);
                }
                Jactor.Add("__children",arr);
            }
            return Jactor;
        }

        public static JObject ToJson(ActorComponent cmp, string P_ID)
        {
            var Jcmp = new JObject();
            Jcmp.Add("__type",cmp.GetType().FullName);
            Jcmp.Merge(JObject.FromObject(cmp));
            return Jcmp;
        }
#endregion
    }
}
