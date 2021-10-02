using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Xunit;

namespace Logging
{
    public class BaseAbstractions
    {
        [Fact]
        public void Should_log_the_Service_Execute_line()
        {
            // Arrange
            var lines = new List<string>();
            var args = new string[0];
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddAssertableLogger(lines);
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IService, Service>();
                })
                .Build();
            var service = host.Services.GetRequiredService<IService>();

            // Act
            service.Execute();

            // Assert
            Assert.Collection(lines,
                line => Assert.Equal("Service.Execute()", line)
            );
        }

        public interface IService
        {
            void Execute();
        }

        public class Service : IService
        {
            private readonly ILogger _logger;
            public Service(ILogger<Service> logger)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public void Execute()
            {
                _logger.LogInformation("Service.Execute()");
            }
        }
    }
}
