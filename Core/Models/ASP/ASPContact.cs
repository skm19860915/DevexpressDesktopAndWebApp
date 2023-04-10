using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlitzerCore.Models.ASP
{
    public class ASPContact : UIContact
    {
        public List<SelectListItem> HotelMemberShips { get; set; }
        public List<SelectListItem> CarMemberShips { get; set; }
        public List<SelectListItem> FrequentFlyerMemberShips { get; set; }
    }
}