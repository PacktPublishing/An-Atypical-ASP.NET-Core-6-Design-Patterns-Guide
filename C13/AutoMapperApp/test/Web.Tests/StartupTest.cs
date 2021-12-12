using AutoMapper;
using Infrastructure.Data.EF;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using Xunit;

namespace Web;

public class StartupTest
{
    [Fact]
    public async Task The_products_endpoint_should_be_reachable()
    {
        // Arrange
        await using var application = new AutoMapperAppWebApplication(databaseName: nameof(The_products_endpoint_should_be_reachable));
        using var client = application.CreateClient();

        // Act
        using var response = await client.GetAsync("/products");

        // Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AutoMapper_configuration_is_valid()
    {
        // Arrange
        await using var application = new AutoMapperAppWebApplication(databaseName: nameof(AutoMapper_configuration_is_valid));
        var mapper = application.Services.GetRequiredService<IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}

internal class AutoMapperAppWebApplication : WebApplicationFactory<Program>
{
    private readonly string _databaseName;
    public AutoMapperAppWebApplication(string databaseName)
    {
        _databaseName = databaseName;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Override the default ProductContext options to make
            // a different InMemory database per test case so there is no
            // seed conflicts.
            services.AddScoped(sp =>
            {
                return new DbContextOptionsBuilder<ProductContext>()
                    .UseInMemoryDatabase(_databaseName)
                    .UseApplicationServiceProvider(sp)
                    .Options;
            });
        });
        return base.CreateHost(builder);
    }
}
