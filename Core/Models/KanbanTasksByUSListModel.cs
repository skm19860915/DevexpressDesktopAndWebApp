using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class KanbanTasksByUSListModel
    {
        public TaskStatusTypes Status { get; set; }
        public IEnumerable<UITask> Tasks { get; set; }
        public string Info { get; set; }
    }
}
