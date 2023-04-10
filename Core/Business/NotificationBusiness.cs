using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Helpers;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business
{
    public class NotificationBusiness
    {
        public const string ClassName = "NotificationBusiness::";
        private IDbContext DbContext { get; set; }
        IConfiguration Configuration { get; }

        public NotificationBusiness(IDbContext mContext, IConfiguration aConfig)
        {
            DbContext = mContext;
            Configuration = aConfig;
        }

        public int SendFinalPayment(Trip aTrip, Agent aAgent)
        {
            string FuncName = ClassName + $"SendFinalPayment (TridId = {aTrip.ID})";
            if (aTrip == null)
                return -1;
            int lRetVal = 0;

            try
            {
                // Get the Email Addresses
                var lEmailAddrs = GetEmailAddress(aTrip);
                var lTravelers = aTrip.Travelers.Select(x => x.User).ToList();

                // Does the user have a credit card
                var lBalance = aTrip.Balance;
                if (lBalance < 1)
                {
                    Logger.LogInfo($"{FuncName} - Not sending final payment because there is no payment due");
                    return 0;
                }

                FOP lCreditCard = null;
                foreach (var lTraveler in lTravelers)
                {
                    var lCards = new ContactBusiness(DbContext).GetCCs(lTraveler);
                    if (lCards != null && lCards != null && lCards.Count() > 0 )
                    {
                        lCreditCard = lCards.FirstOrDefault();
                        break;
                    }
                }

                var lAgentName = aAgent?.Name;

                if (lCreditCard != null)
                    lRetVal = SendFinalPaymentEmail(aTrip, lTravelers, lEmailAddrs, lBalance, lCreditCard);
                else
                    lRetVal = SendFinalPaymentCreditCardEmail(aTrip, lTravelers, lEmailAddrs, lBalance, lCreditCard);
            } catch ( Exception e ) 
            {
                Logger.LogException($"{FuncName} Exception processing SendFinalPayment", e);
            } finally
            {
                Logger.LogInfo($"{FuncName} Finishing Sending final payment email");
            }

            try
            {
                Note lNote = new Note()
                {
                    Memo = "Sent Final Payment Notice",
                    OpportunityId = aTrip.ID,
                    When = DateTime.Now,
                    Where = "",
                    Who = null,
                    WriterId = aAgent.Id
                };
                new TripBusiness(DbContext).SaveNote(lNote, aTrip);
            } finally
            {
                Logger.LogInfo($"{FuncName} Finishing adding final payment note");
            }

            return lRetVal;
        }

        private int SendFinalPaymentCreditCardEmail(Trip aTrip, List<Contact> aContacts, List<string> aEmailAddrs, double aBalance, FOP aCreditCard)
        {
            string FuncName = ClassName + $"SendFinalPaymentCreditCardEmail ()";
            var lCBiz = new ContactBusiness(DbContext);

            if (aEmailAddrs.Count() < 1)
            {
                Logger.LogError($"{FuncName} - Not sending final payment because there are no emails");
                return -1;
            }

            if (aContacts.Count() < 1)
            {
                Logger.LogError($"{FuncName} - Not sending final payment because there are no contacts");
                return -1;
            }

            Booking lBooking = GetBooking(aTrip);
            if (lBooking == null)
                return -1;

            string lHeader = "Final Payment : Outstanding balance due by " + DataHelper.GetDateString(lBooking.FinalPayment);

            var lCCList = new List<string>() { "Info@eze2Travel.com" };

            string lGreeting = "Hello " + aContacts[0].First;
            if (aContacts.Count > 1)
                lGreeting += " and " + aContacts[1].First;
            lGreeting += CoreEmailHelper.NewLine;
            lGreeting += CoreEmailHelper.NewLine;

            string lBalance = DataHelper.ConvertToCurrency(aTrip.Balance);
            string lFinalPaymentDate = DataHelper.GetDateString(lBooking.FinalPayment);
            string lLink = lCBiz.GetPortalLink(aContacts[0], Configuration, "Portal");
            string lBody = $"Please login to your Eze2Travel {lLink} and input your credit card and authorize Eze2Travel to process the final payment of {lBalance} or give us a call at (919) 815-0200 to arrange final payment";

            var lMsg = lGreeting + lBody + lCBiz.GetEmailFooter();

            var lEmailer = new CoreEmailHelper(Configuration);
            lEmailer.SendEmail(aEmailAddrs, lCCList, lHeader, lMsg);
            Logger.LogInfo($"{FuncName} - Sent final Credit Card payment email");
            return 1;
        }

        private Booking GetBooking(Trip aTrip)
        {
            string FuncName = ClassName + $"GetBooking () - ";
            if ( aTrip.Bookings == null || aTrip.Bookings.Count() == 0 )
            {
                Logger.LogWarning($"{FuncName}There is not booking for trip : " + aTrip.Name);
                return null;
            }

            return aTrip.Bookings.Where(x => x.Amount - x.Payments.Sum(y => y.Amount) > 0).FirstOrDefault();
        }

        private int SendFinalPaymentEmail(Trip aTrip,  List<Contact> aContacts, List<string> aEmailAddrs, double aBalance, FOP aCreditCard)
        {
            string FuncName = ClassName + $"SendFinalPaymentEmail ()";
            var lCBiz = new ContactBusiness(DbContext);

            if (aCreditCard == null || aBalance < 1 || aEmailAddrs.Count() < 1)
            {
                Logger.LogInfo($"{FuncName} - Not sending final payment because hit error");
                return -1;
            }

            string lHeader = "Final Payment : Request to charge outstanding balance";

            var lCCList = new List<string>() { "Info@eze2Travel.com" };

            string lGreeting = "Hello " + aContacts[0].First;
            if (aEmailAddrs.Count > 1)
                lGreeting += " and " + aContacts[1].First;
            lGreeting += CoreEmailHelper.NewLine;
            lGreeting += CoreEmailHelper.NewLine;

            string lBalance = DataHelper.ConvertToCurrency(aTrip.Balance);
            string lLast4 = aCreditCard.Number.Substring(aCreditCard.Number.Count() - 4);
            string lFinalPaymentDate = DataHelper.GetDateString ( aTrip.Bookings.FirstOrDefault().FinalPayment);
            string lDeposit = DataHelper.ConvertToCurrency(TripBusiness.Payments(aTrip));

            string lTOs = "";
            foreach ( var lTo in aTrip.Bookings)
            {
                if (lTOs == "")
                    lTOs = lTo.TourOperator.Name;
                else
                    lTOs += " and " + lTo.TourOperator.Name;
            }

            string lBody = $"Do you authorize Eze2Travel to process the final payment to {lTOs} for {lBalance} to your credit card ending in {lLast4}?  If you wish to use another card, please contact the office at (919) 815-0200. If payment is not processed by 5pm on {lFinalPaymentDate}, the deposit of {lDeposit} will be forfeited.";

            var lMsg = lGreeting + lBody + lCBiz.GetEmailFooter();

            var lEmailer = new CoreEmailHelper(Configuration);
            lEmailer.SendEmail(aEmailAddrs, lCCList, lHeader, lMsg);
            Logger.LogInfo($"{FuncName} - Sent final payment email");
            return 1;
        }

        internal int SendNewTask(Task aTask)
        {
            string FuncName = ClassName + $"SendNewTask ()";
            var lCBiz = new ContactBusiness(DbContext);
            var lIssuer = lCBiz.Get(aTask.IssuerID);
            var lOwner = lCBiz.Get(aTask.OwnerID);

            if (aTask == null || lIssuer == null)
            {
                Logger.LogInfo($"{FuncName} - Not sending final payment because hit error");
                return -1;
            }

            string lHeader = $"New Task : {aTask.Name}";

            string lGreeting = $"Hello {lOwner.First},";
            lGreeting += CoreEmailHelper.NewLine;
            lGreeting += CoreEmailHelper.NewLine;
            string lFirst = lIssuer.First;
            string lBaseUrl = Configuration["appSettings:SiteUrl"];
            string lTaskId = aTask.Id.ToString();

            string lBody = $"{lFirst} assigned you a new task <a href=\"{lBaseUrl}/Task/Edit/{lTaskId}\">task</a>.  Please review when possible";

            var lMsg = lGreeting + lBody + lCBiz.GetEmailFooter();

            var lEmailer = new CoreEmailHelper(Configuration);
            lEmailer.SendEmail(new List<string>() { lOwner.PrimaryEmail }, new List<string>(), lHeader, lMsg, false);
            Logger.LogInfo($"{FuncName} - Sent new task email to {lOwner.PrimaryEmail}");
            return 1;
        }

        internal int SendTaskComplete(Task aTask, string aIssuerId, string aOwnerId)
        {
            string FuncName = ClassName + $"SendTaskComplete ()";
            var lCBiz = new ContactBusiness(DbContext);
            var lTarget = lCBiz.Get(aIssuerId);
            var lOwner = lCBiz.Get(aOwnerId);

            if (aTask == null ||lTarget == null)
            {
                Logger.LogInfo($"{FuncName} - Not sending final payment because hit error");
                return -1;
            }

            string lHeader = $"Task Completed : {aTask.Name}";

            string lGreeting = $"Hello {lTarget.First},";
            lGreeting += CoreEmailHelper.NewLine;
            lGreeting += CoreEmailHelper.NewLine;
            string lFirst = lOwner.First;
            string lBaseUrl = Configuration["appSettings:SiteUrl"];
            string lTaskId = aTask.Id.ToString();

            string lBody = $"{lFirst} completed the <a href=\"{lBaseUrl}/Task/Edit/{lTaskId}\">task</a>.  Please review task and market complete";

            var lMsg = lGreeting + lBody + lCBiz.GetEmailFooter();

            var lEmailer = new CoreEmailHelper(Configuration);
            lEmailer.SendEmail(new List<string>() { lTarget.PrimaryEmail }, new List<string>(), lHeader, lMsg, false);
            Logger.LogInfo($"{FuncName} - Sent completed task email");
            return 1;
        }

        private List<string> GetEmailAddress(Trip aTrip )
        {
            if (aTrip == null || aTrip.Travelers == null || aTrip.Travelers.Count() == 0 )
                return new List<string>();

            return aTrip.Travelers.Select(x => x.User.PrimaryEmail).ToList();
        }

        private List<string> GetEmailAddress(Agent aAgent)
        {
            if (aAgent == null )
                return new List<string>();

            return new List<string>() { aAgent.PrimaryEmail };
        }

        public int SendPayment(Payment aPayment)
        {
            if (aPayment == null)
                return -1;

            string FuncName = ClassName + $"SendPayment (Payment = {aPayment.PaymentID})";
            int lRetVal = 0;

            var lBooking = aPayment.Booking;
            var lTrip = lBooking.Trip;
            var lBody = "";

            try
            {
                // Get the Email Addresses
                var lEmailAddrs = GetEmailAddress(lTrip.Agent);
                var lAmount = DataHelper.ConvertToCurrency(aPayment.Amount);
                var lFinalPayment = DataHelper.GetDateString(lBooking.FinalPayment);

                string lHeader = $"ACTION REQUIRED : Make payment on {lTrip.Name} for {lAmount}" ;

                var lCCList = new List<string>() { "Support@eze2Travel.com" };

                string lGreeting = "Hello " + lTrip.Agent.First + ",";
                lGreeting += CoreEmailHelper.NewLine;
                lGreeting += CoreEmailHelper.NewLine;

                lBody = $"{aPayment.Payee.First} just authorized a payment of {lAmount} to the {lTrip.Name} trip.  Finaly payment is due on the {lFinalPayment}";

                var lMsg = lGreeting + lBody ;

                var lEmailer = new CoreEmailHelper(Configuration);
                lEmailer.SendEmail(lEmailAddrs, lCCList, lHeader, lMsg);
                Logger.LogInfo($"{FuncName} - Sent payment email");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Exception processing SendFinalPayment", e);
            }
            finally
            {
                Logger.LogInfo($"{FuncName} Finishing Sending final payment email");
            }

            try
            {
                Note lNote = new Note()
                {
                    Memo = lBody,
                    OpportunityId = lTrip.ID,
                    When = DateTime.Now,
                    Where = "Portal",
                    Who = aPayment.Payee.First,
                    WriterId = lTrip.AgentId
                };
                new TripBusiness(DbContext).SaveNote(lNote, lTrip);
            }
            finally
            {
                Logger.LogInfo($"{FuncName} Finishing adding final payment note");
            }

            return lRetVal;
        }
    }
}
