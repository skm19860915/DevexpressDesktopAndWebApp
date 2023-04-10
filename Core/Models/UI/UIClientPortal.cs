using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UIClientPortal
    {
        public List<UIQuote> Quotes { get; set; }
        public bool UpdateProfile { get; set; }
        public string UserId { get; set; }
        public List<UIContact> Clients {get; set;}
    }
}
