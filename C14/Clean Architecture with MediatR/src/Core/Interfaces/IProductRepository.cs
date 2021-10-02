using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> AllAsync(CancellationToken cancellationToken);
        Task<Product> FindByIdAsync(int productId, CancellationToken cancellationToken);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);
        Task InsertAsync(Product product, CancellationToken cancellationToken);
        Task DeleteByIdAsync(int productId, CancellationToken cancellationToken);
    }
}
