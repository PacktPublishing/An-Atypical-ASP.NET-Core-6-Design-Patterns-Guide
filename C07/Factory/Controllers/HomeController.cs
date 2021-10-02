using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Factory.Models;
using Factory.Services;

namespace Factory.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeViewModelFactory _homeViewModelFactory;
        public HomeController(IHomeViewModelFactory homeViewModelFactory)
        {
            _homeViewModelFactory = homeViewModelFactory ?? throw new ArgumentNullException(nameof(homeViewModelFactory));
        }

        public IActionResult Index([FromServices]HomePageViewModel viewModel)
        {
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            var viewModel = _homeViewModelFactory.CreatePrivacyViewModel();
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
