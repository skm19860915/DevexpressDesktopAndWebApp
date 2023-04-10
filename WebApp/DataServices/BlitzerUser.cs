using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using BlitzerCore.Models;

namespace WebApp.DataServices
{
    public class BlitzerUser : IdentityUser
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
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Contact User {get; set;}
        public string LoginEmail { get; set; }
    }
}
