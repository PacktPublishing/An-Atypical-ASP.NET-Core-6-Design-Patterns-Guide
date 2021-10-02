using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
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
            return _db.Products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                QuantityInStock = p.QuantityInStock
            });
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
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                QuantityInStock = product.QuantityInStock
            };
        }

        public void Insert(Product product)
        {
            var data = new Models.Product
            {
                Id = product.Id,
                Name = product.Name,
                QuantityInStock = product.QuantityInStock
            };
            _db.Products.Add(data);
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            var data = _db.Products.Find(product.Id);
            data.Name = product.Name;
            data.QuantityInStock = product.QuantityInStock;
            _db.SaveChanges();
        }
    }
}
