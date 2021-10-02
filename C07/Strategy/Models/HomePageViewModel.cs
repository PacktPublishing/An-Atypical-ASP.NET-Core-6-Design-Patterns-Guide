using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<string> SomeData { get; }
        public HomePageViewModel(IEnumerable<string> someData)
        {
            SomeData = someData ?? throw new ArgumentNullException(nameof(someData));
        }
    }
}
