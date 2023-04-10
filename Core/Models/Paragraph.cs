using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzerCore.Models
{
    public class Paragraph
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
