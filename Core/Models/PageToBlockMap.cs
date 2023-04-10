using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models.UI;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class PageToBlockMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        //public int PageId { get; set; }
        //[ForeignKey("PageId")]
        //public virtual Page Page { get; set; }
        public int BlockId { get; set; }
        [ForeignKey("BlockId")]    
        public virtual Block Block { get; set; }

    }
}
