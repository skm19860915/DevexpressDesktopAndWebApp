using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Utilities;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    public class BookingDataAccess
    {
        const string ClassName = "BookingDataAccess::";
        IDbContext mContext = null;
        public BookingDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public int Save(Booking aBooking)
        {
            string FuncName = $"{ClassName}Save -";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aBooking.BookingID > 0)
                {
                    mContext.Bookings.Update(aBooking);
                    lAction = "Updated";
                }
                else
                    mContext.Bookings.Add(aBooking);

                lCount = mContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} booking records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save booking", e);
                throw e;
            }
            return lCount;

        }

        public Booking Get(int aBookingID)
        {
            var lBooking = mContext.Bookings
                .Include(x => x.TourOperator)
                .Include(x => x.Supplier)
                .Include(x => x.Credits).ThenInclude(sub=>sub.Traveler)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.Payments)
                .Include(x => x.Trip).ThenInclude(x1=>x1.Agent).ThenInclude(x2=>x2.Employer)
                .Include(x => x.Trip).ThenInclude(x1 => x1.Travelers)
               .FirstOrDefault(x => x.BookingID == aBookingID);
            return lBooking;
        }
        public IEnumerable<Booking> Get(Agent aAgent)
        {
            var lTeamMemberIDs = mContext.TeamMembers.Where(i => mContext.TeamMembers.Where(u => u.MemberId == aAgent.Id).Select(y => y.TeamId).Contains(i.TeamId)).Select(t => t.MemberId).ToList();

            if (lTeamMemberIDs.Count > 0)
                return mContext.Bookings
                .Include(x => x.TourOperator)
                .Include(x => x.Supplier)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.Payments)
                .Include(x => x.Trip)
                .Where(x => lTeamMemberIDs.Contains(x.Trip.AgentId)).ToList();
            else
                return mContext.Bookings
                        .Include(x => x.TourOperator)
                        .Include(x => x.CreatedBy)
                        .Include(x => x.UpdatedBy)
                        .Include(x => x.Supplier)
                        .Include(x => x.Payments)
                        .Include(x => x.Trip)
                        .Where(x => x.Trip.AgentId == aAgent.Id);
        }
        public IEnumerable<Booking> Get(DateTime aTravelDue)
        {
                var lData = mContext.Bookings
                        .Include(x => x.TourOperator)
                        .Include(x => x.Trip).ThenInclude(sub=>sub.Agent)
                        .Include(x => x.Trip).ThenInclude(sub=>sub.Travelers)
                        .Include(x => x.CreatedBy)
                        .Include(x => x.UpdatedBy)
                        .Include(x => x.Supplier)
                        .Include(x => x.Payments)
                        .Include(x => x.Trip)
                        .Where(x => x.Trip.EndDate < aTravelDue);
            return lData;
        }
    }
}
