using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BlitzerCore.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Team()
        {
            Active = true;
            Agents = new List<Agent>();
        }

        public bool Active { get; set; }
        public virtual List<Agent> Agents { get; set; }
        public string Name { get; set; }
        public string Email {
            get {
                var lEmail = Agents.SelectMany(x => x.Emails).FirstOrDefault(y => y.Preferred == true);
                if (lEmail != null)
                    return lEmail.Address;
                return "";
            }
        }
        public string PhoneNumber
        {
            get
            {
                var PhoneNumber = Agents.SelectMany(x => x.PhoneNumbers).FirstOrDefault(y => y.Defaut == true);
                if (PhoneNumber != null)
                    return PhoneNumber.PhoneNumber;
                return "";
            }
        }
        public override string ToString() {return this.Name; }
        public bool contains(string aEmpID) { return Agents.Any(x => x.Id == aEmpID); }       
        public string PrimaryId { get; set; }
        [ForeignKey("PrimaryId")]
        public virtual Agent Primary { get; set; }
        public DateTime LastUpdated { get; set; }
        public void copy(Team passedTeam)
        {
            Name = passedTeam.Name;
            PrimaryId = passedTeam.PrimaryId;
            Active = passedTeam.Active;
            LastUpdated = DateTime.Now;
        }

    }
}
