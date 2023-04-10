using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UISKU : BaseUI
    {
        public int Id { get; set; }
        public int ProviderId { get; set; } 
        public string Provider { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public string Description { get; set; }
        public Uri URL { get; set; }
        public int SortOrder { get; set; }
    }
}
