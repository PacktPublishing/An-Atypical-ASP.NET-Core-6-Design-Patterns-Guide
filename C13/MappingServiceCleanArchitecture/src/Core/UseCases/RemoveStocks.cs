using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases
{
    public class RemoveStocks
    {
        private readonly IProductRepository _productRepository;
        public RemoveStocks(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public Product Handle(int productId, int amount)
        {
            var product = _productRepository.FindById(productId);
            if (amount > product.QuantityInStock)
            {
                throw new NotEnoughStockException(product.QuantityInStock, amount);
            }
            product.QuantityInStock -= amount;
            _productRepository.Update(product);
            return product;
        }
    }
}
