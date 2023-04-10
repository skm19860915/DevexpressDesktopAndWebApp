using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlitzerCore.Business.DBConverters
{
    public class WorldAgentDirectConverter : ConverterBase, ITourOperatorDBConverter
    {
        const string AllInclusive = "AI";
        public string GetLocation(string aLocation)
        {
            if (aLocation == null)
                return "";

            if (aLocation.IndexOf(':') > 0)
            {
                var lOutput = aLocation.Split(':')[1].Trim();
                if ( lOutput.IndexOf('-') > 0 )
                    return lOutput.Split('-')[0].Trim();

                return lOutput;
            }
            else
            {
                BlitzerCore.Utilities.Logger.LogWarning("World Agent Direction Bot found incorrectly formatted location.  Missing : [{aLocation}]");
                return aLocation;
            }
        }
        public double? GetStars(string aStars)
        {
            if (aStars == null || aStars.Length == 0)
                return null;

            var lNum = aStars.Trim().Split(' ')[0];
            double lOutput = 0;
            if (Double.TryParse(lNum, out lOutput))
                return Math.Round(lOutput);

            return null;
        }

        public AmenityMap MapAdultsOnly ( IDbContext aDbContext, List<AmenityMap> aAmenityMap,  List<Amenity> aAmenities, Staging.Hotel aHotel, Hotel aAccommodation, IHotelBusiness aQRBusiness)
        {
            AmenityMap lAmenityMap = null;

            if (aHotel.Name.Contains(ADULTS_ONLY) == true)
                lAmenityMap = new HotelBusiness(aDbContext).CreateAmenityMap(aAccommodation, aAmenityMap, aAmenities, Amenity.AmenityTypes.AdultsOnly);

            if (lAmenityMap != null)
                aAccommodation.Amenities.Add(lAmenityMap);

            return lAmenityMap;
        }

        public AmenityMap MapAllInclusive(IDbContext aDbContext, List<AmenityMap> aAmenityMap, List<Amenity> aAmenities, Staging.Hotel aHotel, Hotel aAccommodation, IHotelBusiness aQRBusiness)
        {
            AmenityMap lAmenityMap = null;

            if (aHotel.HotelRateTypes.Count(x=>x.RateType == AllInclusive) > 0 )
                lAmenityMap = new HotelBusiness(aDbContext).CreateAmenityMap(aAccommodation, aAmenityMap, aAmenities, Amenity.AmenityTypes.AllInclusive);

            if (lAmenityMap != null)
                aAccommodation.Amenities.Add(lAmenityMap);


            return lAmenityMap;
        }

        public string GetName(string aName, List<NameReplacement> aReplacements = null )
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