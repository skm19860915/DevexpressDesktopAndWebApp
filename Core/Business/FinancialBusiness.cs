using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using Microsoft.Extensions.Configuration;

namespace BlitzerCore.Business
{
    public class FinancialBusiness
    {
        private IDbContext DbContext { get; set; }
        public IConfiguration Configuration { get; }

        public FinancialBusiness(IDbContext aContext, IConfiguration aConfiguration)
        {
            DbContext = aContext;
            Configuration = aConfiguration;
        }

        public FinancialSnapShot Get ( Agent aAgent)
        {
            var lSnapShot = new FinancialSnapShot();
            var lData = new BookingBusiness(DbContext, Configuration).Get(aAgent).Where(x => x.Trip.EndDate> YearStart && x.Trip.EndDate < YearEnd);
            var lMonthData = new BookingBusiness(DbContext, Configuration).Get(aAgent).Where(x => x.Trip.OppClosedDate > MonthStart && x.Trip.OppClosedDate < MonthEnd);

            lSnapShot.Sales_YTD = GetSalesYTD(lData);
            lSnapShot.Realized_YTD = GetRealizedYTD(lData);
            lSnapShot.Unrealized_YTD = GetUnRealizedYTD(lData);
            lSnapShot.Bookings = BookingUIHelper.Convert(lData);
            lSnapShot.PL_MTD = GetPLMTD(lMonthData, aAgent);
            return lSnapShot;
        }

        public List<UIBooking> GetFinalPayments(Agent aAgent)
        {
            var lData = new BookingBusiness(DbContext, Configuration).Get(aAgent).Where(x => x.Status != BookingStatus.Cancelled 
            && x.Status != BookingStatus.PaidInFull 
            && x.Status != BookingStatus.PendingCancellation
            && x.Trip.EndDate > DateTime.Now ).OrderBy(x=>x.FinalPayment);
            return BookingUIHelper.Convert(lData);
        }

        DateTime MonthStart => new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime MonthEnd => new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);
        DateTime YearStart => new DateTime(DateTime.Now.Year, 1, 1);
        DateTime YearEnd => new DateTime(DateTime.Now.Year, 12, 31);

        private string GetSalesYTD(IEnumerable<Booking> aData)
        {
            var lSales = aData.Where(x=>x.Trip.StartDate > YearStart && x.Trip.StartDate < YearEnd).Sum(x => x.Amount);
            return DataHelper.ConvertToCurrency(lSales);
        }
        private string GetRealizedYTD(IEnumerable<Booking> aData)
        {
            var lSales = aData.Where(x => x.PaymentDate != null && x.PaymentDate > YearStart && x.PaymentDate < YearEnd )
                .Sum(x => x.ICCommission);
            return DataHelper.ConvertToCurrency(lSales);
        }
        private string GetUnRealizedYTD(IEnumerable<Booking> aData)
        {
            var lSales = aData.Sum(x => x.ICCommission);
            return DataHelper.ConvertToCurrency(lSales);
        }
        private string GetPLMTD(IEnumerable<Booking> aData, Agent aAgent)
        {
            var lCommission = aData.Sum(x => x.ICCommission);
            var lCosts = new AgentDataAccess(DbContext).GetFixedCosts(aAgent);
            return DataHelper.ConvertToCurrency(lCommission - lCosts);
        }
    }
}
