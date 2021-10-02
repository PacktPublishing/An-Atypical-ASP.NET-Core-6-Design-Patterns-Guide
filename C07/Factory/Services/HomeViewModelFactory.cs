using Factory.Models;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Services
{
    public interface IHomeViewModelFactory
    {
        PrivacyViewModel CreatePrivacyViewModel();
    }

    public class HomeViewModelFactory : IHomeViewModelFactory
    {
        public PrivacyViewModel CreatePrivacyViewModel() => new PrivacyViewModel
        {
            Title = "Privacy Policy (from IHomeViewModelFactory)",
            Content = new HtmlString("<p>Use this page to detail your site's privacy policy.</p>")
        };
    }
}
