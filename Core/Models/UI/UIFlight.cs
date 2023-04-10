using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UIFlight
    {
        public int Id { get; set; }
        public int out_Id { get; set; }
        public string Out_Carrier { get; set; }
        public string Out_DepartLocation { get; set; }
        public string Out_ArriveLocation { get; set; }
        public string Out_DepartTime { get; set; }
        public string Out_ArriveTime { get; set; }
        public string Out_Flight { get; set; }
        public string Out_ConnectionAirport { get; set; }
        public string In_DepartLocation { get; set; }
        public string In_ArriveLocation { get; set; }
        public int In_Id { get; set; }
        public string In_Carrier { get; set; }
        public string In_DepartTime { get; set; }
        public string In_ArriveTime { get; set; }
        public string In_Flight { get; set; }
        public string In_ConnectionAirport { get; set; }
        public string ExtraCost { get; set; }
    }
}
