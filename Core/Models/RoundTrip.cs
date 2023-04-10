using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class RoundTrip : OneWayTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoundTripID { get; set; }

        public int InBoundOriginAirportID { get; set; }
        [ForeignKey("InBoundOriginAirportID")]
        public virtual AirPort InBoundOriginAirport { get; set; }

        public int InBoundDestinationAirportID { get; set; }
        [ForeignKey("InBoundDestinationAirportID")]
        public virtual AirPort InBoundDestinationAirport { get; set; }

        public List<Flight> InBoundFlights { get; set; }
    }
}
