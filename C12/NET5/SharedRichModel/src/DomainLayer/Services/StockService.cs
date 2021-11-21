using DataLayer;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Services
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _repository;
        public StockService(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public int AddStock(int productId, int amount)
        {
            var product = _repository.FindById(productId);
            product.AddStock(amount);
            _repository.Update(product);
            return product.QuantityInStock;
        }

        public int RemoveStock(int productId, int amount)
        {
            var product = _repository.FindById(productId);
            product.RemoveStock(amount);
            _repository.Update(product);
            return product.QuantityInStock;
        }
    }
}
