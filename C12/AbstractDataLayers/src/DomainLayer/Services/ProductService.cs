using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public IEnumerable<Product> All()
        {
            return _repository.All().Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                QuantityInStock = p.QuantityInStock
            });
        }
    }
}
