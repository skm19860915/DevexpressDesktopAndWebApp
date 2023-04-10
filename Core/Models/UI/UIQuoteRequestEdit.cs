using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlitzerCore.Models.UI
{
    public class UIQuoteRequestEdit
    {
        public int Id { get; set; }
        public int QuoteRequestId { get; set; }
        public UIQuoteRequest QuoteRequest { get; set; }
        public string TourOperator { get; set; }
        public int SupplierId { get; set; }
        public int CountryId { get; set; }
        public int? SupplierPageId { get; set; }
        public string SupplierName { get; set; }
        public UIHotel Supplier { get; set; }
        public string RoomType { get; set; }
        public string Total { get; set; }
        public bool IncludesAir { get; set; }
        public string HotelStay { get; set; }
        public string StatusDisplay { get; set; }
        public bool Booked { get; set; }
        public bool Exclude { get; set; }
        public bool Break { get; set; }
        public UICountry CountryData { get; set; }
        public int Count { get; set; }
        public string KingOrQueen { get; set; }
        public string WarningNotice { get; set; }
        public string TO_Initials { get; internal set; }
        public string Transfer { get; set; }
        public string Insurance { get; set; }
        public string Flight_Out_Depart { get; set; }
        public string Flight_Out_Date { get; set; }
        public string Flight_Out_Arrive { get; set; }
        public string Flight_Out_Layover { get; set; }
        public string Flight_Out_Duration { get; set; }
        public string Flight_Out_Numbers { get; set; }
        public string Flight_In_Depart { get; set; }
        public string Flight_In_Date { get; set; }
        public string Flight_In_Arrive { get; set; }
        public string Flight_In_Layover { get; set; }
        public string Flight_In_Duration { get; set; }
        public string Flight_In_Numbers { get; set; }

    }
}
