using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.UseCases;
using ForEvolve.DependencyInjection;
using ForEvolve.EntityFrameworkCore.Seeders;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.Mappers;
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
using Web.Controllers;
using Web.Mappers;

namespace Web
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
            services.AddSingleton<IMapper<Core.Entities.Product, StocksController.StockLevel>, StockMapper>();
            services.AddSingleton<IMapper<Core.Entities.Product, ProductsController.ProductDetails>, Mappers.ProductMapper>();
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
            services.AddScoped<AddStocks>();
            services.AddScoped<RemoveStocks>();
        }
    }

    public class DataLayerModule : DependencyInjectionModule
    {
        public DataLayerModule(IServiceCollection services)
            : base(services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddSingleton<IMapper<Infrastructure.Data.Models.Product, Core.Entities.Product>, Infrastructure.Data.Mappers.ProductMapper>();
            services.AddSingleton<IMapper<Core.Entities.Product, Infrastructure.Data.Models.Product>, Infrastructure.Data.Mappers.ProductMapper>();

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
}
