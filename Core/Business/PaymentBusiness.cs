using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using Microsoft.Extensions.Configuration;

namespace BlitzerCore.Business
{
    public class PaymentBusiness
    {
        const string ClassName = "PaymentBusiness::";
        private IDbContext DbContext { get; set; }
        private ContactDataAccess DataAccess { get; set; }
        public IConfiguration Configuration { get; }
        public PaymentBusiness(IDbContext mContext,IConfiguration aConfiguration)
        {
            this.DbContext = mContext;
            DataAccess = new ContactDataAccess(mContext);
            Configuration = aConfiguration;
        }

        public UIPayment Create ( Booking aBooking )
        {
            var lPayment = new Payment() { BookingID = aBooking.BookingID, PaymentDate = DateTime.Now };
            lPayment.Amount = aBooking.Amount - aBooking.Payments.Where(x=>x.Status != PaymentStatus.Deleted && x.Status != PaymentStatus.Declined).Sum(x => x.Amount);
            var lUIPayment = PaymentUIHelper.Convert(lPayment);
            return lUIPayment;
        }

        public Payment Get(int aID)
        {
            return new PaymentDataAccess(DbContext).Get(aID);
        }

        public Payment Save(UIPayment aPayment, Contact aContact)
        {
            string FuncName = ClassName + $"Save(UIPayment = ${aPayment.PayeeId})";
            Logger.EnterFunction(FuncName);
            try
            {
                // Check if there is a new card for the traveler
                FOP lNewCreditCard = SaveNewCreditCard(aPayment, aContact);
                aPayment = ProcessRefundRequest(aPayment);
                var lPayment = PaymentUIHelper.Convert(DbContext, aPayment);
                if (lNewCreditCard != null)
                {
                    lPayment.FOPId = lNewCreditCard.Id;
                    Logger.LogInfo("Created new Credit Card");
                }
                else
                    Logger.LogInfo("Processing existing Credit Card");

                //var lBotPaid = MakeBotPayment(lPayment);
                var lBotPaid = true;
                if (lBotPaid == true)
                {
                    var lSaveResult = Save(lPayment, aContact);
                    // get the booking for the payment and save that because might have to update the status
                    new BookingBusiness(DbContext, Configuration).Save(lPayment.Booking, aContact);
                }
                {
                    Logger.LogInfo($"{FuncName} didn't make payment because Bot failed with automated payment");
                }
                return lPayment;
            }
            catch (BlitzerCore.Models.Exceptions.BookingDoesnotExist)
            {
                throw new BlitzerCore.Models.Exceptions.BookingDoesnotExist();
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + "Failed to Save Payment", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return null;
        }

        private bool MakeBotPayment(Payment aPayment)
        {
            string FuncName = ClassName + $"MakeBotPayment(TO = ${aPayment.Amount} Booking ID = {aPayment.BookingID}) ";
            IWebTravelSrv lWebBot = null;
            try
            {
                aPayment.Booking = new BookingBusiness(DbContext, Configuration).Get(aPayment.BookingID);
                lWebBot = GetWebBot(aPayment.Booking);
                if (lWebBot == null)
                {
                    Logger.LogInfo(FuncName + $"No Bot available to make payment");
                    return false;
                }

                return lWebBot.MakePayment(aPayment);
            }
            catch (Exception)
            {

            }
            finally
            {
                lWebBot.Close();
            }

            return false;
        }

        public IWebTravelSrv GetWebBot(Booking aBooking)
        {
            if (aBooking.TourOperatorID == null)
                return null;

            var lWebBot = new BlitzerBusiness(DbContext, Configuration).GetWebService(aBooking.TourOperator);
            return lWebBot;
        }
        private UIPayment ProcessRefundRequest(UIPayment aPayment)
        {
            // Don't process new payments
            if (aPayment.PaymentId == 0 || aPayment.Status == PaymentStatus.New)
                return aPayment;

            // Do not process updates Refunds
            var lOriginal = Get(aPayment.PaymentId);
            if (lOriginal.Status == aPayment.Status)
                return aPayment;

            // Create duplicate Payment Record and reverse the amount
            UIPayment lPayment = Duplicate(aPayment);
            var lStep1 = DataHelper.ConvertFromCurrency(lPayment.Amount);
            var lStep2 = lStep1 * -1;
            lPayment.Amount =  DataHelper.ConvertToCurrency(lStep2);
            return lPayment;
        }

        private UIPayment Duplicate(UIPayment aPayment)
        {
            UIPayment lDuplicate = new UIPayment();
            lDuplicate.Amount = aPayment.Amount;
            lDuplicate.CreditCard = aPayment.CreditCard;
            lDuplicate.PayeeId = aPayment.PayeeId;
            lDuplicate.Expiration = aPayment.Expiration;
            lDuplicate.BookingId = aPayment.BookingId;
            lDuplicate.CardID = aPayment.CardID;
            lDuplicate.PaymentDate = aPayment.PaymentDate;
            lDuplicate.Confirmation = aPayment.Confirmation;
            lDuplicate.Expiration = aPayment.Expiration;
            lDuplicate.Memo = aPayment.Memo;
            lDuplicate.Status = aPayment.Status;
            lDuplicate.returnUrl = aPayment.returnUrl;
            lDuplicate.CreditCard = aPayment.CreditCard;
            lDuplicate.Description = aPayment.Description;
            return lDuplicate;
        }

        private FOP SaveNewCreditCard(UIPayment aPayment, Contact aAgent)
        {
            FOP lNewCreditCard = null;
            Payment lPayment = PaymentUIHelper.Convert(DbContext, aPayment);
            if (aPayment.CreditCard != null && aPayment.CreditCard.Length > 8)
            {
                var lNewUICreditCard = FOPUIHelper.Convert(new FOPBusiness(DbContext).Create(aPayment.PayeeId, aAgent));
                lNewUICreditCard.Number = aPayment.CreditCard;
                lNewUICreditCard.Expiration = aPayment.Expiration;
                lNewUICreditCard.CVN = aPayment.Code;
                return new FOPBusiness(DbContext).Save(lNewUICreditCard, aAgent);
            }

            return lNewCreditCard;
        }

        public int Save(Payment aPayment, Contact aUser)
        {
            string FuncName = ClassName + $"Save (Payment = {aPayment.PaymentID })";
            try
            {
                Logger.EnterFunction(FuncName);
                Validate(aPayment);
                UpdateTracking(aPayment, aUser);
                var lCnt = new PaymentDataAccess(DbContext).Save(aPayment);
                var lBooking = new BookingBusiness(DbContext, Configuration).Get(aPayment.BookingID);
                new TripBusiness(DbContext).Update(lBooking.TripID);
                return lCnt;
            }
            catch ( Exception e )
            {
                Logger.LogException("Failed to save Payment", e);
            } finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return 0;
        }

        private void Validate(Payment aPayment)
        {
            string FuncName = ClassName + $"Validate (Payment = {aPayment.PaymentID })";
            if ( Math.Abs(aPayment.Amount) < .01)
            {
                string lError = "Payment amount is required to save a payment";
                Logger.LogError(FuncName + " " + lError);
                throw new InvalidDataException(lError);
            }
        }

        private static void UpdateTracking(Payment aPayment, Contact aUser)
        {
            if (aPayment.BookingID == 0)
            {
                aPayment.CreatedById = aUser.Id;
                aPayment.CreatedOn = DateTime.Now;
            }
            aPayment.UpdatedById = aUser.Id;
            aPayment.UpdatedOn = DateTime.Now;
        }
    }
}
