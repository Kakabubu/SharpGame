using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGame
{
    public interface IResourceLoader
    {
        object LoadResource(string path);
    }
}
