using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Net.Http.Headers;


namespace BlitzerCore.Models
{
    public class AgentProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? DefaultAirPortId { get; set; }
        [ForeignKey("DefaultAirPortId")]
        public virtual AirPort DefaultAirPort { get; set; }
        public int TimeZoneDiff { get; set; }
        public double MonthlyFixedCost { get; set; }
        public Kanban.Source Source { get; set;}
        public Kanban.ViewMode ViewMode { get; set; }
        public string AgentId { get; set; }
        [ForeignKey("AgentId")] 
        public virtual Agent Agent { get; set; }

    }
}
