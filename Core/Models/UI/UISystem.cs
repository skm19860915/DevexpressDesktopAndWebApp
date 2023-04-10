using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models.UI
{
    public class UISystem : BaseUI
    {
        public int Id { get; set; }
        public string OwnerID { get; set; }
        public virtual UIContact Owner { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public SystemStatus Status { get; set; }
        public OperationalStatus OperationalStatus { get; set; }
        public string KanbanColor { get; set; }
        public bool isActive { get; set; }
        public List<UIFeature> Features { get; set; }
    }
}
