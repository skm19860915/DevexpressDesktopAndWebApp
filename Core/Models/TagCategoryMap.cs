using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BlitzerCore.Models
{
    public class TagCategoryMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
        public int TagCategoryId { get; set; }
        [ForeignKey("TagCategoryId")]
        public virtual TagCategory TagCategory { get; set; }
    }
}
