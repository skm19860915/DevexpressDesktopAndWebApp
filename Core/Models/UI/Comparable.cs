using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    public class Comparable : Block
    {
        public int BaseResortID { get; set; }
        public int CompID { get; set; }  // Comp to the BaseResort
        [ForeignKey("ResortID")]
        public virtual UIResortPage CompPage { get; set; }
        public string TargetPageUrl => "/Resort/Details/" + CompID;
    }
}
