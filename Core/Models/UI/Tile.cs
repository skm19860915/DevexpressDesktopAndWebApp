using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    public enum TileSubPageTypes {  Gallery }

    public class Tile : Block
    {
        public int? ResortID { get; set; }
        [ForeignKey("ResortID")]
        public virtual Page ResortPage { get; set; }
        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        public string TargetPageUrl => "/ResortPicGallery?ResortID="+ResortID+"&CategoryID=" + CategoryID;
    }
}
