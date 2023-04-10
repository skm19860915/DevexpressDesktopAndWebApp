using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class OneWayTicket 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketID { get; set; }
        public string AirLine { get; set; }

        public int OutBoundOriginAirportID { get; set; }
        [ForeignKey("OriginAirportID")]
        public virtual AirPort OutBoundOriginAirport { get; set; }

        public int OutBoundDestinationAirportID { get; set; }
        [ForeignKey("DestinationAirportID")]
        public virtual AirPort OutBoundDestinationAirport { get; set; }

        public List<Flight> OutBoundFlights { get; set; }
    }
}
