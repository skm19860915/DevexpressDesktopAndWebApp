using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UITaskTemplate
    {
        public int ID { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public int? FromStartDate { get; set; }
        public int? FromEndDate { get; set; }
        public bool Opportunity { get; set; }
    }
}
