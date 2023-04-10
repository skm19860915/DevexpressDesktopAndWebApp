using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UITeam
    {
        public int Id { get; set; }

        public bool Active { get; set; }
        public virtual List<UIContact> Employees { get; set; }
        public string FullName { get; set; }
        public bool Internal { get; set; }
        public int PrimaryId { get; set; }
        public virtual UIContact Primary { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
