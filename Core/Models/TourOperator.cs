using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class TourOperator : Company
    {
        public const string JOURNESE = "Journese";
        public const string DELTA_VACATIONS = "Delta Vacations";
        public const string AA_VACATIONS = "AA Vacations";
        public const string VACATION_EXPRESS = "Vacation Express";
        public ICollection<Booking> Bookings { get; set; }
    }
}
