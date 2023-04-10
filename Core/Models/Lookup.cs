using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlitzerCore.Models
{
    public class Lookup
    {
        public bool Checked { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
