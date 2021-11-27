using Microsoft.AspNetCore.Mvc;

namespace ServiceLocator;

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
        using (var myService = _serviceProvider.GetRequiredService<IMyService>())
        {
            myService.Execute();
            return Ok("Success!");
        }
    }
}
