using Ich.CQRS.Core.Code.Caching;
using Ich.CQRS.Core.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Ich.CQRS.Core.Areas.Seat
{
    public class List
    {
        // Input 

        public class Query : IRequest<Result>
        {
            public int? PlaneId { get; set; }
        }

        // Output

        public class Result
        {
            public int? PlaneId { get; set; }
            public List<Seat> Seats { get; set; } = new List<Seat>();

            public class Seat
            {
                public int Id { get; set; }
                public int PlaneId { get; set; }
                public string Plane { get; set; }
                public string Number { get; set; }
                public string Location { get; set; }
                public int TotalBookings { get; set; }
            }
        }

        // Process

        public class QueryHandler : RequestHandler<Query, Result>
        {
            private readonly CQRSContext _db;
            private readonly ICache _cache;

            public QueryHandler(CQRSContext db, ICache cache)
            {
                _db = db;
                _cache = cache;
            }

            protected override Result Handle(Query query)
            {
                var result = new Result { PlaneId = query.PlaneId };

                var seats = _db.Seat.AsQueryable();

                if (query.PlaneId != null)
                    seats = seats.Where(s => s.PlaneId == query.PlaneId);

                seats = seats.OrderBy(s => s.PlaneId).ThenBy(s => s.Number);

                // ** Iterator pattern

                foreach (var seat in seats)
                {
                    var plane = _cache.Planes[seat.PlaneId];
                    result.Seats.Add(new Result.Seat
                    {
                        // ** Data Mapper Pattern

                        Id = seat.Id,
                        PlaneId = seat.PlaneId,
                        Plane = plane.Model + ": " + plane.Name,
                        Number = seat.Number,
                        Location = seat.Location,
                        TotalBookings = seat.TotalBookings
                    });
                }

                return result;
            }
        }
    }
}
