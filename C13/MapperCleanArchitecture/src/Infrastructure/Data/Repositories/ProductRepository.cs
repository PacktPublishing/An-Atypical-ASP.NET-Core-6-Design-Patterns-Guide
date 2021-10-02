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
        private readonly IMapper<Data.Models.Product, Core.Entities.Product> _dataToEntityMapper;
        private readonly IMapper<Core.Entities.Product, Data.Models.Product> _entityToDataMapper;
        public ProductRepository(ProductContext db, IMapper<Data.Models.Product, Core.Entities.Product> productMapper, IMapper<Core.Entities.Product, Data.Models.Product> entityToDataMapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _dataToEntityMapper = productMapper ?? throw new ArgumentNullException(nameof(productMapper));
            _entityToDataMapper = entityToDataMapper ?? throw new ArgumentNullException(nameof(entityToDataMapper));
        }

        public IEnumerable<Core.Entities.Product> All()
        {
            return _db.Products.Select(p => _dataToEntityMapper.Map(p));
        }

        public void DeleteById(int productId)
        {
            var product = _db.Products.Find(productId);
            _db.Products.Remove(product);
            _db.SaveChanges();
        }

        public Core.Entities.Product FindById(int productId)
        {
            var product = _db.Products.Find(productId);
            return _dataToEntityMapper.Map(product);
        }

        public void Insert(Core.Entities.Product product)
        {
            var data = _entityToDataMapper.Map(product);
            _db.Products.Add(data);
            _db.SaveChanges();
        }

        public void Update(Core.Entities.Product product)
        {
            var data = _db.Products.Find(product.Id);
            data.Name = product.Name;
            data.QuantityInStock = product.QuantityInStock;
            _db.SaveChanges();
        }
    }
}
