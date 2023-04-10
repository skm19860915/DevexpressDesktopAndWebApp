using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models.UI
{
    [NotMapped]
    public class UIContact : BaseUI
    {
        public UIContact()
        {
            TripsActive = new List<UITrip>();
            TripsInActive = new List<UITrip>();
            OpportunitiesActive = new List<UIOpportunity>();
            OpportunitiesInActive = new List<UIOpportunity>();
        }
        public string Id { get; set; }
        [DisplayName("Nick Name")]
        public string NickName { get; set; }
        public Gender? Gender { get; set; }
        public string EmailConfirmed { get; set; }
        public int? EmployerId { get; set; }
        public string EmployerName { get; set; }
        public int RelationshipID { get; set; }
        public string Relationship { get; set; }
        public string Title { get; set; }
        public string First { get; set; }
        public string Middle { get; set; }
        public bool Middle_IsBlank { get; set; }
        public string Last { get; set; }
        public string Suffix { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Anniversary { get; set; }
        public string Cell { get; set; }
        [DisplayName("Phone")]
        public string PrimaryPhone { get; set; }
        [DisplayName("Email")]
        public string PrimaryEmail { get; set; }
        [DisplayName("Address")]
        public string AddressLine1 { get; set; }
        [DisplayName("Apt/Suite")]
        public string AddressLine2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set;  }
        public string ZipCode { get; set; }
        public string AgentId { get; set; }
        public bool AAA_Member { get; set; }
        public bool Deleted { get; set; }
        public string GE_OR_TSA { get; set; }
        [DisplayName("Global Entry Number")]
        public string GlobalEntryNumber { get; set; }
        [DisplayName("TSA Number")]
        public string TSANumber { get; set; }

        // Preferences
        [DisplayName("Seat Preference")]
        public string SeatPreferences { get; set; }
        public MealRequest MealRequest { get; set; }

        public string SpecialRequests { get; set; }

        // Passport Info
        [DisplayName("Passport Number")]
        public string PassportNumber { get; set; }
        [DisplayName("Passport Country")]
        public string PassportCountry { get; set; }
        [DisplayName("Passport Expriation")]
        [DataType(DataType.Date)]
        public DateTime? PassportExpirationDate { get; set; }
        [DisplayName("Passport Issue Date")]
        [DataType(DataType.Date)]
        public DateTime? PassportIssueDate { get; set; }
        [DisplayName("Passport Issue Agency")]
        public string PassportIssueAgency { get; set; }
        public List<UIQuoteRequest> QuoteRequests { get; set; }
        public List<UIOpportunity> OpportunitiesActive { get; set; }
        public List<UIOpportunity> OpportunitiesInActive { get; set; }
        public List<UITrip> TripsActive { get; set; }
        public List<UITrip> TripsInActive { get; set; }
        public IEnumerable<MemberShip> MemberShips { get; set; }
        public IEnumerable<UIContact> HouseHoldMembers { get; set; }
        public List<UIFOP> Cards { get; set; }
        public string Notes { get; set; }
        public IEnumerable<UINote> NoteEntries { get; set; }
        public string Note_Who { get; set; }
        public string Note_Where { get; set; }
        public string Note_Text { get; set; }
        public bool ProfileComplete { get; set; }
        public string RootMemberId { get; set; }
        public DateTime? ActivationDate { get; set; }
        public List<UICategory> Categories { get; set; }
    }
}
