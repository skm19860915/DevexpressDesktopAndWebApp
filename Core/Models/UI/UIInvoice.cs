using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UIInvoice
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int TripID { get; set; }
        public UITrip Trip { get; set; }
        public string ClientId { get; set; }
        public UIContact Client { get; set; }
    }
}
