#undef RICH_MODEL

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.EFCore;
using DomainLayer;
using DomainLayer.Services;
using ForEvolve.DependencyInjection;
using ForEvolve.EntityFrameworkCore.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SharedModels;

namespace PresentationLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .ScanForDIModules()
                .FromAssemblyOf<Startup>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Seed temp data
            app.Seed<ProductContext>();
        }
    }

    public class DomainLayerModule : DependencyInjectionModule
    {
        public DomainLayerModule(IServiceCollection services)
            : base(services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockService, StockService>();
        }
    }

    public class DataLayerModule : DependencyInjectionModule
    {
        public DataLayerModule(IServiceCollection services)
            : base(services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddDbContext<ProductContext>(options => options
                .UseInMemoryDatabase("ProductContextMemoryDB")
                .ConfigureWarnings(builder => builder.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            );
            services.AddForEvolveSeeders().Scan<Startup>();
        }
    }

    public class ProductSeeder : ISeeder<ProductContext>
    {
        public void Seed(ProductContext db)
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
            db.SaveChanges();
        }
    }
}
