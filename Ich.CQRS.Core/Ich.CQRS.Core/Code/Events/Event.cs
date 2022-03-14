using Ich.CQRS.Core.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ich.CQRS.Core.Code.Events
{
    #region Interface

    public interface IEvent
    {
        void InsertBooking(Booking booking);
        void DeleteBooking(Booking booking);
        void UpdateBooking(Booking oldBooking, Booking newBooking);
    }

    #endregion
    public class Event : IEvent
    {
        #region Dependency Injection

        private readonly CQRSContext _db;

        public Event(CQRSContext db)
        {
            _db = db;
        }

        #endregion

        public void InsertBooking(Booking booking)
        {
            var evt = new Domain.Event
            {
                Action = "Insert",
                Transaction = Guid.NewGuid().ToString(),
                Table = "Booking",
                TableId = booking.Id,
                Content = JsonConvert.SerializeObject(booking),
                EventDate  =DateTime.UtcNow
            };

            _db.Event.Add(evt);
            _db.SaveChanges();
        }

        public void UpdateBooking(Booking oldBooking, Booking newBooking)
        {
            bool flightChanged = oldBooking.FlightId != newBooking.FlightId;
            bool seatChanged = oldBooking.SeatId != newBooking.SeatId;

            if (!flightChanged && !seatChanged) return;

            var evt = new Domain.Event
            {
                Action = "Update",
                Transaction = Guid.NewGuid().ToString(),
                Table = "Booking",
                TableId = newBooking.Id,
                Content = JsonConvert.SerializeObject(newBooking),
                EventDate = DateTime.UtcNow
            };

            _db.Event.Add(evt);
            _db.SaveChanges();
        }


        public void DeleteBooking(Booking booking)
        {
            var evt = new Domain.Event
            {
                Action = "Delete",
                Transaction = Guid.NewGuid().ToString(),
                Table = "Booking",
                TableId = booking.Id,
                Content = $"{{Id : {booking.Id}}}",
                EventDate = DateTime.UtcNow
            };

            _db.Event.Add(evt);
            _db.SaveChanges();
        }
    }
}
