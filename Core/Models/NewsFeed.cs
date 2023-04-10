using System;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace BlitzerCore.Models
{
    public class NewsFeed
    {
        public enum SourceTypes
        {
            ADDEDUSERSTORY,
            ADDEDTASK,
            ADDEDCOMPANY,
            ADDEDOPPORTUNITY
        }

        public int ID { get; set; }
        public string ContactId { get; set; }
        [ForeignKey("ContactId")]
        public virtual Contact Contact { get; set; }
        public int? ParentID { get; set; }
        [ForeignKey("ParentID")]
        public virtual NewsFeed Parent { get; set; }
        public int UserId { get; set; }
        public int? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public virtual Company Company { get; set; }
        public int? OpportunityID { get; set; }
        [ForeignKey("OpportunityID")]
        public virtual Opportunity Opportunity { get; set; }
        public int? TaskID { get; set; }
        [ForeignKey("TaskID")]
        public virtual Task Task { get; set; }
        public int? UserStoryID { get; set; }
        [ForeignKey("UserStoryID")]
        public virtual UserStory UserStory { get; set; }
        public string UserName { get; set; }
        public int NewsTypeId { get; set; }
        public int SourceId { get; set; }
        public SourceTypes SourceType { get; set; }
        public DateTime ActionDateTime { get; set; }
        public string Avatar { get; set; }
        public string News { get; set; }
        public string TimeAgo { get; set; }

        public NewsFeed ()
        {
        }
    }

    public class NewsType
    {
        public int NewsTypeId { get; set; }
        public string NewsText { get; set; }
    }
}
