using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public class KanbanFeatureListModel
    {
        public FeatureStatus Status { get; set; }
        public IEnumerable<UIFeature> Features { get; set; }
        public string Info { get; set; }
    }
}
