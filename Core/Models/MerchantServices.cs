using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class MerchantServices
    {

        public int MerchantID { get; set; }

        public int ServiceID { get; set; }

        [ForeignKey("TransportationID")]
        public virtual Merchant Merchant { get; set; }
        [ForeignKey("AccommodationID")]
        public virtual Service Service { get; set; }
    }
}

