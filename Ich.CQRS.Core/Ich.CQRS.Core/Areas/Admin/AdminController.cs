using Ich.CQRS.Core.Code.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ich.CQRS.Core.Areas.Admin
{
    [Menu("Admin")]
    [AdminMenu("Overview")]
    [Route("admin")]

    public class AdminController : Controller
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator) { _mediator = mediator; }

        #region Queries

        [HttpGet]
        public async Task<IActionResult> List(List.Query query)
        {
            var model = await _mediator.Send(query);
            return View(model);
        }

        #endregion
    }
}
