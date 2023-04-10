using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class KanbanFeatureModel
    {
        public BlitzSystem System { get; set; }
        public IEnumerable<FeatureStatus> Statuses { get; set; }
        public IEnumerable<UIFeature> Features { get; set; }
        public IEnumerable<Contact> Employees { get; set; }
    }
}
