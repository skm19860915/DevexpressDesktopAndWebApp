using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace BlitzerCore.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }
        public int ParentPostID { get; set; }
        [ForeignKey("ParentPostID")]
        public Post ParentPost { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public Contact User { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime PostedOn { get; set; }

        // This post can be on the graphic below
        public int? PToMMapID { get; set; }
        [ForeignKey("PToMMapID")]
        public virtual PostToMediaMapper PToMMap { get; set; }
        public List<UI.Media> Picture { get; set; }
        public bool Approved { get; set; }
        public DateTime ApprovalDate { get; set; }
        // Post can be on a Hotel
        public int? HotelID { get; set; }
        [ForeignKey("HotelID")]
        public virtual Hotel Hotel { get; set; }
        public string ApproverID { get; set; }
        [ForeignKey("ApproverID")]
        public Contact Approver { get; set; }
    }
}
