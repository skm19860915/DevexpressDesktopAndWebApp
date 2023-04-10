using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class DBVersion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Don't forget to modify ExtensionMethods::ViewExtensions::isRelease()
        //                        BlitzerCore.Models.DBVersion
        //                        WebApp.Services.BlitzerServices
        //                        Desktop.BlitzerDesktop.Label
        public int Major { get; set; } // Major Release on 12/26/2020 for Destinations
        public int Minor { get; set; } // Adding Support for SubPages, Add Contact from SalesForces
        public DateTime? ChangedOn {get; set;}
        public string Description { get; set; }
        public string Label { get { return $"DB Version {Major}.{Minor}"; }  }
    }
}
