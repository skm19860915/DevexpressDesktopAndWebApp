using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders.Physical;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class AccommodationDataAccess
    {
        const string ClassName = "AccommodationDataAccess::";
        IDbContext mContext = null;
        public AccommodationDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public List<Hotel> GetAll()
        {
            return mContext.Accommodations.Where(x => x.BusinessTypeID == 3).Distinct().OrderBy(m => m.Name).ToList();
        }

        public List<RoomType> GetAllRoomTypes(Hotel aHotel)
        {
            var lOutput = mContext.RoomTypes.Where(x => x.ProviderID == aHotel.Id);
            if (lOutput != null )
                return lOutput.Distinct().OrderBy(m => m.SortOrder).ToList();
            else
                return new List<RoomType>();
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return mContext.RoomTypes.Distinct().OrderBy(m => m.Name).ToList();
        }

        public int GetAdultsOnly()
        {
            return mContext.Accommodations.ToList().Count(x => x.Amenities.Exists(y => y.AmenityID == (int)Amenity.AmenityTypes.AdultsOnly));
        }

        public int GetAllInclusive()
        {
            return mContext.Accommodations.ToList().Count(x => x.Amenities.Exists(y => y.AmenityID == (int)Amenity.AmenityTypes.AllInclusive));
        }

        public List<Amenity> GetAllAmenities()
        {
            return mContext.Amenities.ToList();
        }

        public Hotel Get(int aResortID)
        {
            return mContext.Accommodations
                .Include(x=>x.Amenities)
                .Where(x => x.Id == aResortID).FirstOrDefault();
        }

        public Amenity GetAmenity(string aAmenityName)
        {

            return mContext.Amenities.Where(x => x.Type == aAmenityName).FirstOrDefault();
        }

        public int UpdateAmentity(string aAmenityName)
        {
            string FuncName = ClassName + $"UpdateAmentity (aAmenityName = {aAmenityName})";

            mContext.Amenities.Add(new Amenity() { Type = aAmenityName });
            try
            {
                return mContext.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to update Amenity", e);
            }

            return -1;
        }

        public List<int> GetQuoteFilteredAccommodations(QuoteGroup aQuoteGroup)
        {
            return (from F in mContext.Filters
                    join Q in mContext.QuoteGroups on F.QuoteGroupID equals Q.Id

                    join FA in mContext.FilteredAccommodations on F.FilterID equals FA.FilterID
                    where Q.Id == aQuoteGroup.Id
                    select FA.AccommodationID).Distinct().ToList();
        }

        public List<Hotel> GetByAirPort(int aAirPortID)
        {
            return mContext.Accommodations.Where(x => x.AirPortID != null && x.AirPortID.Value == aAirPortID).OrderBy(m => m.Name).ToList();
        }

        public void AddAccommodation(Hotel aHotel)
        {
            if (aHotel.Id == 0)
                mContext.Accommodations.Add(aHotel);
            else
                mContext.Accommodations.Update(aHotel);
        }

        public AmenityMap GetAmenityMap(Staging.Hotel aResort, string aAmenity)
        {
            if (aResort.RequestID < 1)
                return null;

            return mContext.AmenityMaps.Where(x => x.StagingHotelID == aResort.HotelStagingID && x.Amenity.Type == aAmenity).FirstOrDefault();
        }

        public List<RoomType> GetRoomTypes(int aHotelId)
        {
            var lOutput = mContext.SKUs
                .Include(x => x.Provider)
                .Where(x => x.ProviderID == aHotelId)
                .ToList();

            return lOutput.OfType<RoomType>().ToList();
        }

        public RoomType GetRoomType(int id)
        {
            return mContext.SKUs
                .Include(x => x.Provider)
                .FirstOrDefault(x => x.SKUID == id) as RoomType;
        }

        public AmenityMap CreateAmenityMap(Hotel aResort, Amenity.AmenityTypes aType)
        {
            var lAmenityName = Amenity.GetAmenityName(aType);
            var lAmenity = GetAmenity(lAmenityName);

            if (lAmenity == null)
            {
                lAmenity = new Amenity() { Type = lAmenityName };
                var lCnt = new ResortPageDataAccess(mContext).Save(lAmenity);
            }

            if (aResort.Id > 0)
            {
                var lMap = mContext.AmenityMaps.Where(x => x.AccommodationID == aResort.Id && x.Amenity.Type == lAmenityName).FirstOrDefault();
                if (lMap != null)
                    return lMap;
            }

            return new AmenityMap() { Accommodation = aResort, AmenityID = lAmenity.ID };
        }


        public AmenityMap GetAmenityMap(Hotel aResort, string aAmenity)
        {
            if (aResort.Id < 1)
                return null;

            return mContext.AmenityMaps.Where(x => x.AccommodationID == aResort.Id && x.Amenity.Type == aAmenity).FirstOrDefault();
        }

        public AmenityMap AddAmenityMap(Hotel aResort, string aAmenity)
        {
            var lAmenity = GetAmenity(aAmenity);
            if (lAmenity == null)
            {
                UpdateAmentity(aAmenity);
                lAmenity = GetAmenity(aAmenity);
            }
            AmenityMap lMap = new AmenityMap() { AccommodationID = aResort.Id, AmenityID = lAmenity.ID };
            mContext.AmenityMaps.Add(lMap);
            return lMap;
        }

        //todo : Must fix that fat that with the staging hotel, how do we map an ammentity to a staging hotel
        // and then once we convert to a real hotel, move the ammentities over
        public AmenityMap AddAmenityMap(Staging.Hotel aResort, string aAmenity)
        {
            var lAmenity = GetAmenity(aAmenity);
            if (lAmenity == null)
            {
                UpdateAmentity(aAmenity);
                lAmenity = GetAmenity(aAmenity);
            }
            AmenityMap lMap = null;
            if (aResort.HotelStagingID > 0)
            {
                lMap = new AmenityMap() { AccommodationID = aResort.HotelStagingID, AmenityID = lAmenity.ID };
                mContext.AmenityMaps.Add(lMap);
            }
            else
                return null;

            //else
            //    lMap = new AmenityMap() { Accommodation = aResort, AmenityID = lAmenity.ID };

            return lMap;
        }
        public int Save(Hotel aResort, bool aCommit = true)
        {
            string FuncName = ClassName + $"Save - (Hotel, {aCommit}";
            if (aResort.Id == 0)
                mContext.Accommodations.Add(aResort);
            else
                mContext.Accommodations.Update(aResort);

            try
            {
                if (aCommit == true)
                {
                    var lCnt = mContext.SaveChanges();
                    Logger.LogInfo(FuncName + $" - Updated {lCnt} records");
                    return lCnt;
                } else 
                    Logger.LogInfo($"{FuncName} - Delayed Save of {aResort.Name}");

                return 0;
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " - Failed to save Hotel", e);
            }

            return 0;
        }

        public List<Hotel> GetByAmentity(Amenity.AmenityTypes aType)
        {
            return (from M in mContext.AmenityMaps
                    join H in mContext.Accommodations on M.AccommodationID equals H.Id
                    join A in mContext.Amenities on M.AmenityID equals A.ID
                    where A.ID == (int)aType
                    select H).Distinct().ToList();
        }

        public List<AmenityMap> GetAllAmenitiesMaps()
        {
            return mContext.AmenityMaps.ToList();
        }
    }
}
