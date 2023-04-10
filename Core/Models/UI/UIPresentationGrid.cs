using System;
using System.Collections.Generic;
using System.Text;
using static BlitzerCore.Models.Presentation;

namespace BlitzerCore.Models.UI
{
    public class UIPresentationGrid
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string ClientName { get; set; }
        public DateTime Created { get; set; }
        public Statuses Status { get; set; }
    }
}
