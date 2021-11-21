using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnemicDomainLayer
{
    public class StockService : IStockService
    {
        private readonly ProductContext _db;
        public StockService(ProductContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public int AddStock(int productId, int amount)
        {
            var product = _db.Products.Find(productId);
            product.QuantityInStock += amount;
            _db.SaveChanges();

            return product.QuantityInStock;
        }

        public int RemoveStock(int productId, int amount)
        {
            var product = _db.Products.Find(productId);
            if (amount > product.QuantityInStock)
            {
                throw new NotEnoughStockException(product.QuantityInStock, amount);
            }
            product.QuantityInStock -= amount;
            _db.SaveChanges();

            return product.QuantityInStock;
        }
    }
}
