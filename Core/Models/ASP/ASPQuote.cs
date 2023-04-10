using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlitzerCore.Models.ASP
{
    public class ASPQuote : UIQuote
    {
        public List<SelectListItem> Resorts { get; set; }
        public List<SelectListItem> ResortRoomTypes { get; set; }
        public List<SelectListItem> TourOperators { get; set; }
    }
}
