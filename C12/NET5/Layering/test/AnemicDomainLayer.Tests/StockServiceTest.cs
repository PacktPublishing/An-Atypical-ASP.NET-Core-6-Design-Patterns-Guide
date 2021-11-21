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
                var quantityInStock = sut.AddStock(1, 9);

                // Assert
                Assert.Equal(10, quantityInStock);
                var actual = assertContext.Products.Find(1);
                Assert.Equal(10, actual.QuantityInStock);
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

                arrangeContext.Products.Add(new() { Name = "Product 1", QuantityInStock = 10 });
                arrangeContext.SaveChanges();

                var sut = new StockService(actContext);

                // Act
                var quantityInStock = sut.RemoveStock(1, 5);

                // Assert
                Assert.Equal(5, quantityInStock);
                var actual = assertContext.Products.Find(1);
                Assert.Equal(5, actual.QuantityInStock);
            }

            [Fact]
            public void Should_throw_a_NotEnoughStockException_when_the_specified_amount_of_items_to_remove_is_greater_than_QuantityInStock()
            {
                // Arrange
                using var arrangeContext = new ProductContext(_builder.Options);
                using var actContext = new ProductContext(_builder.Options);
                using var assertContext = new ProductContext(_builder.Options);

                arrangeContext.Products.Add(new() { Name = "Product 1", QuantityInStock = 10 });
                arrangeContext.SaveChanges();

                var sut = new StockService(actContext);

                // Act & Assert
                var stockException = Assert.Throws<NotEnoughStockException>(
                    () => sut.RemoveStock(1, 11)
                );
                Assert.Equal(10, stockException.QuantityInStock);
                Assert.Equal(11, stockException.AmountToRemove);
            }
        }
    }
}
