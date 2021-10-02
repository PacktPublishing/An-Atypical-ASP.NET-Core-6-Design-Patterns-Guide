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
        private readonly IMappingService _mappingService;
        public ProductRepository(ProductContext db, IMappingService mappingService)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mappingService = mappingService ?? throw new ArgumentNullException(nameof(mappingService));
        }

        public IEnumerable<Product> All()
        {
            return _db.Products.Select(p => _mappingService.Map<Models.Product, Product>(p));
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
            return _mappingService.Map<Models.Product, Product>(product);
        }

        public void Insert(Product product)
        {
            var data = _mappingService.Map<Product, Models.Product>(product);
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
