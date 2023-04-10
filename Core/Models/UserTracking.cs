using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace BlitzerCore.Models
{
    public class UserTracking
    {
        public UserTracking()
        {
            When = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string Path { get; set; }
        public DateTime When {get; set;}
    }
}
