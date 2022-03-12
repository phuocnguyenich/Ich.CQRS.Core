using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ich.CQRS.Core.Code.Attributes
{
    public class MenuAttribute : ActionFilterAttribute
    {
        private readonly string _menu;

        public MenuAttribute(string menu)
        {
            _menu = menu;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            (filterContext.Controller as Controller).ViewBag.Menu = _menu;

            base.OnActionExecuting(filterContext);
        }
    }
}
