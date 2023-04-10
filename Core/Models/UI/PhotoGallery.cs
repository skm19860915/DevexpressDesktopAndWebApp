using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzerCore.Models.UI
{
    public class PhotoGallery
    {
        public string Header { get; set; }
        public IEnumerable<BlitzerCore.Models.UI.Media> Photos { get; set; }
        public int NextResortID { get; set; }
    }
}
