using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.Models;
    
namespace BlitzerCore.Models.UI
{
    public class Panel
    {
        public Panel ( )
        {
            Tiles = new List<Tile>();
            Comparables = new List<Comparable>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<Comparable> Comparables { get; set; }
        public List<Tile> Tiles { get; set; }
    }
}
