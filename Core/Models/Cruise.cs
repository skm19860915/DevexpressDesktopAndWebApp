using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Cruise : SKU
    {
        [NotMapped]
        public int CruiseID
        {
            get { return SKUID; }
            set { SKUID = value; }
        }
        [NotMapped]
        public virtual CruiseLine CruiseLine
        {
            get { return Provider as CruiseLine; }
            set { CruiseLine = value; }
        }
    }
}
