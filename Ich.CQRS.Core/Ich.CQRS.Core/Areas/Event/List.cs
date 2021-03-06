using Ich.CQRS.Core.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Ich.CQRS.Core.Areas.Event
{
    // ** Command Query pattern

    public class List
    {
        // Input

        public class Query : IRequest<Result> { }

        // Output

        public class Result
        {
            public List<Event> Events { get; set; } = new List<Event>();

            public class Event
            {
                public int Id { get; set; }
                public string Transaction { get; set; }
                public string EventDate { get; set; }
                public string Action { get; set; }
                public string Table { get; set; }
                public int TableId { get; set; }
                public int Version { get; set; }
                public string Content { get; set; }
            }
        }

        // Process

        public class QueryHandler : RequestHandler<Query, Result>
        {
            // ** DI Pattern

            private readonly CQRSContext _db;

            public QueryHandler(CQRSContext db)
            {
                _db = db;
            }

            protected override Result Handle(Query query)
            {
                var result = new Result();
                var events = _db.Event.OrderByDescending(e => e.EventDate);

                foreach (var evt in events)
                {
                    result.Events.Add(new Result.Event
                    {
                        // ** Data Mapper Pattern

                        Id = evt.Id,
                        Transaction = evt.Transaction,
                        EventDate = evt.EventDate.ToString(),
                        Action = evt.Action,
                        Table = evt.Table,
                        TableId = evt.TableId,
                        Version = evt.Version,
                        Content = evt.Content
                    });
                }

                return result;
            }
        }
    }
}
