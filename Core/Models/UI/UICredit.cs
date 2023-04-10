using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BlitzerCore.Models.UI
{
    public class UICredit
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Traveler { get; set; }
        public string Amount { get; set; }
        [DataType(DataType.Date)]
        [Display(Name="Expires On")]
        public DateTime When { get; set; }
        public int OriginalBookingId { get; set; }
    }
}
