using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models
{
    public interface ITourOperatorDBConverter
    {
        string GetLocation(string aInput);
        double? GetStars(string stars);
        string GetName(string lName, List<NameReplacement> aList = null );
        AmenityMap MapAdultsOnly(IDbContext aDbContext, List<AmenityMap> aAmenityMap,  List<Amenity> aAmenities, Staging.Hotel aHotel, Hotel aAccommodation, IHotelBusiness aBiz);
        AmenityMap MapAllInclusive(IDbContext aDbContext, List<AmenityMap> aAmenityMap, List<Amenity> aAmenities, Staging.Hotel aHotel, Hotel aAccommodation, IHotelBusiness aBiz);
    }
}
