using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TemplateMethod
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SearchMachine>(x => new LinearSearchMachine(1, 10, 5, 2, 123, 333, 4));
            services.AddSingleton<SearchMachine>(x => new BinarySearchMachine(1, 2, 3, 4, 5, 6, 7, 8, 9, 10));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEnumerable<SearchMachine> searchMachines)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                var elementsToFind = new int[] { 1, 10, 11 };
                await context.WriteLineAsync("<pre>");
                foreach (var searchMachine in searchMachines)
                {
                    var heading = $"Current search machine is {searchMachine.GetType().Name}";
                    await context.WriteLineAsync("".PadRight(heading.Length, '='));
                    await context.WriteLineAsync(heading);
                    foreach (var value in elementsToFind)
                    {
                        var index = searchMachine.IndexOf(value);
                        var wasFound = index.HasValue;
                        if (wasFound)
                        {
                            await context.WriteLineAsync($"The element '{value}' was found at index {index.Value}.");
                        }
                        else
                        {
                            await context.WriteLineAsync($"The element '{value}' was not found.");
                        }
                    }
                }
                await context.WriteLineAsync("</pre>");
            });
        }
    }
}
