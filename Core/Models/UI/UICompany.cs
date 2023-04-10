using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    public class UICompany : BaseUI, IComparable<UICompany>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string PrimaryPhone { get; set; }
        public string PrimaryEmail { get; set; }
        [DisplayName("Address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int? BusinessTypeID { get; set; }
        public string BusinessType { get; set; }
        public string Description { get; set; }
        public string Memo { get; set; }
        [DisplayName("Stars")]
        public double Rating { get; set; }
        public string WebSite { get; set; }
        public List<UIBooking> Bookings { get; set; }
        public List<UIContact> Contacts { get; set; }
        public Visibility Visibility { get; set; }
        public Media ThumbNail { get; set; }
        public int ThumbNailId { get; set; }
        public bool TourOperator { get; set; }
        public UICompanyQuoteSummary QuoteData { get; set; }
        public Page Page { get; set; }
        public int? PageId { get; set; }
        public int CompareTo(UICompany other)
        {
            if (this.Id < other.Id)
            {
                return 1;
            }
            else if (this.Id > other.Id)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
