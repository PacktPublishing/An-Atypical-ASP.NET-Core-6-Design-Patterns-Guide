using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NinjaOCP
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                // Setup the response
                context.Response.ContentType = "text/html";

                // Create actors
                var target = new Ninja("The Unseen Mirage");
                var ninja = new Ninja("The Blue Phantom");

                // First attack (Sword)
                ninja.EquippedWeapon = new Sword();
                var result = ninja.Attack(target);
                await PrintAttackResult(result);

                // Second attack (Shuriken)
                ninja.EquippedWeapon = new Shuriken();
                var result2 = ninja.Attack(target);
                await PrintAttackResult(result2);

                // Write the outcome of an AttackResult to response stream
                async Task PrintAttackResult(AttackResult attackResult)
                {
                    await context.Response.WriteAsync($"'{attackResult.Attacker}' attacked '{attackResult.Target}' using '{attackResult.Weapon}'!<br>");
                }
            });
        }
    }
}
