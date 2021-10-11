using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    // .AddSingleton<IComponent, ComponentA>()
    // .AddSingleton<IComponent>(serviceProvider => new DecoratorA(new ComponentA()))
    .AddSingleton<IComponent>(serviceProvider => new DecoratorB(new DecoratorA(new ComponentA())))
;

var app = builder.Build();
app.MapGet("/", (IComponent component) => component.Operation());
app.Run();

public interface IComponent
{
    string Operation();
}

public class ComponentA : IComponent
{
    public string Operation()
    {
        return "Hello from ComponentA";
    }
}

public class DecoratorA : IComponent
{
    private readonly IComponent _component;

    public DecoratorA(IComponent component)
    {
        _component = component ?? throw new ArgumentNullException(nameof(component));
    }

    public string Operation()
    {
        var result = _component.Operation();
        return $"<DecoratorA>{result}</DecoratorA>";
    }
}

public class DecoratorB : IComponent
{
    private readonly IComponent _component;

    public DecoratorB(IComponent component)
    {
        _component = component ?? throw new ArgumentNullException(nameof(component));
    }

    public string Operation()
    {
        var result = _component.Operation();
        return $"<DecoratorB>{result}</DecoratorB>";
    }
}