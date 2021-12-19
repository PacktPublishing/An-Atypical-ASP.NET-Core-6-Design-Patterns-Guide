using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VerticalApp.Data;

namespace VerticalApp
{
    internal class VerticalAppApplication : WebApplicationFactory<Program>
    {
        private readonly string _databaseName;
        public VerticalAppApplication(string databaseName)
        {
            _databaseName = databaseName;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Override the default ProductContext options to make
                // a different InMemory database per test case so there is no
                // seed conflicts.
                services.AddScoped(sp =>
                {
                    return new DbContextOptionsBuilder<ProductContext>()
                        .UseInMemoryDatabase(_databaseName)
                        .UseApplicationServiceProvider(sp)
                        .Options;
                });
            });
            return base.CreateHost(builder);
        }

        public Task SeedAsync(Func<ProductContext, Task> seeder)
        {
            using var seedScope = Services.CreateScope();
            var db = seedScope.ServiceProvider.GetRequiredService<ProductContext>();
            return seeder(db);
        }
    }
}
