using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models.UI
{
    public class UISprint : BaseUI
    {
        public int Id { get; set; }
        public string OwnerID { get; set; }
        public virtual UIContact Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public string StartStr { get; set; }
        public DateTime End { get; set; }
        public string EndStr { get; set; }
        public Sprint.StatusTypes Status { get; set; }
        public bool isActive { get; set; }
        public List<UIUserStory> UserStories { get; set; }
    }
}
