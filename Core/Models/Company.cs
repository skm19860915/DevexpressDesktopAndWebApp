using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public enum Visibility { Public, Company, Private}
    public class Company
    {
        public Company () { 
            UpdatedOn =  CreatedOn = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }
        public string Street { get; set; }
        public string Street2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Promo { get; set; }

        public ICollection<Note> NoteEntries { get; set; }
        public List<Phone> PhoneNumbers { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public List<Email> Emails { get; set; }
        public string Description { get; set; }
        public string Memo { get; set; }
        public int? BusinessTypeID { get; set; }
        [ForeignKey("BusinessTypeID")]
        public virtual BusinessType BusinessType { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Contact CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedById { get; set; }
        [ForeignKey("UpdatedById")]
        public Contact UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Agent Owner { get; set; }
        public Visibility Visiblity { get; set; }
        public Uri ThumbNail { get; set; }
        public Uri HyperLink { get; set; }
        public int? PageId { get; set; }
        [ForeignKey("PageId")]
        public virtual Page Page { get; set; }
        public int SettlementTerms { get; set; }
        public double? Rating { get; set; }
    }
}
