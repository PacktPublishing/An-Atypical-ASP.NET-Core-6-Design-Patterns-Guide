using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ConfigurationAndValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace OptionsConfiguration
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
            services.Configure<ConfigureMeOptions>(_configuration.GetSection("configureMe"));
            services.AddSingleton<IConfigureOptions<ConfigureMeOptions>, Configure1ConfigureMeOptions>();
            services.AddSingleton<IConfigureOptions<ConfigureMeOptions>, Configure2ConfigureMeOptions>();
            services.Configure<ConfigureMeOptions>(options =>
            {
                options.Lines = options.Lines.Append("Another Configure call");
            });
            services.PostConfigure<ConfigureMeOptions>(options =>
            {
                options.Lines = options.Lines.Append("What about PostConfigure?");
            });
            services.PostConfigureAll<ConfigureMeOptions>(options =>
            {
                options.Lines = options.Lines.Append("Did you forgot about PostConfigureAll?");
            });
            services.ConfigureAll<ConfigureMeOptions>(options =>
            {
                options.Lines = options.Lines.Append("Or ConfigureAll?");
            });
            services.AddOptions<ConfigureMeOptions>().Validate(options =>
            {
                options.Lines = options.Lines.Append("Validate was not intended for this, but this is a trace isn't it?");
                return true;
            });
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
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                endpoints.MapGet("/", async context =>
                {
                    var count = 0;
                    var baseUri = $"{context.Request.Scheme}://{context.Request.Host}";
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("[");
                    await WriteUriAsync("/configure-me");
                    await context.Response.WriteAsync("]");

                    async Task WriteUriAsync(string uri)
                    {
                        if (count++ > 0)
                        {
                            await context.Response.WriteAsync(",");
                        }
                        await context.Response.WriteAsync($"\"{baseUri}{uri}\"");
                    }
                });
                endpoints.MapGet("/configure-me", async context =>
                {
                    var options = context.RequestServices
                        .GetRequiredService<IOptionsMonitor<ConfigureMeOptions>>();
                    var json = JsonSerializer.Serialize(options, jsonOptions);

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
                });
            });
        }
    }

    public class Configure1ConfigureMeOptions : IConfigureOptions<ConfigureMeOptions>
    {
        public void Configure(ConfigureMeOptions options)
        {
            options.Lines = options.Lines.Append("Added line 1!");
        }
    }

    public class Configure2ConfigureMeOptions : IConfigureOptions<ConfigureMeOptions>
    {
        public void Configure(ConfigureMeOptions options)
        {
            options.Lines = options.Lines.Append("Added line 2!");
        }
    }
}
