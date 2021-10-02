using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OpaqueFacadeSubSystem.Abstractions;
using TransparentFacadeSubSystem.Abstractions;

namespace Facade
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddRouting()
                .AddOpaqueFacadeSubSystem()
                .AddTransparentFacadeSubSystem()
                .AddSingleton<IComponentB, UpdatedComponentB>()
                ;
        }

        public void Configure(IApplicationBuilder app, IOpaqueFacade opaqueFacade, ITransparentFacade transparentFacade)
        {
            app.UseRouter(routeBuilder =>
            {
                routeBuilder.MapGet("/opaque/a", async context =>
                {
                    var result = opaqueFacade.ExecuteOperationA();
                    await context.Response.WriteAsync(result);
                });
                routeBuilder.MapGet("/opaque/b", async context =>
                {
                    var result = opaqueFacade.ExecuteOperationB();
                    await context.Response.WriteAsync(result);
                });
                routeBuilder.MapGet("/transparent/a", async context =>
                {
                    var result = transparentFacade.ExecuteOperationA();
                    await context.Response.WriteAsync(result);
                });
                routeBuilder.MapGet("/transparent/b", async context =>
                {
                    var result = transparentFacade.ExecuteOperationB();
                    await context.Response.WriteAsync(result);
                });
            });
        }
    }
}
