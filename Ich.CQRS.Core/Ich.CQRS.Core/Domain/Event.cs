using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ich.CQRS.Core.Domain
{
    public partial class Event
    {
        public int Id { get; set; }
        public string Transaction { get; set; }
        public DateTime EventDate { get; set; }
        public string Action { get; set; }
        public string Table { get; set; }
        public int TableId { get; set; }
        public int Version { get; set; }
        public string Content { get; set; }
    }
}
