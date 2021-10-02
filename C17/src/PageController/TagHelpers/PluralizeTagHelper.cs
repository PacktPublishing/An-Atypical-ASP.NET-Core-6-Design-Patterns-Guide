using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PageController.TagHelpers
{
    [HtmlTargetElement("pluralize", TagStructure = TagStructure.WithoutEndTag)]
    public class PluralizeTagHelper : TagHelper
    {
        public int Count { get; set; }
        public string Singular { get; set; }
        public string Plural { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var text = Count > 1 ? Plural : Singular;
            text = string.Format(text, Count);

            output.TagName = null;
            output.Content.SetContent(text);
        }
    }
}
