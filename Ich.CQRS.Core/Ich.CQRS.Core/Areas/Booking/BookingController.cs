using Ich.CQRS.Core.Code.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ich.CQRS.Core.Areas.Booking
{
    [Menu("Bookings")]
    [Route("bookings")]
    public class BookingController : Controller
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Queries

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] List.Query query)
        {
            var model = await _mediator.Send(query);
            return View(model);
        }

        #endregion
    }
}
