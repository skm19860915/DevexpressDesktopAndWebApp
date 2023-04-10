using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.UIHelpers
{
    public class InvoiceUIHelper
    {
        public static UIInvoice Convert(Invoice aInvoice)
        {
            var lOutput = new UIInvoice()
            {
                Id = aInvoice.Id,
                InvoiceDate = aInvoice.InvoiceDate,
                Trip = TripUIHelper.Convert(aInvoice.Trip),
                Client = ContactUIHelper.Convert(aInvoice.Client)
            };            

            return lOutput;
        }
    }
}
