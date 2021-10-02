using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CommonScenarios
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MyOptions>("Options1", _configuration.GetSection("options1"));
            services.Configure<MyOptions>("Options2", _configuration.GetSection("options2"));
            services.Configure<MyDoubleNameOptions>(_configuration.GetSection("myDoubleNameOptions"));
            services.AddTransient<MyNameServiceUsingDoubleNameOptions>();
            services.AddTransient<MyNameServiceUsingNamedOptionsFactory>();
            services.AddTransient<MyNameServiceUsingNamedOptionsMonitor>();
            services.AddTransient<MyNameServiceUsingNamedOptionsSnapshot>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var count = 0;
                    var baseUri = $"{context.Request.Scheme}://{context.Request.Host}";
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("[");
                    await WriteUriAsync("Options 1", $"{baseUri}/options/true");
                    await WriteUriAsync("Options 2", $"{baseUri}/options/false");
                    await WriteUriAsync("Options 1", $"{baseUri}/factory/true");
                    await WriteUriAsync("Options 2", $"{baseUri}/factory/false");
                    await WriteUriAsync("Options 1", $"{baseUri}/monitor/true");
                    await WriteUriAsync("Options 2", $"{baseUri}/monitor/false");
                    await WriteUriAsync("Options 1", $"{baseUri}/snapshot/true");
                    await WriteUriAsync("Options 2", $"{baseUri}/snapshot/false");
                    await context.Response.WriteAsync("]");

                    async Task WriteUriAsync(string expectedName, string uri)
                    {
                        if (count++ > 0)
                        {
                            await context.Response.WriteAsync(",");
                        }
                        await context.Response.WriteAsync("{\"expecting\":\"");
                        await context.Response.WriteAsync(expectedName);
                        await context.Response.WriteAsync("\",\"uri\":\"");
                        await context.Response.WriteAsync(uri);
                        await context.Response.WriteAsync("\"}");
                    }
                });
                endpoints.MapGet("/{serviceName}/{someCondition?}", async context =>
                {
                    var serviceName = (string)context.Request.RouteValues["serviceName"];
                    if (bool.TryParse((string)context.Request.RouteValues["someCondition"], out var someCondition))
                    {
                        var myNameService = GetMyNameService(serviceName);
                        var name = myNameService.GetName(someCondition);

                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync("{");
                        await context.Response.WriteAsync("\"name\":");
                        await context.Response.WriteAsync($"\"{name}\"");
                        await context.Response.WriteAsync("}");
                    }

                    IMyNameService GetMyNameService(string serviceName) => serviceName switch
                    {
                        "factory" => context.RequestServices
                            .GetRequiredService<MyNameServiceUsingNamedOptionsFactory>(),
                        "monitor" => context.RequestServices
                            .GetRequiredService<MyNameServiceUsingNamedOptionsMonitor>(),
                        "snapshot" => context.RequestServices
                            .GetRequiredService<MyNameServiceUsingNamedOptionsSnapshot>(),
                        "options" => context.RequestServices
                            .GetRequiredService<MyNameServiceUsingDoubleNameOptions>(),
                        _ => throw new NotSupportedException($"The service named '{serviceName}' is not supported."),
                    };
                });
            });
        }
    }
}
