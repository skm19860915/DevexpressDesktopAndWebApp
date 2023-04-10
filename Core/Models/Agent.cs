using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public enum Roles { Admin, Owner, Manager, IC, Assistant }
    public class Agent : Contact
    {
        public double CommissionRate { get; set; }
        public double Fee { get; set; }
        public Roles Role { get; set; }
        public int? PrimaryTeamId { get; set; }
        [ForeignKey("PrimaryTeamId")]
        public virtual Team PrimaryTeam { get; set; }
        public int? AgentProfileId { get; set; }
        [ForeignKey("AgentProfileId")]
        public virtual AgentProfile Profile { get; set; }
        public virtual ICollection<Contact> Clients { get; set; }

    }
}
