using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.Helpers;
using BlitzerCore.Business;

namespace BlitzerCore.UIHelpers
{
    public class PaymentUIHelper
    {
        public static UIPayment Convert(Payment aInput)
        {
            var lUIPayment = new UIPayment();
            lUIPayment.BookingId = aInput.BookingID;
            lUIPayment.PaymentId = aInput.PaymentID;
            lUIPayment.Amount = DataHelper.ConvertToCurrency(aInput.Amount);
            lUIPayment.PaymentDate = aInput.PaymentDate;
            lUIPayment.PaymentDateStr = DataHelper.GetDateString(aInput.PaymentDate);
            lUIPayment.PayeeId = aInput.PayeeID;
            lUIPayment.Payee = aInput.Payee?.Name;
            if ( aInput.Card != null )
                lUIPayment.CardID = aInput.Card.Id;
            lUIPayment.Memo = aInput.Memo;
            lUIPayment.Confirmation = aInput.Confirmation;

            if (aInput.CreatedBy != null)
            {
                lUIPayment.CreatedBy = aInput.CreatedBy.Name;
            }
            lUIPayment.CreatedOn = DataHelper.GetDateString(aInput.CreatedOn);
            if (aInput.UpdatedBy != null)
            {
                lUIPayment.UpdatedBy = aInput.UpdatedBy.Name;
            }
            lUIPayment.UpdatedOn = aInput.UpdatedOn.ToString();

            return lUIPayment;
        }

        public static List<UIPayment> Convert(ICollection<Payment> aInput)
        {
            var lOutput = new List<UIPayment>();
            foreach (var lPayment in aInput.Where(x => x.Status != PaymentStatus.Deleted))
                lOutput.Add(Convert(lPayment));
            return lOutput;
        }

        public static Payment Convert(IDbContext aContext, UIPayment aInput)
        {
            var lPayment = new PaymentBusiness(aContext, null).Get(aInput.PaymentId);
            if (lPayment == null)
                lPayment = new Payment();
            lPayment.BookingID = aInput.BookingId;
            lPayment.Amount = DataHelper.ConvertFromCurrency(aInput.Amount);
            lPayment.PaymentDate = aInput.PaymentDate;
            lPayment.Status = aInput.Status;
            lPayment.Confirmation = aInput.Confirmation;
            lPayment.Memo = aInput.Memo;
            lPayment.PayeeID = aInput.PayeeId;
            lPayment.Payee = new ContactBusiness(aContext).Get(aInput.PayeeId);
            if ( aInput.CardID != null )
                lPayment.Card = new FOPBusiness(aContext).Get(aInput.CardID.Value);
            return lPayment;
        }
    }
}
