using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;
using BlitzerCore.Models;

namespace BlitzerCore.Models
{
    public class KanbanSprintListModel
    {
        public TaskStatusTypes Status { get; set; }
        public IEnumerable<UITask> Tasks { get; set; }
        public string Info { get; set; }
    }
}

