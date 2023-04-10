using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models.UI
{
    public class UIFile : BaseUI
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int FileTypeId { get; set; }
        public string FileType { get; set; }
        public string ContactId { get; set; }
        public virtual UIContact Owner { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }
        public string URI { get; set; }
        public int? OpportunityId { get; set; }
        public DateTime Date { get; set; }
        public int BookingTypeId { get; set; }
    }
}
