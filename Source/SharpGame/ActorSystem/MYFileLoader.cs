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
        public static void ToJson (object obj)
        {
            if (obj.GetType() == typeof(Scene))
                SceneToJson(obj);
            else if (obj.GetType() == typeof(Actor))
                ActorToJson(obj);
            else if (obj.GetType() == typeof(ActorComponent))
                ComponentToJson(obj);
        }

       
        private static void SceneToJson(object obj)
        {
            Scene scene = (Scene) obj;
            Predicate<Actor> pre = ok;
            List <Actor> lst = scene.FindAllActors(pre);
            Console.Write("{");
            Console.Write("\"__actors\":[");
            foreach (Actor act in lst)
                ActorToJson(obj);
            Console.Write("]\n}");

        }

        private static void ActorToJson(object obj)
        {
            Actor actor = (Actor) obj;
            List<Actor> chld = actor.FindAllChildren(ok);
            foreach (Actor act in chld)
                ActorToJson(act);
            List<ActorComponent> cmplst = actor.GetAllComponents<ActorComponent>();
            foreach (ActorComponent cmp in cmplst)
                ComponentToJson(cmp);
            Console.Write("{");
            Console.Write("\"name\":" + '"' + actor.Name + "\",\n");
        }

        private static void ComponentToJson(object obj)
        {
            ActorComponent component = (ActorComponent)obj;
            shotatam;
        }


        private static bool ok(Actor ok)
        {
            return true;
        }
        public static string ToJsonString(object obj)
        {
            StringBuilder ex = new StringBuilder("{");
            foreach (var inf in obj.GetType().GetFields
                    (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
            {
                ex.AppendFormat(" \"{0}\": \"{1}\",", inf.Name, inf.GetValue(obj));
            }
            ex.Length -= 1;
            ex.Append("}");
            return ex.ToString();
        }
    }
}
