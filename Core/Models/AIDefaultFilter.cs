using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class AIDefaultFilter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int AirPortID { get; set; }
        [ForeignKey("AirPortID")]
        public virtual AirPort AirPort { get; set; }

        public string AgentId { get; set; }
        [ForeignKey("AgentId")]
        public virtual Agent Agent { get; set; }
        public int? OutBoundFlightFilter { get; set; }
        public int? InBoundFlightFilter { get; set; }
        public int? AirFlightFilter { get; set; }
        public int? ResortFilter { get; set; }
    }
}
