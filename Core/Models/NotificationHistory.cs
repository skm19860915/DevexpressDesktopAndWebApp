using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public enum NotificationTypes  { FinalPayment, LatePayment }

    public class NotificationHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Trip TripId { get; set; }
        [ForeignKey("TripId")]
        public Trip Trip { get; set; }
        public int ContactId {get; set;}
        [ForeignKey("ContactId")]
        public Contact Contact { get; set; }
        public NotificationTypes NotificationType { get; set; }
    }
}
