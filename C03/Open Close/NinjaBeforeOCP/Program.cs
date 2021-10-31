using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NinjaBeforeOCP;
using NinjaShared;
using System;
using System.Numerics;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.MapGet("/", async (HttpContext context) =>
{
    // Create actors
    var target = new Ninja("The Unseen Mirage");
    var ninja = new Ninja("The Blue Phantom");

    // Execute the sequence of actions
    await Logic.ExecuteSequenceAsync(ninja, target, writeAsync: s => context.Response.WriteAsync(s));
});
app.Run();
