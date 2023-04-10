
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Client : Contact
    {
        public Client()
        {
            Emails = new List<Email>();
            PhoneNumbers = new List<Phone>();
        }

        public bool PendingMerchantApproval { get; set; }
        public bool isMerchant { get; set; }
        public string Message { get; set; }
        public bool Primary { get; set; }
        public int? RelationshipID { get; set; }
        [ForeignKey("RelationshipID")]
        public virtual Relationship Relationship { get; set; }
    }
}

