using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public enum TripStage {
        [Display(Name = "Incomplete Profile")]
        CompleteProfile, Booked,
        [Display(Name = "Balance Outstanding")]
        BalanceOutstanding,
        [Display(Name = "Book Transfer")]
        BookTransfer,
        [Display(Name = "Send Documents")]
        SendDocuments,
        [Display(Name = "Open Tasks")]
        OpenTasks,
        [Display(Name = "Ready For Travel")]
        ReadyForTravel,
        [Display(Name = "Traveled")]
        Traveled,
        [Display(Name = "Cancelled")]
        Cancelled

    }

    public class Trip : Opportunity
    {
        public enum Statuses { Active, Cancelled, Completed, Deleted }
        public Trip() : base()
        {
            Bookings = new List<Booking>();
        }
        public Trip(Opportunity aOpportunity) : base()
        {
            this.ID = aOpportunity.ID;
            Name = aOpportunity.Name;
            StartDate = aOpportunity.StartDate;
            EndDate = aOpportunity.EndDate;
            OutboundAirPortID = aOpportunity.OutboundAirPortID;
            InboundAirPortID = aOpportunity.InboundAirPortID;
            QuoteRequests = aOpportunity.QuoteRequests;
            AgentId = aOpportunity.AgentId;
            Stage = aOpportunity.Stage;
            TripStatus = Statuses.Active;
        }

        public virtual List<Booking> Bookings { get; set; }
        public int DaysToStart { get; set; }
        public double Balance { get; set; }
        public Statuses TripStatus { get; set; }
        public DateTime? DocumentsPrintedOn { get; set; }
        public BlitzerCore.Models.TripStage TripStage { get; set; }
        public virtual List<TripComponent> TripComponents{get; set;}
    }
}