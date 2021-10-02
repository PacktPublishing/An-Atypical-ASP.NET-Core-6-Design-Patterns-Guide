using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WASM
{
    public partial class CodeBehindComponent
    {
        [Parameter]
        public string Text { get; set; }
    }
}
