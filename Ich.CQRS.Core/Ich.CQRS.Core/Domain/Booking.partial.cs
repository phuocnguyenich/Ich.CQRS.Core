using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ich.CQRS.Core.Domain
{
    public partial class Booking
    {
        public bool ShouldSerializeTraveler()
        {
            return false;
        }
        public bool ShouldSerializeSeat()
        {
            return false;
        }
        public bool ShouldSerializeFlight() 
        { 
            return false; 
        }
    }
}
