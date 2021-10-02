using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Return_Values
{
    [Route("/")]
    public class ReturnValueController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Index()
        {
            yield return AbsoluteUriOfAction(nameof(GetActionResultMyResult));
            yield return AbsoluteUriOfAction(nameof(GetMyResult));
            yield return AbsoluteUriOfAction(nameof(GetIActionResult));
        }

        [HttpGet("ActionResultMyResult/{input}")]
        public ActionResult<MyResult> GetActionResultMyResult(int input)
        {
            return Ok(new MyResult
            {
                Input = input,
                Value = nameof(GetActionResultMyResult)
            });
        }

        [HttpGet("GetMyResult/{input}")]
        public ActionResult<MyResult> GetMyResult(int input)
        {
            return new MyResult
            {
                Input = input,
                Value = nameof(GetMyResult)
            };
        }

        [HttpGet("GetIActionResult/{input}")]
        public IActionResult GetIActionResult(int input)
        {
            return Ok(new MyResult
            {
                Input = input,
                Value = nameof(GetIActionResult)
            });
        }

        private string AbsoluteUriOfAction(string action)
        {
            return Url.Action(
                action,
                ControllerContext.ActionDescriptor.ControllerName,
                new { input = 1 },
                HttpContext.Request.Scheme
            );
        }
    }
}
