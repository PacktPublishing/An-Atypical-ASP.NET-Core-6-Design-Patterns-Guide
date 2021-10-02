using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Models
{
    public class PrivacyViewModel
    {
        public string Title { get; set; }
        public HtmlString Content { get; set; }
    }
}
