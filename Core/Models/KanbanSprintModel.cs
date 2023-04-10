using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class KanbanSprintModel
    {
        public Sprint Sprint { get; set; }
        public IEnumerable<TaskStatusTypes> Statuses { get; set; }
        public IEnumerable<UITask> Tasks { get; set; }
        public IEnumerable<Contact> Employees { get; set; }
        public Kanban.Source Source { get; set; }
        public Kanban.ViewMode ViewMode { get; set; }
        public string TaskType { get; set; }
    }
}
