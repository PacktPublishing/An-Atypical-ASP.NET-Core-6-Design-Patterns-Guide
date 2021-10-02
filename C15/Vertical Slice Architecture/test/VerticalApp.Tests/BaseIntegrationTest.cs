using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerticalApp.Data;
using VerticalApp.Models;
using Xunit;

namespace VerticalApp
{
    public abstract class BaseIntegrationTest : IAsyncLifetime
    {
        protected readonly IServiceCollection _services;
        private readonly string _databaseName;

        public BaseIntegrationTest(string databaseName)
        {
            _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
            var services = _services = new ServiceCollection();

            new Startup().ConfigureServices(services);
            RemoveDbContext<ProductContext>();

            _services.AddDbContext<ProductContext>(options => options
                .UseInMemoryDatabase(databaseName)
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            );
        }

        protected void RemoveDbContext<TDbContext>()
            where TDbContext : DbContext
        {
            var dbContextDescriptor = _services.FirstOrDefault(x => x.ServiceType == typeof(TDbContext));
            var optionsDescriptor = _services.FirstOrDefault(x => x.ServiceType == typeof(DbContextOptions<TDbContext>));
            _services.Remove(dbContextDescriptor);
            _services.Remove(optionsDescriptor);
        }

        private static object _initLock = new object();
        private static ConcurrentDictionary<string, bool> _dbInit = new ConcurrentDictionary<string, bool>();

        protected abstract Task SeedAsync(ProductContext db);

        public async Task InitializeAsync()
        {
            if (_dbInit.ContainsKey(_databaseName))
            {
                return;
            }

            lock (_initLock)
            {
                if (_dbInit.ContainsKey(_databaseName))
                {
                    return;
                }
                _dbInit[_databaseName] = true;
            }

            var db = _services
                .BuildServiceProvider()
                .CreateScope().ServiceProvider
                .GetRequiredService<ProductContext>();
            await SeedAsync(db);
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
