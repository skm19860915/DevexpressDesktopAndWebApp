using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace BlitzerCore.Models.UI
{
    public class UIHotel : UICompany
    {
        public UIHotel()
        {
        }
        public double AirportDistance { get; set; }
        public string Promo { get; set; }
        public string PictureURL { get; set; }
        public string Location { get; set; }
        public string AirPortCode { get; set; }
        public bool AdultsOnly { get; set; }
        public bool AllInclusive { get; set; }
    }
}
