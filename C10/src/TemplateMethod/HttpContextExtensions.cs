using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TemplateMethod
{
    internal static class HttpContextExtensions
    {
        public static async Task WriteLineAsync(this HttpContext context, string text)
        {
            await context.Response.WriteAsync(text);
            await context.Response.WriteAsync(Environment.NewLine);
        }
    }
}
