using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models.UI
{
    public class UIFOP
    {
        public int Id { get; set; }
        public CardTypes CardType { get; set; }
        public string NameOnCard { get; set; }
        public string UserID { get; set; }
        public string Number { get; set; }
        public string Expiration { get; set; }
        public string CVN { get; set; }
        public bool AddressSameAsResidence { get; set; }
    }
}
