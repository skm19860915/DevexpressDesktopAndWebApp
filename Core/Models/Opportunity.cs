using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BlitzerCore.Utilities;

namespace BlitzerCore.Models
{
    public enum OpportunityStages { Invalid, New, QuoteSent, OnHold, Negotiations, Won, Loss }

    public class Opportunity : IDisposable
    {
        public enum OpportunityStatus { Active, Inactive }
        public Opportunity() 
        {
            Stage = OpportunityStages.New;
            QuoteRequests = new List<QuoteRequest>();
            Travelers = new List<UserMap>();
            this.Activity = Guid.NewGuid().ToString();
        }

        public void Dispose()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Departure Airport")]
        public int OutboundAirPortID { get; set; }
        [ForeignKey("OutboundAirPortID")]
        public virtual AirPort OutboundAirPort { get; set; }
        [Display(Name = "Destinatin Airport")]
        public int InboundAirPortID { get; set; }
        [ForeignKey("InboundAirPortID")]
        public virtual AirPort InboundAirPort { get; set; }
        public virtual List<UserMap> Travelers { get; set; }
        public virtual List<QuoteRequest> QuoteRequests { get; set; }
        public OpportunityStatus Status { get; set; }
        public OpportunityStages Stage { get; set; }
        public string AgentId { get; set; }
        [ForeignKey("AgentId")]
        public virtual Agent Agent { get; set; }
        public string Notes { get; set; }
        [NotMapped]
        public string Activity { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual IEnumerable<Note> NoteEntries { get; set; }
        public int? ReferralId { get; set; }
        [ForeignKey("ReferralId")]
        public virtual ReferralSource Referral { get; set;}
        public DateTime OppClosedDate { get; set; }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Contact CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedById { get; set; }
        [ForeignKey("UpdatedById")]
        public Contact UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
