using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ich.CQRS.Core.Code.Attributes
{
    public class AdminMenuAttribute : ActionFilterAttribute
    {
        private readonly string _menu;

        public AdminMenuAttribute(string menu)
        {
            _menu = menu;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            (filterContext.Controller as Controller).ViewBag.AdminMenu = _menu;

            base.OnActionExecuting(filterContext);
        }
    }
}
