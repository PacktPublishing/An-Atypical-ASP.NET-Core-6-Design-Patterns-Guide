using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerticalApp.Data;
using VerticalApp.Models;
using Xunit;

namespace VerticalApp.Features.Products
{
    public class ProductsTest : BaseIntegrationTest
    {
        public ProductsTest()
            : base(databaseName: "ProductsTest") { }

        protected async override Task SeedAsync(ProductContext db)
        {
            await db.Products.AddAsync(new Product
            {
                Id = 1,
                Name = "Banana",
                QuantityInStock = 50
            });
            await db.Products.AddAsync(new Product
            {
                Id = 2,
                Name = "Scotch Bottle",
                QuantityInStock = 20
            });
            await db.Products.AddAsync(new Product
            {
                Id = 3,
                Name = "Habanero Pepper",
                QuantityInStock = 10
            });
            await db.SaveChangesAsync();
        }

        public class ListAllProductsTest : ProductsTest
        {
            [Fact]
            public async Task Should_return_all_products()
            {
                // Arrange
                var serviceProvider = _services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                // Act
                var result = await mediator.Send(new ListAllProducts.Command());

                // Assert
                using var assertScope = serviceProvider.CreateScope();
                var db = assertScope.ServiceProvider.GetRequiredService<ProductContext>();
                Assert.Collection(result,
                    product => Assert.Equal("Banana", product.Name),
                    product => Assert.Equal("Scotch Bottle", product.Name),
                    product => Assert.Equal("Habanero Pepper", product.Name)
                );
            }
        }
    }
}
