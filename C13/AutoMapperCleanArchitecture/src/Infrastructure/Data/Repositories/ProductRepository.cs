#define USE_PROJECT_TO
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductRepository(ProductContext db, IMapper mapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<Product> All()
        {
#if USE_PROJECT_TO
            // Transposed to a Select() possibly optimal in some cases; previously known as "Queryable Extensions".
            return _mapper.ProjectTo<Product>(_db.Products);
#else
            // Manual Mapping (query the whole object, then map it; could lead to "over-querying" the database)
            return _db.Products.Select(p => _mapper.Map<Product>(p));
#endif
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
            return _mapper.Map<Product>(product);
        }

        public void Insert(Product product)
        {
            var data = _mapper.Map<Models.Product>(product);
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
