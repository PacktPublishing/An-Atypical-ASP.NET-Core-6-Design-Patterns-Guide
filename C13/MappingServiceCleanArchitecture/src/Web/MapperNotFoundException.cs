using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web
{
    public class MapperNotFoundException : Exception
    {
        public MapperNotFoundException(Type source, Type destination)
            : base($"No Mapper from '{source}' to '{destination}' was found.")
        {
        }
    }
}
