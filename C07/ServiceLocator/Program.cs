using ServiceLocator;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSingleton<IMyService, MyServiceImplementation>()
    .AddControllers()
;
var app = builder.Build();
app.MapControllers();
app.MapGet("minimal-api", (IMyService myService) =>
{
    myService.Execute();
    return "Success!";
});
app.Run();
