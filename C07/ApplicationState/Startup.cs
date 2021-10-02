// #define USE_MEMORY_CACHE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApplicationState
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
#if USE_MEMORY_CACHE
            services.AddMemoryCache();
            services.AddSingleton<IApplicationState, ApplicationMemoryCache>();
#else
            services.AddSingleton<IApplicationState, ApplicationDictionary>();
#endif
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApplicationState myAppState)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                if (context.Request.Method == "GET")
                {
                    // GET /?key=SomeAppStateKey
                    await HandleGetRequestAsync(myAppState, context);
                }
                else
                {
                    // POST /
                    // Request body: 
                    //   key=SomeAppStateKey&value=Some value
                    // Using Windows PowerShell: 
                    //   Invoke-WebRequest -Method Post -Body "key=SomeAppStateKey&value=Some value" -Uri https://localhost:5001/
                    await HandlePostRequestAsync(myAppState, context);
                }
            });
        }

        private static async Task HandleGetRequestAsync(IApplicationState myAppState, HttpContext context)
        {
            var key = context.Request.Query["key"];
            if (key.Count != 1)
            {
                await context.Response.WriteAsync("You must specify a single 'key' parameter like '?key=SomeAppStateKey'.");
                return;
            }
            var value = myAppState.Get<string>(key.Single());
            await context.Response.WriteAsync($"{key} = {value ?? "null"}");
        }

        private async Task HandlePostRequestAsync(IApplicationState myAppState, HttpContext context)
        {
            var key = context.Request.Form["key"].SingleOrDefault();
            var value = context.Request.Form["value"].SingleOrDefault();
            if (key == null || value == null)
            {
                await context.Response.WriteAsync("You must specify both a 'key' and a 'value'.");
                return;
            }
            myAppState.Set(key, value);
            await context.Response.WriteAsync($"{key} = {value ?? "null"}");
        }
    }
}
