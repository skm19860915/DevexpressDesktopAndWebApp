using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.UIHelpers;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BlitzerCore.Helpers;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace BlitzerCore.Business
{
    public class HotelBusiness : CompanyBusiness, IHotelBusiness
    {
        readonly AccommodationDataAccess mDataAccess;
        private new const string ClassName = "HotelBusiness::";
        List<Amenity> Amenities { get; set; }

        public HotelBusiness(IDbContext aContext) : base ( aContext)
        {
            mDataAccess = new AccommodationDataAccess(DbContext);
        }

        public List<Lookup> GetFilterView ( QuoteRequest aRequest, out List<int> aSelectedHotels,  QuoteGroup aQuoteGroup =  null )
        {
            aSelectedHotels = GetSelectedAccommodations(aQuoteGroup);
            var lQuoteHotels = new QuoteGroupDataAccess(DbContext).GetUniqueHotels(aQuoteGroup);
            AirPort lTargetAirPort = GetTargetAirPort(aRequest);
            return GetAccommodationsByAirPort(lTargetAirPort, lQuoteHotels);
        }
        public void CreateMissingAccommodations(QuoteGroup aQuoteGroup, IEnumerable<Staging.Hotel> aStagedHotels, IWebTravelSrv aWebSrv)
        {
            string FuncName = ClassName + "CreateMissingAccommodations - ";
            Logger.EnterFunction(FuncName);
            int lStart = DbContext.Accommodations.Count();
            var lStagingDA = new StagingDataAccess(DbContext);
            var lResortDataAccess = new AccommodationDataAccess(DbContext);
            var lReplacements = new BlitzerDataAccess(DbContext).GetNameReplacements();
            int lNew = 0;
            Logger.LogInfo($"Starting with {lStart} Hotels, Merging with {aStagedHotels.Count()} Staging Resorts");
            //Logger.LogInfo($"{mContext.Accommodations.Count(x=>x.Amenities.Exists(y=>y.AmenityID == (int)Amenity.AmenityTypes.AdultsOnly))} Adults Only, {mContext.Accommodations.Count(x=>x.Amenities.Exists(y=>y.AmenityID == (int)Amenity.AmenityTypes.AllInclusive))} All Inclusive Resorts");
            try
            {
                var lAccommodationDA = new AccommodationDataAccess(DbContext);
                var lExistingResorts = lAccommodationDA.GetAll();

                Amenities = lResortDataAccess.GetAllAmenities();

                foreach (var lStagingHotel in aStagedHotels)
                {
                    try
                    {
                        if (lStagingHotel.Name == null)
                            continue;

                        var lHotelName = aWebSrv.GetDBConverter().GetName(lStagingHotel.Name, lReplacements);

                        var lExistingHotel = lExistingResorts.FirstOrDefault(x => x.Name == lHotelName);

                        if (lExistingHotel == null)
                        {
                            Hotel lNewHotel = CreateHotel(lStagingHotel, aQuoteGroup, aWebSrv);
                            Save(lNewHotel, aQuoteGroup.QuoteRequest.Agent, false);
                            MapAmenities(lStagingHotel, lResortDataAccess.GetAllAmenitiesMaps(), Amenities, lNewHotel, aWebSrv);
                            lNew++;
                        }
                        else
                        {
                            MapAmenities(lStagingHotel, lResortDataAccess.GetAllAmenitiesMaps(), Amenities, lExistingHotel, aWebSrv);
                            if (lHotelName.Equals(lStagingHotel.Name) == false)
                            {
                                lStagingHotel.Name = lHotelName;
                                //lStagingDA.Save(lStagingHotel);
                                DbContext.Staging_Hotels.Update(lStagingHotel);
                            }
                        }
                    }
                    catch (Exception e1)
                    {
                        Logger.LogException(FuncName + $" Failed to create staging hotel {lStagingHotel.Name}", e1);
                    }
                }
                var lCount = DbContext.SaveChanges();
                int End = DbContext.Accommodations.Count();
                Logger.LogInfo($"Created " + (lNew - lStart) + $" new Accommodations, Accomodations count = {DbContext.Accommodations.Count()}");
                //Logger.LogInfo($"{mContext.Accommodations.Count(x => x.Amenities.Exists(y => y.AmenityID == (int)Amenity.AmenityTypes.AdultsOnly))} Adults Only, {mContext.Accommodations.Count(x => x.Amenities.Exists(y => y.AmenityID == (int)Amenity.AmenityTypes.AllInclusive))} All Inclusive Resorts");
                Logger.LogInfo(FuncName + $" There are {lResortDataAccess.GetAdultsOnly()} Adults Only Resorts");
                Logger.LogInfo(FuncName + $" There are {lResortDataAccess.GetAllInclusive()} All Inclusive Resorts");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " - Failed to create missing accommodation", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }
        public void CreateMissingRoomTypes(List<Staging.HotelRate> lStaggedResortQuotes, IWebTravelSrv aWebSrv, QuoteGroup aQuoteGroup)
        {
            string FuncName = $"{ClassName}CreateMissingRoomTypes ";
            var lExistingRoomTypes = DbContext.RoomTypes.ToList();
            var lStaggedResorts = lStaggedResortQuotes.Select(x => x.HotelStaging).Distinct().ToList();
            var lResorts = new AccommodationDataAccess(DbContext).GetAll();
            var lRoomTypeStr = "";

            foreach (var lResortPriceByRoomType in lStaggedResortQuotes)
            {
                Hotel lResort = GetResort(lResorts, lResortPriceByRoomType.HotelStaging, aWebSrv, aQuoteGroup);
                if (lResort != null)
                {
                    lRoomTypeStr = DataHelper.ConvertWebString(lResortPriceByRoomType.RoomType);
                    bool lMatchingHotel = lExistingRoomTypes.Any(x => x.AccommodiationID == lResort.Id);
                    bool lMatchingName = lExistingRoomTypes.Any(x => x.Name == lRoomTypeStr && x.AccommodiationID == lResort.Id);
                    if (lMatchingName == false)
                    {
                        var lRoomType = new RoomType();
                        lRoomType.AccommodiationID = lResort.Id;
                        lRoomType.Name = lRoomTypeStr;
                        DbContext.SKUs.Add(lRoomType);
                    }
                }
                else
                {
                    Logger.LogError($"{FuncName}Failed to find a Accomodation using staging name [" + lResortPriceByRoomType.HotelStaging.Name + "]");
                }
            }
            int lCount = DbContext.SaveChanges();
            Logger.LogInfo("Created " + lCount + " new AccommodationRoomTypes");
        }


        public RoomType GetRoomType(IDbContext aContext, BlitzerCore.Models.QuoteGroup aQuoteGroup,
            List<RoomType> aRoomTypes, Staging.HotelRate aStaggedResort, int aResortID)
        {
            try
            {
                //Stopwatch lSW = Logger.StartStopWatch();
                var lHotelRoomTypes = aRoomTypes.Where(x => x.AccommodiationID == aResortID);
                var lRoomType = aRoomTypes.Where(x => x.Name == DataHelper.ConvertWebString(aStaggedResort.RoomType) && x.AccommodiationID == aResortID).FirstOrDefault();
                //Logger.StopWatchElapsedTime(lSW, "Find RoomType");
                if (lRoomType != null)
                {
                    int? lRoomTypeID = lRoomType.SKUID;
                    if (lRoomTypeID != null)
                    {
                        //Logger.StopWatchElapsedTime(lSW, "Find RoomType");
                        return lRoomType;
                    }
                }

                lRoomType = new RoomType();
                lRoomType.Name = aStaggedResort.RoomType.Trim();
                lRoomType.AccommodiationID = aResortID;
                aContext.SKUs.Add(lRoomType);

                return lRoomType;
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to get Room Type ID for " + aStaggedResort.RoomType.Trim() + " StaggedResortID = " + aStaggedResort.HotelRateStagingID, e);
                return null;
            }
        }

        private Hotel GetResort(List<Hotel> aResorts, Staging.Hotel aStaggingHotel, IWebTravelSrv aWebSrv, QuoteGroup aQuoteGroup)
        {
            var lSearchName = aWebSrv.GetDBConverter().GetName(aStaggingHotel.Name).ToUpper();
            var lHotel = aResorts.Where(x => x.Name != null && x.Name.ToUpper() == lSearchName).FirstOrDefault();
            if (lHotel == null)
            {
                Hotel lNewHotel = CreateHotel(aStaggingHotel, aQuoteGroup, aWebSrv);
                Save(lNewHotel, aQuoteGroup.QuoteRequest.Agent, false);

                var lCnt = aResorts.Count(x => x.Name.ToUpper().Contains(lSearchName));
                Logger.LogWarning($"Failed to find resort matching the name {lSearchName} in list of " + aResorts.Count() + $" Contains Haven {lCnt}");
            }
            return lHotel;
        }

        public bool AddAmenity ( Hotel aHotel, Amenity.AmenityTypes aType )
        {
            var lHotelAmenities = aHotel.Amenities;
            var lAmenities = new HotelDataAccess(DbContext).GetAmenities();
            var lAmenityName = Amenity.GetAmenityName(aType);
            var lAmenity = lAmenities.Where(x => x.Type == lAmenityName).FirstOrDefault();

            if (lAmenity != null && lHotelAmenities.Count(x => x.AmenityID == lAmenity.ID) > 0)
                return false;


            if (lHotelAmenities == null )
                lHotelAmenities = new List<AmenityMap>();

            lHotelAmenities.Add(new AmenityMap (){ AccommodationID = aHotel.Id, AmenityID = lAmenity.ID });
            return Save(aHotel) == 1;
        }

        public bool RemoveAmenity (Hotel aHotel, Amenity.AmenityTypes aType )
        {
            var lHotelAmenities = aHotel.Amenities;
            var lAmenities = new HotelDataAccess(DbContext).GetAmenities();
            var lAmenityName = Amenity.GetAmenityName(aType);
            var lAmenity = lAmenities.Where(x => x.Type == lAmenityName).FirstOrDefault();

            // This means it is not in the map
            if (lHotelAmenities.Count() == 0 || lHotelAmenities.Count(x=>x.AmenityID == lAmenity.ID) == 0 )
                return false;

            // Must remove from the Map
            lHotelAmenities.Remove(lHotelAmenities.FirstOrDefault(x => x.AmenityID == lAmenity.ID));
            return Save(aHotel) == 1;
        }

        public AmenityMap CreateAmenityMap(Hotel aResort, List<AmenityMap> aAmenityMap,  List<Amenity> aAmenities, Amenity.AmenityTypes aType)
        {
            var lAmenityName = Amenity.GetAmenityName(aType);
            var lAmenity = aAmenities.Where(x => x.Type == lAmenityName).FirstOrDefault();


            if (lAmenity == null)
            {
                lAmenity = new Amenity() { Type = lAmenityName };
                var lCnt = new ResortPageDataAccess(DbContext).Save(lAmenity);
            }

            if (aResort.Id > 0)
            {
                var lMap = aAmenityMap.Where(x => x.AccommodationID == aResort.Id && x.Amenity.Type == lAmenityName).FirstOrDefault();
                if (lMap != null)
                    return lMap;
            }

            return new AmenityMap() { Accommodation = aResort, AmenityID = lAmenity.ID };
        }
        public Hotel CreateHotel(Staging.Hotel aHotel, QuoteGroup aQuoteGroup, IWebTravelSrv aWebSrv)
        {
            var lNewHotel = new Hotel();
            lNewHotel.Name = aWebSrv.GetDBConverter().GetName(aHotel.Name);
            lNewHotel.Area = aWebSrv.GetDBConverter().GetLocation(aHotel.Location);
            lNewHotel.AAPreferredProvider = aHotel.AAPreferred;
            lNewHotel.AirPortID = aQuoteGroup.QuoteRequest.DestinationAirPortID;
            lNewHotel.Rating = aWebSrv.GetDBConverter().GetStars(aHotel.Stars);
            return lNewHotel;
        }

        private void MapAmenities(Staging.Hotel lStagingHotel, List<AmenityMap> aAmenityMap, List<Amenity> aAmenities, Hotel aHotel, IWebTravelSrv aWebSrv)
        {
            aWebSrv.GetDBConverter().MapAdultsOnly(DbContext, aAmenityMap, aAmenities, lStagingHotel, aHotel, this);
            aWebSrv.GetDBConverter().MapAllInclusive(DbContext, aAmenityMap, aAmenities, lStagingHotel, aHotel, this);

        }

        public int Save(UIHotel aUIHotel, bool aCommit = true)
        {
            var lHotel = HotelUIHelper.Convert(DbContext, aUIHotel);
            
            if (aUIHotel.AllInclusive)
                AddAmenity(lHotel, Amenity.AmenityTypes.AllInclusive);
            else
                RemoveAmenity(lHotel, Amenity.AmenityTypes.AllInclusive);

            if ( aUIHotel.AdultsOnly)
                AddAmenity(lHotel, Amenity.AmenityTypes.AdultsOnly);
            else
                RemoveAmenity(lHotel, Amenity.AmenityTypes.AdultsOnly);

            return new AccommodationDataAccess(DbContext).Save(lHotel, aCommit);
        }

        public int Save(Hotel aHotel, Agent aAgent, bool aCommit = true)
        {
            var lRet = new AccommodationDataAccess(DbContext).Save(aHotel, aCommit);
            Logger.LogInfo($"HotelBusiness::Saved {aHotel.Name} AdultsOnlyCnt={mDataAccess.GetAdultsOnly()} ");
            return lRet;
        }
        public int Save(Hotel aHotel, bool aCommit = true)
        {
            return new AccommodationDataAccess(DbContext).Save(aHotel, aCommit);
        }

        private List<Lookup> GetAccommodationsByAirPort(AirPort lTargetAirPort, List<int> aHotels)
        {
            var lResorts = new List<Hotel>();
            if ( lTargetAirPort != null)
              lResorts = new AccommodationDataAccess(DbContext).GetByAirPort(lTargetAirPort.AirPortID);

            List<Lookup> lOutput = new List<Lookup>();
            foreach (var lResort in lResorts)
                if ( aHotels.Any(x=>x == lResort.Id))
                    lOutput.Add(new Lookup() { ID = lResort.Id, Name = lResort.Name });

            return lOutput;
        }

        private AirPort GetTargetAirPort(QuoteRequest aRequest)
        {
            if (aRequest == null)
                return null;

            return aRequest.DestinationAirPort;
        }

        private List<int> GetSelectedAccommodations(QuoteGroup aQuoteGroup)
        {
            if (aQuoteGroup != null )
                return mDataAccess.GetQuoteFilteredAccommodations(aQuoteGroup);

            return new List<int>();
        }

        public List<Hotel> GetList(Agent agent)
        {
            return mDataAccess.GetAll();
        }

        public List<Hotel> GetAll ()
        {
            return mDataAccess.GetAll();
        }
        public new Hotel Get(int aHotelId)
        { 
            var lHotel = mDataAccess.Get(aHotelId);

            return lHotel;
        }

        public Hotel Create(UICompany aCompany, Agent aAgent)
        {
            Hotel lOutput = new Hotel();
            lOutput.Page = new ResortPageBusiness(DbContext).CreateNewPage(lOutput, aAgent);
            Init(aAgent, lOutput);
            Populate(lOutput, aCompany);
            return lOutput;
        }

        public List<RoomType> GetRoomTypes(int aHotelId)
        {
            return mDataAccess.GetRoomTypes(aHotelId);
        }
        public List<RoomType> GetRoomTypes(Hotel aHotel)
        {
            return mDataAccess.GetRoomTypes(aHotel.Id);
        }
    }
}
