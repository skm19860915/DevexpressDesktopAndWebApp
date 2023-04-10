using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class ErrorMsg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Code { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Language { get; set; }
    }
}