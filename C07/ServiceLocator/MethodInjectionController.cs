using Microsoft.AspNetCore.Mvc;

namespace ServiceLocator;

public class MethodInjectionController : ControllerBase
{
    [Route("/method-injection")]
    public IActionResult GetUsingMethodInjection([FromServices] IMyService myService)
    {
        if (myService == null) { throw new ArgumentNullException(nameof(myService)); }
        myService.Execute();
        return Ok("Success!");
    }
}
