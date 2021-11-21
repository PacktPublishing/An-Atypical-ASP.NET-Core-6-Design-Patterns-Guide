using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicDomainLayer
{
    public interface IProductService
    {
        IEnumerable<Product> All();
    }
}
