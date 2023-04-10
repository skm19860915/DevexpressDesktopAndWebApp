using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class KanbanRequirementListModel
    {
        public Feature Feature { get; set; }
        public UserStoryStatus Status { get; set; }
        public IEnumerable<UIUserStory> UserStories { get; set; }
        public IEnumerable<Contact> Employees { get; set; }
    }
}
