using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGame
{
    public class Resources
    {
        private Dictionary<string, IResourceLoader> loaders;

        public Resources()
        {
            loaders = new Dictionary<string, IResourceLoader>();
        }

        public void RegisterLoader(string extension, IResourceLoader loader)
        {
            loaders[extension] = loader;
        }

        public void UnregisterLoader(string extension)
        {
            loaders.Remove(extension);
        }

        public object Load(string path)
        {
            string extension = Path.GetExtension(path);
            IResourceLoader loader;
            if (loaders.TryGetValue(extension, out loader))
            {
                return loader.LoadResource(path);
            }

            return null;
        }

        public T Load<T>(string path)
        {
            return (T)Load(path);
        }
    }
}
