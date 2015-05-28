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
        public static string ToJson (object obj)
        {
            if (obj.GetType() == typeof(Scene))
                return SceneToJson((Scene)obj);
            else if (obj.GetType() == typeof(Actor))
                return ActorToJson((Actor)obj);
            return null;
        }
       
        private static string SceneToJson(Scene scene)
        {
            StringBuilder sb = new StringBuilder("{\"__actors\":[");
            foreach (Actor act in scene.Actors)
                sb.Append (ActorToJson(act));
            sb.Length -= 1;
            sb.Append("]\n}");
            return sb.ToString();
        }

        private static string ActorToJson(Actor actor)
        {
            StringBuilder sb = new StringBuilder("{\"Name\":");
            sb.AppendFormat("\"{0}\",\n\t", actor.Name);
            if (actor.Children.Count > 0)
            {
                sb.AppendFormat("\n\t\"__Children\":[");
                foreach (Actor act in actor.Children)
                {
                    sb.Append(ActorToJson(act));
                }
                sb.Length -= 1;
            }
            if (actor.Components.Count > 0)
            {
                sb.AppendFormat("\n\t\"__Components\":[");
                foreach (ActorComponent cmp in actor.Components)
                {
                    sb.Append(ComponentToJson(cmp));
                }
                sb.Length -= 1;
                sb.AppendFormat("]\n\t},");
            }
            sb.AppendFormat("}");
            return sb.ToString();
        }

        private static string ComponentToJson(ActorComponent cmp)
        {
            StringBuilder sb = new StringBuilder("\n\t\t{\"__type\":");
            sb.AppendFormat("\"{0}\",", cmp.GetType());
            foreach (var inf in cmp.GetType().GetFields())
            { 
                sb.AppendFormat("\n\t\t\t\"{0}\":\"{1}\",", inf.Name, inf.GetValue(cmp).ToString());
            }
            sb.Length -= 1;
            sb.Append("},");
            return sb.ToString();
        }

        private static void ArrToJson(Array[] arr)
        {
            Console.Write("[");
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i].ToString() + ',');
            Console.CursorLeft -= 1;
            Console.Write("]\n");
        }

        private static void ArrToJson(Array[,] arr)
        {
            Console.Write("[");
            for (int y = 0; y < arr.GetLength(0); y++)
                for (int x = 0; x < arr.GetLength(1); x++)
                    Console.Write(arr[y, x].ToString() + ',');
            Console.CursorLeft -= 1;
            Console.Write("]\n");
        }
        #endregion 
        #region trash
        public static void toJson(object obj)
        {
            File.WriteAllText(PathGenerate(obj), toJsonString(obj));
        }

        public static void toJson(List<object> lst)
        {
            foreach (object obj in lst)
                toJson(obj);
        }

        public static string toJsonString(object obj)
        {
            StringBuilder ex = new StringBuilder("{");
            foreach (var inf in obj.GetType().GetFields
                    (BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                ex.AppendFormat(" \"{0}\": \"{1}\",", inf.Name, inf.GetValue(obj));
            ex.Length -= 1;
            ex.Append("}");
            return ex.ToString();
        }

        public static string PathGenerate(object obj)
        {
            StringBuilder Path = new StringBuilder(@"D:\Anton\Education\Unity 3D C#\Repos\ShrpGm\Source\");
            Path.AppendFormat("{0}{1}", obj.GetType().Name.ToString(), ".txt");
            return Path.ToString();
        }

        #endregion
    }
}
