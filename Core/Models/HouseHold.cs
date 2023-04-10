using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.Business;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class HouseHold
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public HouseHold()
        {
            Members = new List<Contact>();
        }
        public string Name { get; set; }
        public virtual List<Contact> Members { get; set; }

        public bool AddMember(Contact aNewMember)
        {
            if (Members.Any(x => x.Id == aNewMember.Id) == true)
                return false;

            Members.Add(aNewMember);
            aNewMember.HouseHoldId = Id;
            return true;
        }

        public bool RemoveMember(Contact aContact)
        {
            if (Members.Any(x => x.Id == aContact.Id) == false)
                return false;

            Members.Remove(aContact);
            return true;
        }
    }
}
