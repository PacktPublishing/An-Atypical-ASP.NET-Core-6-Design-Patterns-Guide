using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NinjaBeforeOCP;
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

    // First attack (Sword)
    var result = ninja.Attack(target);
    await PrintAttackResult(result);

    // Second attack (Shuriken)
    var result2 = ninja.Attack(target);
    await PrintAttackResult(result2);

    // Write the outcome of an AttackResult to response stream
    async Task PrintAttackResult(AttackResult attackResult)
    {
        await context.Response.WriteAsync($"'{attackResult.Attacker}' attacked '{attackResult.Target}' using '{attackResult.Weapon}'!{Environment.NewLine}");
    }
});
app.Run();
