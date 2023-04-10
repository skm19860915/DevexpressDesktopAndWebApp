using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.Utilities;
using Microsoft.Extensions.Configuration;

namespace BlitzerCore.Business
{
    public class CreditBusiness
    {
        const string ClassName = "CreditBusiness::";

        private IDbContext DbContext { get; set; }
        public IConfiguration Configuration { get; }

        public CreditBusiness(IDbContext mContext, IConfiguration aConfiguration)
        {
            this.DbContext = mContext;
            Configuration = aConfiguration;
        }

        public List<Credit> Get(Agent aAgent)
        {
            List<Credit> lOutput = new List<Credit>();
            if (aAgent == null)
                return lOutput;

            lOutput = new CreditDataAccess(DbContext).Get(aAgent);

            return lOutput;
        }
        public Credit Get(int aCreditId)
        {
            return new CreditDataAccess(DbContext).Get(aCreditId);

        }

        /// <summary>
        /// Save a Credit Voucher for everyone on the trip
        /// </summary>
        /// <param name="aUICredit"></param>
        public void Save(UICredit aUICredit)
        {
            string FuncName = $"{ClassName}Save (UICredit = {aUICredit.Id})";

            try
            {
                var lCreditDA = new CreditDataAccess(DbContext);
                var lBookingBiz = new BookingBusiness(DbContext, Configuration);

                Booking lBooking = lBookingBiz.Get(aUICredit.OriginalBookingId);
                var lTravelers = lBooking.Trip.Travelers;
                var lAmount = DataHelper.ConvertFromCurrency(aUICredit.Amount);
                foreach (var lTraveler in lTravelers)
                {
                    Credit lCredit = lBooking.Credits.Where(x => x.Traveler.Id == lTraveler.UserID && x.OriginalBookingId == aUICredit.OriginalBookingId).FirstOrDefault();
                    if ( lCredit == null )
                        lCredit = new Credit() { Amount = lAmount/lTravelers.Count() , ContactId = lTraveler.UserID, OriginalBookingId = lBooking.BookingID, Reference = aUICredit.Reference, When = aUICredit.When };
                    else
                    {
                        lCredit.Amount = lAmount;
                        lCredit.When = aUICredit.When;
                    }
                    lCreditDA.Save(lCredit);
                    lBookingBiz.Save(lBooking, new ContactBusiness(DbContext).Get(lBooking.Trip.AgentId) as Agent);
                }

            } catch ( Exception e )
            {
                Logger.LogException($"{FuncName} - Failed to save UICredit", e);
            }
        }
    }
}
