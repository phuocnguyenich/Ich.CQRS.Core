using Ich.CQRS.Core.Code.Attributes;
using Ich.CQRS.Core.Code.Caching;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ich.CQRS.Core.Areas.Home
{
    [Menu("Welcome")]
    public class HomeController : Controller
    {
        public HomeController(ICache cache)
        { }

        [HttpGet("")]
        public IActionResult Welcome()
        {
            return View();
        }
    }
}
