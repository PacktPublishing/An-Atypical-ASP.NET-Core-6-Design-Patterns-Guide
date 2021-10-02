using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DecoratorScrutor
{
    public class Client
    {
        private readonly IComponent _component;

        public Client(IComponent component)
        {
            _component = component ?? throw new ArgumentNullException(nameof(component));
        }

        public Task ExecuteAsync(HttpContext context)
        {
            var result = _component.Operation();
            return context.Response.WriteAsync($"Operation: {result}");
        }
    }
}
