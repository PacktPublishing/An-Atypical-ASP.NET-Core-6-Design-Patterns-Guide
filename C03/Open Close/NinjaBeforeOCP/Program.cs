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

    // The Unseen Mirage move away from The Blue Phantom
    target.MoveTo(5, 5);

    // Second attack (Shuriken)
    var result2 = ninja.Attack(target);
    await PrintAttackResult(result2);

    // The Unseen Mirage strikes back
    var result3 = target.Attack(ninja);
    await PrintAttackResult(result3);

    // Write the outcome of an AttackResult to response stream
    async Task PrintAttackResult(AttackResult attackResult)
    {
        if (attackResult.Succeeded)
        {
            await context.Response.WriteAsync($"'{attackResult.Attacker}' attacked '{attackResult.Target}' using '{attackResult.Weapon}' successfully at distance {attackResult.Distance}!{Environment.NewLine}");
        }
        else
        {
            await context.Response.WriteAsync($"'{attackResult.Attacker}' failed to hit '{attackResult.Target}' using '{attackResult.Weapon}' at distance {attackResult.Distance}...{Environment.NewLine}");
        }
    }
});
app.Run();
