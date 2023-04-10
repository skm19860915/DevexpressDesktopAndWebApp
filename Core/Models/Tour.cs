using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Tour : SKU
    {
        [NotMapped]
        public int TourID
        {
            get { return SKUID; }
            set { SKUID = value; }
        }
        [NotMapped]
        public virtual TourOperator TourOperator
        {
            get { return Provider as TourOperator; }
            set { TourOperator = value; }
        }
    }
}
