using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator
{
    public class MyController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        public MyController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        [Route("/")]
        public IActionResult Get()
        {
            using (var myService = _serviceProvider.GetService<IMyService>())
            {
                myService.Execute();
                return Ok("Success!");
            }
        }
    }
}
