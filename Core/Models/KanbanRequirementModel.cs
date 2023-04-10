using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class KanbanRequirementModel
    {
        public Feature Feature { get; set; }
        public IEnumerable<UserStoryStatus> Statuses { get; set; }
        public IEnumerable<UIUserStory> UserStories { get; set; }
        public IEnumerable<Contact> Employees { get; set; }
    }
}
