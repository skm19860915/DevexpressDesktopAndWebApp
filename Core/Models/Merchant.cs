using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models
{
    public class Merchant : Company
    {
        public Merchant() { }

        public Merchant(RegisterMerchantModel aModel)
        {
            throw new NotImplementedException();

            //MerchantName = aModel.CompanyName;
            ////PhoneNumbers = new List<Phone>() { new Phone() { PhoneID = 1,  aModel.PhoneNumber;
            //Emails = aModel.Emails;
            //FirstName = aModel.FirstName;
            //LastName = aModel.LastName;
        }

        internal void Copy(Merchant aMerchant)
        {
            Name = aMerchant.Name;
            PhoneNumbers = aMerchant.PhoneNumbers;
            Emails = aMerchant.Emails;
        }

        public Merchant(Contact lMerchantUser)
        {
            PhoneNumbers = lMerchantUser.PhoneNumbers;
            Emails = lMerchantUser.Emails;
        }

        public virtual AccountStatus Status { get; set; }
    }
}
