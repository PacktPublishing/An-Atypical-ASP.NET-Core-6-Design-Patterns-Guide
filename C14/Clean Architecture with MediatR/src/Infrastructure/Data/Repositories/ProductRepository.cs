using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public async Task<IEnumerable<Product>> AllAsync(CancellationToken cancellationToken)
        {
            var products = await _db.Products.ToArrayAsync(cancellationToken);
            return products;
        }

        public async Task DeleteByIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FindAsync(productId, cancellationToken);
            _db.Products.Remove(product);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<Product> FindByIdAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _db.Products.FindAsync(productId, cancellationToken);
            return product;
        }

        public async Task InsertAsync(Product product, CancellationToken cancellationToken)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
