using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DecoratorScrutor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<Client>()
                .AddSingleton<IComponent, ComponentA>()
                .Decorate<IComponent, DecoratorA>()
                .Decorate<IComponent, DecoratorB>()
                ;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Client client)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) => await client.ExecuteAsync(context));
        }
    }
}
