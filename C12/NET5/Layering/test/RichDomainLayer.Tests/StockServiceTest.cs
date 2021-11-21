using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using Xunit;

namespace RichDomainLayer
{
    public class StockServiceTest
    {
        private readonly DbContextOptionsBuilder _builder = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        ;

        public class AddStock : StockServiceTest
        {
            [Fact]
            public void Should_add_the_specified_amount_to_QuantityInStock()
            {
                // Arrange
                using var arrangeContext = new ProductContext(_builder.Options);
                using var actContext = new ProductContext(_builder.Options);
                using var assertContext = new ProductContext(_builder.Options);

                arrangeContext.Products.Add(new() { Name = "Product 1", QuantityInStock = 1 });
                arrangeContext.SaveChanges();

                var sut = new StockService(actContext);

                // Act
                var quantityInStock = sut.AddStock(productId: 1, amount: 2);

                // Assert
                Assert.Equal(3, quantityInStock);
                var actual = assertContext.Products.Find(1);
                Assert.Equal(3, actual.QuantityInStock);
            }
        }

        public class RemoveStock : StockServiceTest
        {
            [Fact]
            public void Should_remove_the_specified_amount_to_QuantityInStock()
            {
                // Arrange
                using var arrangeContext = new ProductContext(_builder.Options);
                using var actContext = new ProductContext(_builder.Options);
                using var assertContext = new ProductContext(_builder.Options);

                arrangeContext.Products.Add(new() { Name = "Product 1", QuantityInStock = 5 });
                arrangeContext.SaveChanges();

                var sut = new StockService(actContext);

                // Act
                var quantityInStock = sut.RemoveStock(productId: 1, amount: 2);

                // Assert
                Assert.Equal(3, quantityInStock);
                var actual = assertContext.Products.Find(1);
                Assert.Equal(3, actual.QuantityInStock);
            }
        }
    }
}
