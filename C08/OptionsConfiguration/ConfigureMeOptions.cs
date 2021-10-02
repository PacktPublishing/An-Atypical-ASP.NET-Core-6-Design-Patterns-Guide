using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationAndValidation
{
    public class ConfigureMeOptions
    {
        public string Title { get; set; }
        public IEnumerable<string> Lines { get; set; }
    }
}
