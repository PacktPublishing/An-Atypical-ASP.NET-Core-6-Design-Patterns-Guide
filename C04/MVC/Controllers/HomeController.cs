using MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        public void ActionWithoutResult()
        {
            var youCanSetABreakpointHere = "";
        }

        public async Task ActionWithoutResultV2Async()
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(), 
                "wwwroot/images/netcore-logo.png"
            );
            var bytes = System.IO.File.ReadAllBytes(filePath);
            //
            // Do some processing here
            //
            Response.ContentLength = bytes.Length;
            Response.ContentType = "image/png";
            await Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }

        public async Task DownloadTheImageAsync()
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/images/netcore-logo.png"
            );
            var bytes = System.IO.File.ReadAllBytes(filePath);
            //
            // Do some processing here
            //
            Response.ContentLength = bytes.Length;
            Response.ContentType = "image/png";
            Response.Headers.Add("Content-Disposition", "attachment; filename=\"netcore.png\"");
            await Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }

        public IActionResult ActionWithSomeInput(int id)
        {
            var model = id;
            return View(model);
        }

        public IActionResult ActionWithSomeInputAndAModel(int id)
        {
            var model = new SomeModel
            {
                SelectedId = id,
                Title = "This title was set in HomeController!"
            };
            return View(model);
        }
    }
}