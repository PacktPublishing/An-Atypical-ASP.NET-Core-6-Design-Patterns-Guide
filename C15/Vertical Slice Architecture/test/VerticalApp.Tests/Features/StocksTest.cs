using MediatR;
using Microsoft.Extensions.DependencyInjection;
using VerticalApp.Data;
using VerticalApp.Models;
using Xunit;

namespace VerticalApp.Features.Stocks;

public class StocksTest
{
    private static async Task SeederDelegate(ProductContext db)
    {
        db.Products.RemoveRange(db.Products.ToArray());
        await db.Products.AddAsync(new Product(
            id: 4,
            name: "Ghost Pepper",
            quantityInStock: 10
        ));
        await db.Products.AddAsync(new Product(
            id: 5,
            name: "Carolina Reaper",
            quantityInStock: 10
        ));
        await db.SaveChangesAsync();
    }

    public class AddStocksTest : StocksTest
    {
        [Fact]
        public async Task Should_increment_QuantityInStock_by_the_specified_amount()
        {
            // Arrange
            await using var application = new VerticalAppApplication(databaseName: nameof(Should_increment_QuantityInStock_by_the_specified_amount));
            await application.SeedAsync(SeederDelegate);
            using var requestScope = application.Services.CreateScope();
            var mediator = requestScope.ServiceProvider.GetRequiredService<IMediator>();

            // Act
            var result = await mediator.Send(new AddStocks.Command
            {
                ProductId = 4,
                Amount = 10
            });

            // Assert
            using var assertScope = application.Services.CreateScope();
            var db = assertScope.ServiceProvider.GetRequiredService<ProductContext>();
            var peppers = await db.Products.FindAsync(4);
            Assert.NotNull(peppers);
            Assert.Equal(20, peppers!.QuantityInStock);
        }
    }

    public class RemoveStocksTest : StocksTest
    {
        [Fact]
        public async Task Should_decrement_QuantityInStock_by_the_specified_amount()
        {
            // Arrange
            await using var application = new VerticalAppApplication(databaseName: nameof(Should_decrement_QuantityInStock_by_the_specified_amount));
            await application.SeedAsync(SeederDelegate);
            using var requestScope = application.Services.CreateScope();
            var mediator = requestScope.ServiceProvider.GetRequiredService<IMediator>();

            // Act
            var result = await mediator.Send(new RemoveStocks.Command
            {
                ProductId = 5,
                Amount = 10
            });

            // Assert
            using var assertScope = application.Services.CreateScope();
            var db = assertScope.ServiceProvider.GetRequiredService<ProductContext>();
            var peppers = await db.Products.FindAsync(5);
            Assert.NotNull(peppers);
            Assert.Equal(0, peppers!.QuantityInStock);
        }

        [Fact]
        public async Task Should_throw_a_NotEnoughStockException_when_the_resulting_QuantityInStock_would_be_less_than_zero()
        {
            // Arrange
            await using var application = new VerticalAppApplication(databaseName: nameof(Should_throw_a_NotEnoughStockException_when_the_resulting_QuantityInStock_would_be_less_than_zero));
            await application.SeedAsync(SeederDelegate);
            using var requestScope = application.Services.CreateScope();
            var mediator = requestScope.ServiceProvider.GetRequiredService<IMediator>();

            // Act & Assert
            await Assert.ThrowsAsync<NotEnoughStockException>(() => mediator.Send(new RemoveStocks.Command
            {
                ProductId = 5,
                Amount = 11
            }));
        }
    }
}
