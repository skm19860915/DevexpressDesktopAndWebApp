using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public enum MaritalStatuses { Single, Married, Relationship, Seperated, Divoiced, Widowed }
    public enum Gender { Male, Female}
    public enum MealRequest { None, Vegatarian, Kosher, Diabetic, Vegan}
    public enum SeatPreferences { None, LeftAisle, RightAisle, Window, ExitRow }
    public enum ViewModes { MyTasksOnly, AllTasks, Bugs, Delegated, Completed }
    public enum Tiers { Tier_1, Tier_2, Tier_3 }
    public class Contact
    {
        public Contact()
        {
            PhoneNumbers = new List<Phone>();
            Emails = new List<Email>();
            MemberShips = new List<MemberShip>();
        }
        [Key]
        [StringLength(450)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Name
        {
            get
            {
                var lNickName = "";
                if (NickName != null && NickName.Trim().Length > 0)
                    lNickName = " ( " + NickName + " )";
                return Title + " " + First + " " + Middle + " " + Last + " " + Suffix + lNickName;
            }
        }
        public string First { get; set; }
        public string Middle { get; set; }
        public bool Middle_IsBlank { get; set; }
        public string Last { get; set; }
        public string Suffix { get; set; }
        public Gender? Gender { get; set; }
        public Tiers? Tier { get; set; }
        public Visibility Visiblity { get; set; }
        public virtual List<FOP> Cards { get; set; }
        public virtual List<Credit> Credits { get; set; }
        public virtual ICollection<Note> NoteEntries { get; set; }
        public virtual List<Phone> PhoneNumbers { get; set; }
        [Required]
        public List<Email> Emails { get; set; }
        public List<PreferredAirPort> PreferredAirPorts { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DOB { get; set; }
        public DateTime? Anniversary { get; set; }
        public int? Age { get; set; }
        public MaritalStatuses MaritalStatus { get; set; }
        public bool Deleted { get; set; }
        public string NickName { get; set; }
        public bool? AAA_Member { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Notes { get; set; }
        public string GlobalEntryNumber { get; set; }
        public string TSANumber { get; set; }
        // Preferences
        public SeatPreferences SeatPreferences { get; set; }
        public MealRequest MealRequest { get; set; }
        public string SpecialRequests { get; set; }

        // Passport Info
        public string PassportNumber { get; set; }
        public string PassportCountry { get; set; }
        public DateTime? PassportExpirationDate { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public string PassportIssueAgency { get; set; }
        // Memberships
        public IEnumerable<MemberShip> MemberShips { get; set; }

        // Version 3.1
        public int? ContactTypeId { get; set; }
        [ForeignKey("ContactTypeId")]
        public ContactType ContactType { get; set; }
        public int? ContactSubTypeId { get; set; }
        [ForeignKey("ContactSubTypeId")]
        public ContactSubType ContactSubType { get; set; }
        public int? EmployerId { get; set; }
        [ForeignKey("EmployerId")]
        public virtual Company Employer { get; set; }
        public int? HouseHoldId { get; set; }
        [ForeignKey("HouseHoldId")]
        public virtual HouseHold HouseHold {get; set;}
        public string OwnedById { get; set; }
        [ForeignKey("OwnedById")]
        public virtual Agent OwnedBy { get; set; }
        public string PrimaryEmail
        {
            get
            {
                if (Emails == null)
                    return "";

                var lEmail = Emails.FirstOrDefault(y => y.Preferred == true);
                if (lEmail != null)
                    return lEmail.Address;
                if (lEmail == null && Emails.Any())
                    return Emails[0].Address;

                return "";
            }
        }
        public string PrimaryPhoneNumber
        {
            get
            {
                var PhoneNumber = PhoneNumbers.FirstOrDefault(y => y.Defaut == true);
                if (PhoneNumber != null)
                    return PhoneNumber.PhoneNumber;
                if (PhoneNumber == null && PhoneNumbers != null && PhoneNumbers.Any())
                    return PhoneNumbers[0].PhoneNumber;
                return "";
            }
        }
        public string CreatedById { get; set; }
        [ForeignKey("CreatedById")]
        public Contact CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedById { get; set; }
        [ForeignKey("UpdatedById")]
        public Contact UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string SystemId { get; set; }
        public DateTime? ActivationDate { get; set; }
        public ViewModes ViewMode { get; set; }
    }
}
