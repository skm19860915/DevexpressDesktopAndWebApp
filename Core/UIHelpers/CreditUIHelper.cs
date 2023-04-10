using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;

namespace BlitzerCore.UIHelpers
{
    public class CreditUIHelper
    {
        public static List<UICredit> Convert(List<Credit> aCredits)
        {
            var lOutput = new List<UICredit>();
            if (aCredits == null)
                return lOutput;

            foreach (var lCredit in aCredits)
                lOutput.Add(Convert(lCredit));
            return lOutput;
        }

        public static UICredit Convert(Credit aCredit)
        {
            if (aCredit == null)
                return null;

            var lUICredit = new UICredit();
            lUICredit.Traveler = aCredit.Traveler.Id;
            lUICredit.OriginalBookingId = aCredit.OriginalBookingId;
            lUICredit.Id = aCredit.Id;
            lUICredit.Reference = aCredit.Reference;
            lUICredit.Traveler = aCredit.Traveler.Name;
            lUICredit.Amount = DataHelper.ConvertToCurrency(aCredit.Amount);
            lUICredit.When = aCredit.When;
            return lUICredit;
        }
    }
}
