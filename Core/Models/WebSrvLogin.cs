using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BlitzerCore.Models
{
    public class WebSrvLogin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WebSrvLoginID { get; set; }
        public string AgentId { get; set; }
        [ForeignKey("AgentId")]
        public virtual Agent Agent { get; set; }
        public int TourOperatorID { get; set; }
        [ForeignKey("TourOperatorID")]
        public virtual TourOperator TourOperator { get; set; }
        public string UserName { get; set;  }
        public string Password { get; set; }
    }
}
