using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichDomainLayer
{
    public interface IProductService
    {
        IEnumerable<Product> All();
    }
}
