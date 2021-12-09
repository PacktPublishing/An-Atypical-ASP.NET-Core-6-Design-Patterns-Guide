using Core;
using Core.Interfaces;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    // Core Layer
    .AddScoped<StockService>()

    // Infrastructure Layer (mapping Core to Infrastructure.Data.EF)
    .AddScoped<IProductRepository, ProductRepository>()
    .AddDbContext<ProductContext>(options => options
        .UseInMemoryDatabase("ProductContextMemoryDB")
        .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
    )

    // Web Layer
    .AddSingleton<IMapper<Product, ProductDetails>, ProductMapper>()

    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
;

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/products", async (IProductRepository productRepository, IMapper<Product, ProductDetails> mapper, CancellationToken cancellationToken) =>
{
    var products = await productRepository.AllAsync(cancellationToken);
    return products.Select(p => mapper.Map(p));
}).Produces(200, typeof(ProductDetails[]));

app.MapPost("/products/{productId:int}/add-stocks", async (int productId, AddStocksCommand command, StockService stockService, CancellationToken cancellationToken) =>
{
    try
    {
        var quantityInStock = await stockService.AddStockAsync(productId, command.Amount, cancellationToken);
        var stockLevel = new StockLevel(quantityInStock);
        return Results.Ok(stockLevel);
    }
    catch (ProductNotFoundException ex)
    {
        return Results.NotFound(new
        {
            ex.Message,
            productId,
        });
    }
});

app.MapPost("/products/{productId:int}/remove-stocks", async (int productId, RemoveStocksCommand command, StockService stockService, CancellationToken cancellationToken) =>
{
    try
    {
        var quantityInStock = await stockService.RemoveStockAsync(productId, command.Amount, cancellationToken);
        var stockLevel = new StockLevel(quantityInStock);
        return Results.Ok(stockLevel);
    }
    catch (NotEnoughStockException ex)
    {
        return Results.Conflict(new
        {
            ex.Message,
            ex.AmountToRemove,
            ex.QuantityInStock
        });
    }
    catch (ProductNotFoundException ex)
    {
        return Results.NotFound(new
        {
            ex.Message,
            productId,
        });
    }
});

using (var seedScope = app.Services.CreateScope())
{
    var db = seedScope.ServiceProvider.GetRequiredService<ProductContext>();
    await ProductSeeder.SeedAsync(db);
}
app.Run();

internal class AddStocksCommand
{
    public int Amount { get; set; }
}

internal class RemoveStocksCommand
{
    public int Amount { get; set; }
}

internal class StockLevel
{
    public StockLevel(int quantityInStock)
    {
        QuantityInStock = quantityInStock;
    }

    public int QuantityInStock { get; set; }
}

internal static class ProductSeeder
{
    public static Task SeedAsync(ProductContext db)
    {
        db.Products.Add(new Product(
            id: 1,
            name: "Banana",
            quantityInStock: 50
        ));
        db.Products.Add(new Product(
            id: 2,
            name: "Apple",
            quantityInStock: 20
        ));
        db.Products.Add(new Product(
            id: 3,
            name: "Habanero Pepper",
            quantityInStock: 10
        ));
        return db.SaveChangesAsync();
    }
}

public class ProductDetails
{
    public ProductDetails(int id, string name, int quantityInStock)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        QuantityInStock = quantityInStock;
    }

    public int Id { get; }
    public string Name { get; }
    public int QuantityInStock { get; }
}

public class ProductMapper : IMapper<Product, ProductDetails>
{
    public ProductDetails Map(Product entity)
    {
        return new ProductDetails(
            id: entity.Id ?? default,
            name: entity.Name,
            quantityInStock: entity.QuantityInStock
        );
    }
}