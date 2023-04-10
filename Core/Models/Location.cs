using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class Location
    {
        public enum Statuses  {  Active, Inactive }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }
        public string Name { get; set; }
        public Uri PictureURL { get; set; }
        public int? ParentID { get; set; }
        //public virtual Location Parent { get; set; }
        public Statuses Status { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

    }
}
