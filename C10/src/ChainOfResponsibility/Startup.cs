using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChainOfResponsibility
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Create the chain of responsibility, 
            // ordered by the most called (or the one that must be executed the faster)
            // to the less called handler (or the one that can take more time to be executed), 
            // followed by the DefaultHandler.
            services.AddSingleton<IMessageHandler>(new AlarmTriggeredHandler(new AlarmPausedHandler(new AlarmStoppedHandler(new DefaultHandler()))));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageHandler messageHandler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var message = new Message
                {
                    Name = context.Request.Query["name"],
                    Payload = context.Request.Query["payload"]
                };
                try
                {
                    // Send the message into the chain of responsibility
                    messageHandler.Handle(message);
                    await context.Response.WriteAsync($"Message '{message.Name}' handled successfully.");
                }
                catch (NotSupportedException ex)
                {
                    await context.Response.WriteAsync(ex.Message);
                }
            });
        }
    }
}
