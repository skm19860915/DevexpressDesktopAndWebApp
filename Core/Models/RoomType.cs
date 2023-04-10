using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlitzerCore.Models
{
    public class RoomType : SKU
    {
        [NotMapped]
        public int AccommodiationID
        {
            get { return ProviderID; }
            set { ProviderID = value; }
        }
        [NotMapped]
        public virtual Hotel Accommodation
        {
            get { return Provider as Hotel; }
            set { Provider = value; }
        }

        [DisplayName("King or 2 Queens")]
        public bool KingOr2Queens { get; set; }
    }
}
