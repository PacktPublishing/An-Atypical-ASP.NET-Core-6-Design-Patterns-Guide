using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UseCases
{
    public class AddStocks
    {
        private readonly IProductRepository _productRepository;
        public AddStocks(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public int Handle(int productId, int amount)
        {
            var product = _productRepository.FindById(productId);
            product.QuantityInStock += amount;
            _productRepository.Update(product);
            return product.QuantityInStock;
        }
    }
}
