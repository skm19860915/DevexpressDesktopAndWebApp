using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;

namespace BlitzerCore.UIHelpers
{
    public class TripUIHelper
    {
        public static List<UITrip> Convert(List<Trip> aTrips, bool aShallowCopy = false )
        {
            var lOutput = new List<UITrip>();
            foreach (var lTrip in aTrips)
                lOutput.Add(Convert(lTrip, aShallowCopy));
            return lOutput;
        }

        public static UITrip Convert(Trip aTrip, bool aShallow = false )
        {
            var lUITrip = new UITrip();
            if ( aTrip == null )
            {
                Logger.LogWarning("TripUIHelper::Convert(Trip,aShallow) passed a null");
                return lUITrip;
            }
            if (aTrip.Tasks == null)
                aTrip.Tasks = new List<Task>();

            lUITrip.Id = aTrip.ID;
            lUITrip.Name = aTrip.Name;
            lUITrip.Agent = ContactUIHelper.Convert(aTrip.Agent);
            lUITrip.OutBoundDate = DataHelper.GetDateString(aTrip.StartDate);
            lUITrip.InBoundDate = DataHelper.GetDateString(aTrip.EndDate);
            lUITrip.DaysToStart = (int)Math.Round(aTrip.StartDate.Subtract(DateTime.Now).TotalDays, MidpointRounding.ToEven)+1;
            lUITrip.Balance = DataHelper.ConvertToCurrency(TripBusiness.Balance(aTrip));
            lUITrip.Total = DataHelper.ConvertToCurrency(TripBusiness.Total(aTrip));
            lUITrip.ICCommission = DataHelper.ConvertToCurrency(TripBusiness.ICCommission(aTrip));
            lUITrip.GrossCommission = DataHelper.ConvertToCurrency(TripBusiness.GrossCommission(aTrip));
            lUITrip.Travelers = ContactUIHelper.Convert(aTrip.Travelers.Select(x => x.User));
            var lFinalPayment = aTrip.Bookings.Min(x => x.FinalPayment);
            if (lFinalPayment != null)
                lUITrip.FinalPayment = DataHelper.GetDateString(lFinalPayment);
            else
                lUITrip.FinalPayment = "TBD";
            if ( aTrip.Bookings != null && aTrip.Bookings.Count > 0 )
                lUITrip.CreditMemo = DataHelper.ConvertToCurrency( aTrip.Bookings.Where(x=>x.Credits != null).Sum(x => x.Amount));
            if ( aShallow == false ) 
                lUITrip.Bookings = Convert(aTrip.Bookings);
            lUITrip.FinalPaymentStatus = TripBusiness.FinalPaymentStatus(aTrip);
            lUITrip.TripStage = aTrip.TripStage;
            lUITrip.TripStageStr = Convert(aTrip.TripStage);
            lUITrip.TripStatusStr = Convert(aTrip.TripStatus);
            lUITrip.TripStatus = aTrip.TripStatus;
            lUITrip.Notes = aTrip.Notes;
            lUITrip.HasTransfer = TripBusiness.HasTransfer(aTrip);
            lUITrip.NoteEntries = NoteUIHelper.Convert(aTrip.NoteEntries);
            lUITrip.Tasks = TaskUIHelper.Convert(aTrip.Tasks.Where(x=>x.Status != TaskStatusTypes.DELETED).OrderBy(x=>x.Status));

            if (aTrip.Files != null)
                lUITrip.Files = aTrip.Files.Select(x => FileUIHelper.Convert(x)).ToList();

            return lUITrip;
        }

        public static List<UITripList> ConvertList(List<Trip> aTripList)
        {
            var lOutput = new List<UITripList>();
            foreach (var lTrip in aTripList)
            {
                lOutput.Add(ConvertRAW(lTrip));
            }

            return lOutput;
        }

        private static UITripList ConvertRAW(Trip aTrip)
        {
            return new UITripList()
            {
                Id = aTrip.ID,
                Name = aTrip.Name, EndDate = aTrip.EndDate, StartDate = aTrip.StartDate,
                GrossCommission = TripBusiness.GrossCommission(aTrip), ICCommission = TripBusiness.ICCommission(aTrip),
                Stage = Convert(aTrip.TripStage), Status = Convert(aTrip.TripStatus)
            };
        }

        public static string Convert(TripStage aStage)
        {
            switch ( aStage)
            {
                case TripStage.BookTransfer: return "Book Transfer";
                case TripStage.BalanceOutstanding: return "Balance Outstanding";
                case TripStage.Booked: return "Book";
                case TripStage.CompleteProfile: return "Incomplete Profile";
                case TripStage.Traveled: return "Travelled";
                case TripStage.ReadyForTravel: return "Ready for Travel";
            }

            return "";
        }

        public static string Convert(Trip.Statuses aStatus)
        {
            switch (aStatus)
            {
                case Trip.Statuses.Active: return "Active";
                case Trip.Statuses.Cancelled: return "Cancelled";
                case Trip.Statuses.Completed: return "Completed";
            }

            return "";
        }

        public static List<UIBooking> Convert(List<Booking> aBookings)
        {
            List<UIBooking> lBookings = new List<UIBooking>();
            foreach (var lBooking in aBookings.Where(x=>x.Status != BookingStatus.Deleted))
                lBookings.Add(BookingUIHelper.Convert(lBooking));

            return lBookings;
        }
        public static List<UITrip> Convert(IDbContext aContext, List<Trip> aTrips)
        {
            var lOutput = new List<UITrip>();
            foreach (var lTrip in aTrips)
                lOutput.Add(Convert(lTrip));
            return lOutput;
        }


    }
}
