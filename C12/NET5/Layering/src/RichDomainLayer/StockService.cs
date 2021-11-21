using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RichDomainLayer
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
            var data = _db.Products.Find(productId);
            var product = new Product(
                id: data.Id,
                name: data.Name,
                quantityInStock: data.QuantityInStock
            );
            product.AddStock(amount);
            data.QuantityInStock = product.QuantityInStock;
            _db.SaveChanges();
            return product.QuantityInStock;
        }

        public int RemoveStock(int productId, int amount)
        {
            var data = _db.Products.Find(productId);
            var product = new Product(
                id: data.Id,
                name: data.Name,
                quantityInStock: data.QuantityInStock
            );
            product.RemoveStock(amount);
            data.QuantityInStock = product.QuantityInStock;
            _db.SaveChanges();
            return product.QuantityInStock;
        }
    }
}
