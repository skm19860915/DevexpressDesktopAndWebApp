using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class Sprint
    {
        public enum StatusTypes
        {
            Current,
            Future,
            Completed,
            Deleted
        }

        public Sprint()
        {
        }

        [Key]
        public int Id { get; set; }
        public string OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public virtual Contact Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<UserStory> UserStories { get; set; }
        public Sprint.StatusTypes Status { get; set; }
        public bool isActive
        {
            get
            {
                if (Status == StatusTypes.Current || Status == StatusTypes.Future)
                    return true;

                return false;
            }
        }
    }
}
