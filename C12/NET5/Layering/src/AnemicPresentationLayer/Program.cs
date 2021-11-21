using DataLayer;
using ForEvolve.DependencyInjection;
using ForEvolve.EntityFrameworkCore.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .ScanForDIModules()
    .FromAssemblyOf<Program>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed temp data
app.Seed<ProductContext>();

app.Run();

public class RichDomainLayerModule : DependencyInjectionModule
{
    public RichDomainLayerModule(IServiceCollection services)
        : base(services)
    {
        services.AddScoped<AnemicDomainLayer.IProductService, AnemicDomainLayer.ProductService>();
        services.AddScoped<AnemicDomainLayer.IStockService, AnemicDomainLayer.StockService>();
    }
}

public class DataLayerModule : DependencyInjectionModule
{
    public DataLayerModule(IServiceCollection services)
        : base(services)
    {
        services.AddDbContext<ProductContext>(options => options
            .UseInMemoryDatabase("AnemicProductContextMemoryDB")
            .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        );
        services.AddForEvolveSeeders().Scan<Program>();
    }
}

public class ProductSeeder : ISeeder<ProductContext>
{
    public void Seed(ProductContext db)
    {
        db.Products.Add(new Product
        {
            Id = 1,
            Name = "Banana",
            QuantityInStock = 50
        });
        db.Products.Add(new Product
        {
            Id = 2,
            Name = "Apple",
            QuantityInStock = 20
        });
        db.Products.Add(new Product
        {
            Id = 3,
            Name = "Habanero Pepper",
            QuantityInStock = 10
        });
        db.SaveChanges();
    }
}
