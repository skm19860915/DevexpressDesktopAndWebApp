using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class ContactTrip
    {
        public int TripID { get; set; }
        public string ContactId { get; set; }
        public int RelationshipID { get; set; }


        [ForeignKey("RelationshipID")]
        public virtual Relationship Relationship { get; set; }
        [ForeignKey("TripID")]
        public virtual Trip Trip { get; set; }
        [ForeignKey("ContactId")]
        public virtual Client Contact { get; set; }
    }
}
