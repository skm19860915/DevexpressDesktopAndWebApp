using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class PresentationQueueItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int WebPageId { get; set; }
        public virtual WebPage WebPage { get; set; }
        public int Location { get; set; }   
        public int PresentationId { get; set; }
        [ForeignKey("PresentationId")]
        public virtual Presentation Presentation { get; set; }
    }
}
