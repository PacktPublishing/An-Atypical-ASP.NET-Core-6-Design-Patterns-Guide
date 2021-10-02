using AutoMapper;
using FluentValidation.AspNetCore;
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

namespace VerticalApp.Features.Stocks
{
    public class StocksTest : BaseIntegrationTest
    {
        public StocksTest()
            : base(databaseName: "StocksTest") { }

        protected async override Task SeedAsync(ProductContext db)
        {
            await db.Products.AddAsync(new Product
            {
                Id = 4,
                Name = "Ghost Pepper",
                QuantityInStock = 10
            });
            await db.Products.AddAsync(new Product
            {
                Id = 5,
                Name = "Carolina Reaper",
                QuantityInStock = 10
            });
            await db.SaveChangesAsync();
        }

        public class AddStocksTest : StocksTest
        {
            private const int _productId = 4;

            [Fact]
            public async Task Should_increment_QuantityInStock_by_the_specified_amount()
            {
                // Arrange
                var serviceProvider = _services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                // Act
                var result = await mediator.Send(new AddStocks.Command
                {
                    ProductId = _productId,
                    Amount = 10
                });

                // Assert
                using var assertScope = serviceProvider.CreateScope();
                var db = assertScope.ServiceProvider.GetRequiredService<ProductContext>();
                var peppers = await db.Products.FindAsync(_productId);
                Assert.Equal(20, peppers.QuantityInStock);
            }
        }

        public class RemoveStocksTest : StocksTest
        {
            private const int _productId = 5;

            [Fact]
            public async Task Should_decrement_QuantityInStock_by_the_specified_amount()
            {
                // Arrange
                var serviceProvider = _services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                // Act
                var result = await mediator.Send(new RemoveStocks.Command
                {
                    ProductId = _productId,
                    Amount = 10
                });

                // Assert
                using var assertScope = serviceProvider.CreateScope();
                var db = assertScope.ServiceProvider.GetRequiredService<ProductContext>();
                var peppers = await db.Products.FindAsync(_productId);
                Assert.Equal(0, peppers.QuantityInStock);
            }

            [Fact]
            public async Task Should_throw_a_NotEnoughStockException_when_the_resulting_QuantityInStock_would_be_less_than_zero()
            {
                // Arrange
                using var scope = _services.BuildServiceProvider().CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                // Act & Assert
                await Assert.ThrowsAsync<NotEnoughStockException>(() => mediator.Send(new RemoveStocks.Command
                {
                    ProductId = _productId,
                    Amount = 11
                }));
            }
        }
    }
}
