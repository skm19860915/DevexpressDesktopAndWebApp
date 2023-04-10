using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BlitzerCore.Models
{
    public class Franchise : Company
    {
        public double CommissionRate { get; set; }  /* 2% means headquarters get 2% */
        public double Fee { get; set; }
        public string FranchiseOwnerId { get; set; }
        [ForeignKey("FranchiseOwnerId")]
        public virtual Agent FranchiseOwner { get; set; }
    }
}
