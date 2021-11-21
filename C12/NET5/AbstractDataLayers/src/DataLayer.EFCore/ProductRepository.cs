using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.EFCore
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _db;
        public ProductRepository(ProductContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public IEnumerable<Product> All()
        {
            return _db.Products;
        }

        public void DeleteById(int productId)
        {
            var product = _db.Products.Find(productId);
            _db.Products.Remove(product);
            _db.SaveChanges();
        }

        public Product FindById(int productId)
        {
            var product = _db.Products.Find(productId);
            return product;
        }

        public void Insert(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            //var data = _db.Products.Find(product.Id);
            //data.Name = product.Name;
            //data.QuantityInStock = product.QuantityInStock;
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}
