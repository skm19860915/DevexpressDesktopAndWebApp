using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class KanbanSystemModel
    {
        public IEnumerable<SystemStatus> Statuses { get; set; }
        public IEnumerable<UISystem> Systems { get; set; }
        public IEnumerable<Contact> Employees { get; set; }
    }
}
