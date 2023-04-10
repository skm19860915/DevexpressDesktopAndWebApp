using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UIQuoteRequestWrapper
    {
        public string BackgroundImage { get; set; }
        public string BackgroundCaption { get; set; }
        public string BackgroundTitle { get; set; }

        public string Start { get; set; }
        public string End { get; set; }
        public string People { get; set; }
        public string Rooms { get; set; }
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
        public List<UIQuoteRequestEdit> Quotes { get; set; }
        public string DepartureCityCode { get; set; }
        public string DestinationCityCode { get; set; }
        public bool isAgent { get; set; }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
