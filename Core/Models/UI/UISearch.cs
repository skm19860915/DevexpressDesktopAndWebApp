using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Models.UI
{
    public class UISearch
    {
        public IEnumerable<UIContact> Contacts { get; set; }
        public IEnumerable<UIOpportunity> Opportunities { get; set; }
        public IEnumerable<UITrip> Trips { get; set; }
        public string SearchText { get; set; }
    }
}
