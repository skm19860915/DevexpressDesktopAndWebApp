using BlitzerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUnitTests.Business
{
    public class BookingBusiness
    {
        IDbContext DbContext { get; set; }

        public BookingBusiness(IDbContext aContext)
        {
            DbContext = aContext;
        }
        public BlitzerCore.Models.Booking CreateBooking(BlitzerCore.Models.Trip aTrip, double aAmount)
        {
            var lBooking = new BlitzerCore.Models.Booking() {TripID = aTrip.ID, Amount = aAmount};
            DbContext.Bookings.Add(lBooking);
            DbContext.SaveChanges();
            return lBooking;
        }
    }
}
