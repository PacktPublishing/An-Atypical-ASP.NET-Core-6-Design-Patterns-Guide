using Core.Models;

namespace Core.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> AllAsync(CancellationToken cancellationToken);
}
