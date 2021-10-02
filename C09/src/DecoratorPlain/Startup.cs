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

namespace DecoratorPlain
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<Client>()
                //.AddSingleton<IComponent, ComponentA>()
                //.AddSingleton<IComponent>(serviceProvider => new DecoratorA(new ComponentA()))
                .AddSingleton<IComponent>(serviceProvider => new DecoratorB(new DecoratorA(new ComponentA())))
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
