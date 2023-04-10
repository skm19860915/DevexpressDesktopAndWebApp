using System;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Helpers;
using BlitzerCore.Utilities;
using System.Collections.Generic;

namespace BlitzerCore.Business.DBConverters
{
    public class AAConverter : ConverterBase, ITourOperatorDBConverter
    {
        public string GetLocation(string aLocation)
        {
            return "";
        }

        public AmenityMap MapAdultsOnly(IDbContext aDbContext, List<AmenityMap> aAmenityMap, List<Amenity> aAmenities, Staging.Hotel aHotel, Hotel aAccommodation, IHotelBusiness aBiz)
        {
            return null;
        }
        public AmenityMap MapAllInclusive(IDbContext aDbContext, List<AmenityMap> aAmenityMap, List<Amenity> aAmenities, Staging.Hotel aHotel, Hotel aAccommodation, IHotelBusiness aBiz)
        {
            return null;
        }

        public double? GetStars(string innerText)
        {
            double lOutput = -1;
            if (innerText == null ||innerText.Contains(':') == false)
                return 0;

            var lValue = innerText.Split(':')[1];

            if (double.TryParse(lValue.Trim(), out lOutput))
                return lOutput;
            else
                Logger.LogWarning("Failed to parse Stars >" + innerText + "<");

            return lOutput;
        }

        public string GetName(string aName, List<NameReplacement> aReplacements = null)
        {
            string lName = aName;
            if (aReplacements != null)
            {
                var lReplacment = aReplacements.Where(x =>
                        x.Original == aName && x.ReplaceType == NameReplacement.ReplacementTypes.Hotel)
                    .Select(x => x.Hotel.Name);

                lName = lReplacment.Any() == false ? aName : lReplacment.First();
            }

            lName = DataHelper.ConvertWebString(lName);

            // Remove Semi Colen
            lName = lName.Replace(",", "");
            lName = RemoveAdultsOnly(lName);
            return lName;
        }
    }
}