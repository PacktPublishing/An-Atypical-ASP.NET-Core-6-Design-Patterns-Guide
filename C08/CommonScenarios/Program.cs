using CommonScenarios;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MyOptions>("Options1", builder.Configuration.GetSection("options1"));
builder.Services.Configure<MyOptions>("Options2", builder.Configuration.GetSection("options2"));
builder.Services.Configure<MyDoubleNameOptions>(builder.Configuration.GetSection("myDoubleNameOptions"));
builder.Services.AddTransient<MyNameServiceUsingDoubleNameOptions>();
builder.Services.AddTransient<MyNameServiceUsingNamedOptionsFactory>();
builder.Services.AddTransient<MyNameServiceUsingNamedOptionsMonitor>();
builder.Services.AddTransient<MyNameServiceUsingNamedOptionsSnapshot>();

var app = builder.Build();
app.MapGet("/", (HttpContext context) => new[] {
    new { expecting =  "Options 1", uri = $"https://{context.Request.Host}/options/true" },
    new { expecting =  "Options 2", uri = $"https://{context.Request.Host}/options/false" },
    new { expecting =  "Options 1", uri = $"https://{context.Request.Host}/factory/true" },
    new { expecting =  "Options 2", uri = $"https://{context.Request.Host}/factory/false" },
    new { expecting =  "Options 1", uri = $"https://{context.Request.Host}/monitor/true" },
    new { expecting =  "Options 2", uri = $"https://{context.Request.Host}/monitor/false" },
    new { expecting =  "Options 1", uri = $"https://{context.Request.Host}/snapshot/true" },
    new { expecting =  "Options 2", uri = $"https://{context.Request.Host}/snapshot/false" },
});
app.MapGet("/{serviceName}/{someCondition}", (string serviceName, bool someCondition, HttpContext context) =>
{
    var myNameService = GetMyNameService(serviceName, context.RequestServices);
    var name = myNameService.GetName(someCondition);
    return Results.Ok(new { name });

    static IMyNameService GetMyNameService(string serviceName, IServiceProvider services) => serviceName switch
    {
        "options" => services.GetRequiredService<MyNameServiceUsingDoubleNameOptions>(),
        "factory" => services.GetRequiredService<MyNameServiceUsingNamedOptionsFactory>(),
        "monitor" => services.GetRequiredService<MyNameServiceUsingNamedOptionsMonitor>(),
        "snapshot" => services.GetRequiredService<MyNameServiceUsingNamedOptionsSnapshot>(),
        _ => throw new NotSupportedException($"The service named '{serviceName}' is not supported."),
    };
});
app.Run();

