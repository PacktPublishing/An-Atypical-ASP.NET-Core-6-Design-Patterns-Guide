using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AnemicDomainLayer
{
    public class ProductServiceTest
    {
        public class All : ProductServiceTest
        {
            [Fact]
            public void Should_return_all_products()
            {
                // Arrange
                var builder = new DbContextOptionsBuilder<ProductContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                ;
                using var arrangeContext = new ProductContext(builder.Options);
                using var actContext = new ProductContext(builder.Options);

                arrangeContext.Products.Add(new() { Name = "Product 1" });
                arrangeContext.Products.Add(new() { Name = "Product 2" });
                arrangeContext.SaveChanges();

                var sut = new ProductService(actContext);

                // Act
                var actual = sut.All();

                // Assert
                Assert.Collection(actual,
                    product => Assert.Equal("Product 1", product.Name),
                    product => Assert.Equal("Product 2", product.Name)
                );
            }
        }
    }
}
