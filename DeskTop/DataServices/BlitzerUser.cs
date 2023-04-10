using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BlitzerCore.Models;

namespace Desktop.Models
{
    public class BlitzerUser 
    {
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string FirstName { get; set; }
        public string Middle { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
        [Required]
        public List<Email> Emails { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}" )]
        public DateTime? DOB { get; set; }
        public string NickName { get; set; }
        public string GE_OR_TSA { get; set; }
        public bool AAA_Member { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
