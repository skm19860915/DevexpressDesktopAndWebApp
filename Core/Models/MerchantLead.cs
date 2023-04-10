using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class MerchantLead : Client
    {
        public MerchantLead() { }

        public MerchantLead(RegisterMerchantModel aModel )
        {
            throw new NotImplementedException();

            //MerchantName = aModel.CompanyName;
            ////PhoneNumber = aModel.PhoneNumber;
            //Emails = aModel.Emails;
            //FirstName = aModel.FirstName;
            //LastName = aModel.LastName;
        }

        internal void Copy(Merchant aMerchant)
        {
            MerchantName = aMerchant.Name;
            PhoneNumbers = aMerchant.PhoneNumbers;
            Emails = aMerchant.Emails;
        }

        public MerchantLead(Contact lMerchantUser)
        {
            PhoneNumbers = lMerchantUser.PhoneNumbers;
            Emails = lMerchantUser.Emails;
        }

        public string MerchantId { get; set; }
        [Required]
        public string MerchantName { get; set; }
        public virtual Contact SalesPerson { get; set; }
        public virtual AccountStatus Status {get; set; }
        //public bool PendingMerchantApproval { get; set; }
        //public bool isMerchant { get; set; }
    }
}
