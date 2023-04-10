using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace BlitzerCore.Models.UI
{
    public class UICruiseLine : UICompany
    {
        public UICruiseLine()
        {
        }
        public string Promo { get; set; }
        public string PictureURL { get; set; }
        public string Location { get; set; }
    }
}
