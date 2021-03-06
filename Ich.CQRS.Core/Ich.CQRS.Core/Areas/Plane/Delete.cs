using Ich.CQRS.Core.Code.Caching;
using Ich.CQRS.Core.Domain;
using MediatR;

namespace Ich.CQRS.Core.Areas.Plane
{
    // ** Command Query pattern

    public class Delete
    {
        // Input

        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        // Process

        public class CommandHandler : RequestHandler<Command>
        {
            // ** DI Pattern

            private readonly CQRSContext _db;
            private readonly ICache _cache;

            public CommandHandler(CQRSContext db, ICache cache)
            {
                _db = db;
                _cache = cache;
            }

            protected override void Handle(Command message)
            {
                var plane = _db.Plane.Find(message.Id);

                _db.Plane.Remove(plane);
                _db.SaveChanges();

                _cache.ClearPlanes();

            }
        }
    }
}
