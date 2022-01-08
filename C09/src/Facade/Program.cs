using Facade;
using OpaqueFacadeSubSystem.Abstractions;
using TransparentFacadeSubSystem.Abstractions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddOpaqueFacadeSubSystem()
    .AddTransparentFacadeSubSystem()
    .AddSingleton<IComponentB, UpdatedComponentB>()
;

var app = builder.Build();
app.MapGet("/opaque/a", (IOpaqueFacade opaqueFacade) => opaqueFacade.ExecuteOperationA());
app.MapGet("/opaque/b", (IOpaqueFacade opaqueFacade) => opaqueFacade.ExecuteOperationB());
app.MapGet("/transparent/a", (ITransparentFacade transparentFacade) => transparentFacade.ExecuteOperationA());
app.MapGet("/transparent/b", (ITransparentFacade transparentFacade) => transparentFacade.ExecuteOperationB());
app.Run();
