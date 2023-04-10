using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class KanbanSystemListModel
    {
        public SystemStatus Status { get; set; }
        public IEnumerable<UISystem> Systems { get; set; }
        public string Info { get; set; }
    }
}
