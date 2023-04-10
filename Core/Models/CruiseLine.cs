using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;
using System.Collections.Generic;

namespace BlitzerCore.Models
{
    public enum CruiseType { Ocean, River }
    public class CruiseLine : TourOperator
    {
        public CruiseType Type { get; set; }
        public List<AmenityMap> Amenities { get; set; }

    }
}
