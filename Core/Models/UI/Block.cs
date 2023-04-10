using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    public class Block
    {
        public int Id { get; set; }
        public string Title { get; set; } // this is seen in the admin screen
        public string BlockTitle { get; set; }  // Display on the screen
        public string Description { get; set; }
        public string Body { get; set; }  // Display on the screen
        public string Caption { get; set; } 
        public string ListTitle { get; set; }
        public bool Published { get; set; }
        public int? BlockToPageMapID { get; set; }
        [ForeignKey("BlockToPageMapID")]
        public virtual BlockToPageMap PageMap { get; set; }   // Points to a single page
        public int? MediaID { get; set; }
        [ForeignKey("MediaID")]
        public virtual Media Media { get; set; }
        public bool HTML { get; set; }
        public int OrderId { get; set; }
    }
}
