global using System;

using ChainOfResponsibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Create the chain of responsibility,
// ordered by the most called (or the one that must be executed the faster)
// to the less called handler (or the one that can take more time to be executed),
// followed by the DefaultHandler.
builder.Services.AddSingleton<IMessageHandler>(new AlarmTriggeredHandler(new AlarmPausedHandler(new AlarmStoppedHandler(new DefaultHandler()))));

var app = builder.Build();

// "Menu" enpoint
app.MapGet("/", () => new[] {
    "/handle/AlarmTriggered",
    "/handle/AlarmPaused",
    "/handle/AlarmStopped",
    "/handle/SomeUnhandledMessageName",
});

// Consumer (client) endpoint
app.MapGet("/handle/{name}", (string name, string payload, IMessageHandler messageHandler) =>
{
    var message = new Message
    {
        Name = name,
        Payload = payload,
    };
    try
    {
        // Send the message into the chain of responsibility
        messageHandler.Handle(message);
        return $"Message '{message.Name}' handled successfully.";
    }
    catch (NotSupportedException ex)
    {
        return ex.Message;
    }
});
app.Run();
