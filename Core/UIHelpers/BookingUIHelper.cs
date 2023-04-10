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
    public class BookingUIHelper
    {
        public static Booking Convert( IDbContext aContext, UIBooking aBooking)
        {
            Booking lOutput = new BookingBusiness(aContext, null).Get(aBooking.BookingID);
            if (lOutput == null)
            {
                lOutput = new Booking();
                lOutput.Trip = new TripBusiness(aContext).Get(aBooking.TripID);
                if ( aBooking.SupplierId != null )  
                  lOutput.Supplier = new CompanyBusiness(aContext).Get(aBooking.SupplierId.Value);
                if (aBooking.TourOperatorID != null)
                    lOutput.TourOperator = new CompanyBusiness(aContext).Get(aBooking.TourOperatorID.Value) as TourOperator;
            }
            lOutput.Amount = DataHelper.ConvertFromCurrency(aBooking.Amount);
            lOutput.Received = DataHelper.ConvertFromCurrency(aBooking.Received);
            lOutput.BookingNumber = aBooking.BookingNumber;
            lOutput.GrossCommission = DataHelper.ConvertFromCurrency(aBooking.GrossCommission);
            lOutput.Deposit = DataHelper.ConvertFromCurrency(aBooking.Deposit);
            lOutput.DepositDueDate = aBooking.DepositDueDate;
            lOutput.PaymentDate = aBooking.PaymentDate;
            lOutput.SupplierId = aBooking.SupplierId;
            lOutput.FinalPayment = aBooking.FinalPayment;
            lOutput.TourOperatorID = aBooking.TourOperatorID;
            lOutput.Status = aBooking.Status;
            lOutput.TripID = aBooking.TripID;
            lOutput.BookingID = aBooking.BookingID;
            lOutput.Memo = aBooking.Memo;
            return lOutput;
        }

        public static List<UIBooking> Convert(IEnumerable<Booking> aBookings)
        {
            var lUIBookings = new List<UIBooking>();
            if (aBookings == null)
                return lUIBookings;

            foreach (var lBooking in aBookings)
                lUIBookings.Add(Convert(lBooking));

            return lUIBookings;
        }

        public static UIBooking Convert(Booking aBooking)
        {
            UIBooking lOutput = new UIBooking();
            lOutput.Amount = DataHelper.ConvertToCurrency(aBooking.Amount);
            lOutput.Deposit = DataHelper.ConvertToCurrency(aBooking.Deposit);
            lOutput.BookingNumber = aBooking.BookingNumber;
            lOutput.Received = DataHelper.ConvertToCurrency(aBooking.Received);
            lOutput.GrossCommission = DataHelper.ConvertToCurrency(aBooking.GrossCommission);
            lOutput.HQCommission = DataHelper.ConvertToCurrency(aBooking.HQCommission);
            lOutput.ICCommission = DataHelper.ConvertToCurrency(aBooking.ICCommission);
            lOutput.HostAgencyCommission = DataHelper.ConvertToCurrency(aBooking.HostAgencyCommission);
            lOutput.FinalPaymentStr = DataHelper.GetDateString(aBooking.FinalPayment);
            lOutput.FinalPayment = aBooking.FinalPayment;
            lOutput.SupplierId = aBooking.SupplierId;
            if ( aBooking.SupplierId != null )
                lOutput.Supplier = aBooking.Supplier?.Name;
            lOutput.Agent = aBooking.Trip.Agent.Name;
            lOutput.TourOperatorID = aBooking.TourOperatorID;
            lOutput.TourOperatorName = aBooking.TourOperator?.Name;
            lOutput.TripID = aBooking.TripID;
            lOutput.TripName = aBooking.Trip.Name;
            lOutput.Status = aBooking.Status;
            lOutput.EndDate = DataHelper.GetDateString( aBooking.Trip.EndDate);
            lOutput.Payments = PaymentUIHelper.Convert(aBooking.Payments);
            lOutput.Credits = CreditUIHelper.Convert(aBooking.Credits);
            lOutput.Balance = DataHelper.ConvertToCurrency(BookingBusiness.Balance(aBooking));
            lOutput.Paid = DataHelper.ConvertToCurrency(BookingBusiness.Paid(aBooking));
            lOutput.TargetPayment = aBooking.TargetPayment;
            lOutput.SettlementAge = aBooking.SettlementAge;
            lOutput.DepositDueDate = aBooking.DepositDueDate;
            lOutput.PaymentDate = aBooking.PaymentDate;
            lOutput.BookingID = aBooking.BookingID;
            lOutput.Memo = aBooking.Memo;
            if (aBooking.CreatedBy != null)
            {
                lOutput.CreatedBy = aBooking.CreatedBy.Name;
            }
            lOutput.CreatedOn = DataHelper.GetDateString(aBooking.CreatedOn);
            if (aBooking.UpdatedBy != null)
            {
                lOutput.UpdatedBy = aBooking.UpdatedBy.Name;
            }
            lOutput.UpdatedOn = aBooking.UpdatedOn.ToString();
            return lOutput;
        }

    }
}
