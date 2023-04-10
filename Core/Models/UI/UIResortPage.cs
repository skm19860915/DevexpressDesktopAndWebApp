using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace BlitzerCore.Models.UI
{
    public class UIResortPage : Page
    {
        public UIResortPage()
        {
            Comparables = new List<Comparable>();
        }
        public int? LeftPanelID { get; set; }
        [ForeignKey("LeftPanelID")]    
        public virtual Panel LeftPanel { get; set; }
        public ICollection<Comparable> Comparables { get; set; }
        public int? RightPanelID { get; set; }
        [ForeignKey("RightPanelID")]
        public Panel RightPanel { get; set; } 
        [NotMapped]
        public List<Category> Categories { get; set; }
    }
}
