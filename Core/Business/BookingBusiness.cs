using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class BookingBusiness
    {
        private IDbContext mContext;
        public IConfiguration Configuration { get; }

        public BookingBusiness(IDbContext mContext, IConfiguration aConfiguration)
        {
            this.mContext = mContext;
            Configuration = aConfiguration;
        }

        public static double Paid(Booking aBook)
        {
            return aBook.Payments.Where(x=>x.Status != PaymentStatus.Deleted).Sum(x => x.Amount);
        }

        public static double Balance ( Booking aBook )
        {
            if (aBook.Status == BookingStatus.Deleted 
                || aBook.Status == BookingStatus.Cancelled 
                || aBook.Status == BookingStatus.PendingCancellation)
                return 0;

            return aBook.Amount - Paid(aBook);
        }
        public int Save(Booking aBooking, Contact aAgent)
        {
            if (aBooking == null)
                return 0;

            UpdateTracking(aBooking, aAgent);
            ComputeCommission(aBooking);
            UpdateStatus(aBooking);
            var lCnt = new BookingDataAccess(mContext).Save(aBooking);

            // Must be aware of the Trip conversion which results in a new trip
            if ( aBooking.Trip != null )
                new TripBusiness(mContext).Update(aBooking.TripID);
            return lCnt;
        }

        public static bool isValid (Booking aBooking )
        {
            return (aBooking.Status == BookingStatus.PendingCancellation ||
                aBooking.Status == BookingStatus.Cancelled ||
                aBooking.Status == BookingStatus.Deleted) == false;
        }

        private void UpdateStatus(Booking aBooking)
        {
            if (isValid(aBooking) == false)
                return;

            var lPaid = aBooking.Payments.Sum(x => x.Amount);
            if (lPaid >= aBooking.Amount)
                aBooking.Status = BookingStatus.PaidInFull;
            else if ( lPaid >= aBooking.Deposit)
                aBooking.Status = BookingStatus.BalanceOutstanding;
            else
                aBooking.Status = BookingStatus.AwaitingDeposit;
        }

        public static double Payments ( Booking aBooking )
        {
            if (isValid(aBooking) == false)
                return 0;

            return aBooking.Payments.Where(x=>x.Status == PaymentStatus.New).Sum(x => x.Amount);
        }

        private void ComputeCommission(Booking aBooking)
        {
            Agent lAgent = null;
            if (aBooking.Trip == null)
                lAgent = new OpportunityDataAccess(mContext).Get(aBooking.TripID).Agent;
            else
                lAgent = new ContactBusiness(mContext).GetAgent(aBooking.Trip.AgentId);

            var lAgentRate = 1.0;
            if (lAgent.CommissionRate != 0)
                lAgentRate = lAgent.CommissionRate;
            var lOriginalAgentRate = lAgentRate;

            var lHostPercent = lAgent.CommissionRate;
            var lFranchise = lAgent.Employer as Franchise;
            var lFranchisePercent = 0.0;
            if (lFranchise != null  )
                lFranchisePercent = lFranchise.CommissionRate;

            if (lAgentRate == 1)
                lAgentRate = 1 - lFranchisePercent;

            aBooking.ICCommission = aBooking.GrossCommission * lAgentRate;
            var lRemainder = aBooking.GrossCommission - aBooking.ICCommission;
            if (lOriginalAgentRate != 1)
                aBooking.HQCommission = lFranchisePercent * lRemainder;
            else
                aBooking.HQCommission = lRemainder;

            aBooking.HostAgencyCommission = aBooking.GrossCommission - aBooking.ICCommission - aBooking.HQCommission;
            if (aBooking.Trip != null && aBooking.Trip.EndDate != null )
                aBooking.TargetPayment = aBooking.Trip.EndDate.AddDays(45);
        }

        public IEnumerable<Booking> Get(Agent aAgent)
        {
            return new BookingDataAccess(mContext).Get(aAgent)
                .Where(x => x.Status != BookingStatus.Cancelled
                && x.Status != BookingStatus.Deleted
                && x.Status != BookingStatus.PendingCancellation);
        }

        public IEnumerable<Booking> Get(DateTime aTravelDate)
        {
            var lData = new BookingDataAccess(mContext).Get(aTravelDate)
                .Where(x => x.Status != BookingStatus.Cancelled
                && x.Status != BookingStatus.Deleted
                && x.Status != BookingStatus.PendingCancellation
                && x.Trip.EndDate < DateTime.Now);
            SetPaymentDates(lData);
            return lData.OrderByDescending(m => m.SettlementAge);
        }

        private void SetPaymentDates(IEnumerable<Booking> aBookings)
        {
            //var lTourOperators = new CompanyDataAccess(mContext).GetTourOperators();
            foreach ( var lBooking in aBookings)
            {
                if (lBooking.TourOperator == null)
                    continue;

                lBooking.TargetPayment = lBooking.Trip.EndDate.AddDays(lBooking.TourOperator.SettlementTerms);
                lBooking.SettlementAge = DateTime.Now.Subtract(lBooking.TargetPayment.Value).Days;
            }
        }

        public Booking Get ( int aBookingID )
        {
            var lResults = new BookingDataAccess(mContext).Get(aBookingID);
            return lResults;
        }

        internal static void UpdateTracking(Booking aBooking, Contact aUser)
        {
            if (aBooking.BookingID == 0)
            {
                aBooking.CreatedById = aUser.Id;
                aBooking.CreatedOn = DateTime.Now;
            }
            aBooking.UpdatedById = aUser.Id;
            aBooking.UpdatedOn = DateTime.Now;

        }
    }
}