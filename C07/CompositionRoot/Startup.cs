using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace CompositionRoot
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // This could be the composition root
            services.AddSingleton<Dependency2>();
            services.AddSingleton<Dependency3>();
            services.AddDemoFeature();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Dependency3 dependency3)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });

            Assert.NotNull(dependency3.Dependency1);
            Assert.NotNull(dependency3.Dependency2);
        }
    }

    public class Dependency1
    {

    }
    public class Dependency2
    {

    }
    public class Dependency3
    {
        public Dependency3(Dependency1 dependency1, Dependency2 dependency2)
        {
            Dependency1 = dependency1;
            Dependency2 = dependency2;
        }

        public Dependency1 Dependency1 { get; }
        public Dependency2 Dependency2 { get; }
    }
}
