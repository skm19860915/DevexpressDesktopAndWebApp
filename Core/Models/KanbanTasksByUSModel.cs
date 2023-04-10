using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class KanbanTasksByUSModel
    {
        public UserStory UserStory { get; set; }
        public IEnumerable<TaskStatusTypes> Statuses { get; set; }
        public IEnumerable<UITask> Tasks { get; set; }
        public IEnumerable<Contact> Employees { get; set; }
    }
}
