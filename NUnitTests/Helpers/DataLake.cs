
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using WebApp.DataServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;

namespace NUnitTests.Helpers
{
    public class StagingHotelRoomQuote
    {
        public string Price { get; set; }
        public string RateTime { get; set; }
        public string RoomType { get; set; }
    }
    public class DataLake
    {
        const string ClassName = "DataLake::";
        public const string AIRPORTCODE1 = "EJW";
        public const string AIRPORTCODE2 = "BUN";
        public const string AIRPORTCODE3 = "TFY";
        public const string AIRPORTCODE4 = "ATL";
        public const string AIRPORTCODE5 = "CUN";
        public const string AIRPORTCODE6 = "RDU";
        public const string AIRPORTCODE7 = "JFK";
        public const string AIRPORTCODE8 = "LAX";

        public const string HOTEL1_NAME = "Test Hotel 1 - Adults Only";
        public const string HOTEL2_NAME = "Test Hotel 2";
        public const string HOTEL3_NAME = "Test Hotel 3";
        public const string HOTEL4_NAME = "Test Hotel 4 - Adults Only";
        public const int DELTA_AIRLINESID = 3320;
        public const int AA_AIRLINESID = 3321;
        public const int VE_AIRLINESID = 3322;
        public const int USCOUNTRYID = 23;

        public static DateTime START_DATE = DateTime.Now.AddDays(60);
        public static DateTime END_DATE = DateTime.Now.AddDays(67);

        public static List<EmailType> GetEmailTypes()
        {
            List<EmailType> lOutput = new List<EmailType>();
            lOutput.Add(new EmailType() { EmailTypeID = 1, Name = "Personnal" });
            lOutput.Add(new EmailType() { EmailTypeID = 2, Name = "Business" });
            lOutput.Add(new EmailType() { EmailTypeID = 3, Name = "Personnal 2" });

            return lOutput;
        }

        internal static void Init(RepositoryContext mContext)
        {
            Logger.Init("NUnitTests");
            Logger.LogInfo($"========================================================================================================");
            LoadAmenities(mContext);
            //DataLake.LoadHotels(mContext);
            //DataLake.LoadRoomTypes(mContext);
            DataLake.LoadAgent(mContext);
            DataLake.LoadContacts(mContext);
            DataLake.LoadEmailTypes(mContext);
            DataLake.LoadCountries(mContext);
            DataLake.LoadTourOps(mContext);
            DataLake.LoadStages(mContext);
            DataLake.LoadAirPorts(mContext);
            DataLake.LoadAgentPasswords(mContext);
            DataLake.LoadDefaultAIFilters(mContext);
            DataLake.LoadDefaultAgentFilters(mContext);
            DataLake.LoadRelationShips(mContext);
            DataLake.LoadWebPages(mContext);
        }

        public static List<WebPage> GetWebPages()
        {
            var lOutput = new List<WebPage>();
            lOutput.Add(new WebPage() { Id = 401, Name = "Atelier", Url = "https://info.eze2travel.com/tours/atelier-playa-mujeres" });
            lOutput.Add(new WebPage() { Id = 402, Name = "Valentin", Url = "https://info.eze2travel.com/tours/valentin-imperial-maya" });
            lOutput.Add(new WebPage() { Id = 403, Name = "Unico", Url = "https://info.eze2travel.com/tours/unico-20-87-riviera-maya" });
            return lOutput;
        }

        private static void LoadWebPages(RepositoryContext mContext)
        {
            var lOutput = new List<WebPage>();
            foreach ( var lPage in DataLake.GetWebPages() )
            mContext.WebPages.Add(lPage);
            mContext.SaveChanges();
        }

        private static void LoadTALogins(RepositoryContext mContext)
        {
            var lTourOps = new TourOperatorDataAccess(mContext).GetAll();

        }

        private static void LoadAmenities(RepositoryContext mContext)
        {
            var lOutput = new List<AIDefaultFilter>();
            mContext.Amenities.Add(new Amenity() { ID = (int)Amenity.AmenityTypes.AdultsOnly, Type = "Adults Only"});
            mContext.Amenities.Add(new Amenity() { ID = (int)Amenity.AmenityTypes.AllInclusive, Type = "All Inclusive" });
            mContext.SaveChanges();
        }

        private static List<AIDefaultFilter> LoadDefaultAIFilters(RepositoryContext mContext)
        {

            var lOutput = new List<AIDefaultFilter>();
            mContext.AIDefaultFilters.Add( new AIDefaultFilter() { AgentId = Defines.EZE2TRAVEL, AirPortID = 349, AirFlightFilter = 2 });
            mContext.SaveChanges();
            return lOutput;
        }

        private static List<AIDefaultFilter> LoadDefaultAgentFilters(RepositoryContext mContext)
        {

            var lOutput = new List<AIDefaultFilter>();
            mContext.AgentAirPortPreferences.Add(new AgentAirPortPreference() { AgentId = Defines.EZE2TRAVEL, AirportID = 349, AllInclusive = true, AdultOnly = true  });
            mContext.SaveChanges();
            return lOutput;
        }

        public static List<Stage> GetStages()
        {
            var lOutput = new List<Stage>();
            lOutput.Add(new Stage() { StageID = 1, Description = "Qualitification", Default = true });
            lOutput.Add(new Stage() { StageID = 2, Description = "Meeting Scheduled" });
            lOutput.Add(new Stage() { StageID = 3, Description = "Price Quote" });
            lOutput.Add(new Stage() { StageID = 4, Description = "Negotiation" });
            lOutput.Add(new Stage() { StageID = 5, Description = "Won" });
            lOutput.Add(new Stage() { StageID = 6, Description = "Loss" });
            return lOutput;
        }

        internal static void LoadAgentPasswords(RepositoryContext mContext)
        {
            mContext.WebSrvLogins.Add(new WebSrvLogin() { AgentId = DataLake.GetAgents()[0].Id, TourOperatorID = DELTA_AIRLINESID, UserName = "", Password = "" });
            mContext.WebSrvLogins.Add(new WebSrvLogin() { AgentId = DataLake.GetAgents()[1].Id, TourOperatorID = DELTA_AIRLINESID, UserName = "", Password = "" });
            mContext.WebSrvLogins.Add(new WebSrvLogin() { AgentId = DataLake.GetAgents()[2].Id, TourOperatorID = DELTA_AIRLINESID, UserName = "", Password = "" });
            mContext.WebSrvLogins.Add(new WebSrvLogin() { AgentId = DataLake.GetAgents()[0].Id, TourOperatorID = AA_AIRLINESID, UserName = "", Password = "" });
            mContext.WebSrvLogins.Add(new WebSrvLogin() { AgentId = DataLake.GetAgents()[1].Id, TourOperatorID = AA_AIRLINESID, UserName = "", Password = "" });
            mContext.WebSrvLogins.Add(new WebSrvLogin() { AgentId = DataLake.GetAgents()[2].Id, TourOperatorID = AA_AIRLINESID, UserName = "", Password = "" });
            mContext.WebSrvLogins.Add(new WebSrvLogin() { AgentId = DataLake.GetAgents()[0].Id, TourOperatorID = VE_AIRLINESID, UserName = "", Password = "" });
            mContext.WebSrvLogins.Add(new WebSrvLogin() { AgentId = DataLake.GetAgents()[1].Id, TourOperatorID = VE_AIRLINESID, UserName = "", Password = "" });
            mContext.WebSrvLogins.Add(new WebSrvLogin() { AgentId = DataLake.GetAgents()[2].Id, TourOperatorID = VE_AIRLINESID, UserName = "", Password = "" });
            mContext.SaveChanges();
        }

        public static List<Agent> GetAgents()
        {
            var lEmail1 = new Email() { Address = "Agent1@gmail.com", EmailTypeID = 2, Preferred = false };
            var lEmail11 = new Email() { Address = "PamGreen@gmail.com", EmailTypeID = 1, Preferred = true };
            var lEmail2 = new Email() { Address = "Agent2@gmail.com", EmailTypeID = 2, Preferred = false };
            var lEmail21 = new Email() { Address = "SandrySmith@gmail.com", EmailTypeID = 1, Preferred = true };
            var lEmail3 = new Email() { Address = "Agent3@gmail.com", EmailTypeID = 2, Preferred = false };
            var lEmail31 = new Email() { Address = "RachealRay@gmail.com", EmailTypeID = 1, Preferred = true };

            List<Agent> lOutput = new List<Agent>();
            var lAgent1 = new Agent() { Id = "Pam", First = "Pam", Last = "Green", Gender = Gender.Female,  Emails = new List<Email>() { lEmail1, lEmail11 }, PhoneNumbers = new List<Phone>() { new Phone() {  UserId = "Pam", PhoneNumber = "123456789", Defaut = true } } };
            var lAgent2 = new Agent() { Id = "Sandry", First = "Sandry", Last = "Smith", Gender = Gender.Female, Emails = new List<Email>() { lEmail2, lEmail21 }, PhoneNumbers = new List<Phone>() { new Phone() { UserId = "Sandry", PhoneNumber = "123456787", Defaut = true } } };
            var lAgent3 = new Agent() { Id = "Chuck", First = "Chuck", Last = "Walker", Gender = Gender.Male, Emails = new List<Email>() { lEmail3, lEmail31 }, PhoneNumbers = new List<Phone>() { new Phone() { UserId = "Chuck", PhoneNumber = "123456784", Defaut = true } } };

            lOutput.Add(lAgent1);
            lOutput.Add(lAgent2);
            lOutput.Add(lAgent3);

            return lOutput;
        }

        internal static void SetAdultsOnlyResorts(RepositoryContext aContext)
        {
            //NUnit.Framework.Assert.Greater(aContext.Accommodations.Count(), 3, "This should be called after the searched is executed");
            aContext.Accommodations.Add(new Resort() { Id = 8000, Name = "TEST - Hyatt Ziva Cancun", Amenities = new List<AmenityMap>() { new AmenityMap() { AccommodationID = 7, AmenityID = (int)Amenity.AmenityTypes.AdultsOnly } } });
            aContext.Accommodations.Add(new Resort() { Id = 8001, Name = "TEST - UNICO 20º87º Hotel Riviera Maya - Adults Only", Amenities = new List<AmenityMap>() { new AmenityMap() { AccommodationID = 7, AmenityID = (int)Amenity.AmenityTypes.AdultsOnly } } });
            aContext.Accommodations.Add(new Resort() { Id = 8002, Name = "TEST - Panama Jack Resorts Playa del Carmen", Amenities = new List<AmenityMap>() { new AmenityMap() { AccommodationID = 7, AmenityID = (int)Amenity.AmenityTypes.AdultsOnly } } });
            //aContext.Accommodations.Where(x => x.AccommodationID == 7).ToList().ForEach(x => x.AdultsOnly = true);
            //aContext.Accommodations.Where(x => x.AccommodationID == 58).ToList().ForEach(x => x.AdultsOnly = true);
            //aContext.Accommodations.Where(x => x.AccommodationID == 78).ToList().ForEach(x => x.AdultsOnly = true);
            aContext.SaveChanges();
        }

        internal static void SetAllInclusiveResorts(RepositoryContext aContext)
        {
            //NUnit.Framework.Assert.Greater(aContext.Accommodations.Count(), 3, "This should be called after the searched is executed");
            aContext.Accommodations.Add(new Resort() { Id = 7, Name = "Hyatt Ziva Cancun", Amenities = new List<AmenityMap>() { new AmenityMap() { AccommodationID = 7, AmenityID = (int)Amenity.AmenityTypes.AdultsOnly } } });
            aContext.Accommodations.Add(new Resort() { Id = 58, Name = "UNICO 20º87º Hotel Riviera Maya - Adults Only", Amenities = new List<AmenityMap>() { new AmenityMap() { AccommodationID = 7, AmenityID = (int)Amenity.AmenityTypes.AdultsOnly } } });
            aContext.Accommodations.Add(new Resort() { Id = 78, Name = "Panama Jack Resorts Playa del Carmen", Amenities = new List<AmenityMap>() { new AmenityMap() { AccommodationID = 7, AmenityID = (int)Amenity.AmenityTypes.AdultsOnly } } });
            //aContext.Accommodations.Where(x => x.AccommodationID == 7).ToList().ForEach(x => x.AdultsOnly = true);
            //aContext.Accommodations.Where(x => x.AccommodationID == 58).ToList().ForEach(x => x.AdultsOnly = true);
            //aContext.Accommodations.Where(x => x.AccommodationID == 78).ToList().ForEach(x => x.AdultsOnly = true);
            aContext.SaveChanges();
        }

        internal static void ClearStagingData(RepositoryContext aContext)
        {
            aContext.Staging_HotelRates.RemoveRange(aContext.Staging_HotelRates);
            aContext.Staging_Hotels.RemoveRange(aContext.Staging_Hotels);
            aContext.Staging_Flights.RemoveRange(aContext.Staging_Flights);
        }

        public static List<AirPort> GetAirPorts()
        {
            var lOutput = new List<AirPort>();
            lOutput.Add(new AirPort() { AirPortID = 344, Name = "AirPort1 (" +AIRPORTCODE1+")", Code = AIRPORTCODE1, CountryId = 23, Default = true});
            lOutput.Add(new AirPort() { AirPortID = 345, Name = "AirPort2 (" + AIRPORTCODE2 + ")", Code = AIRPORTCODE2, CountryId = 23 });
            lOutput.Add(new AirPort() { AirPortID = 346, Name = "AirPort3 (" + AIRPORTCODE3 + ")", Code = AIRPORTCODE3, CountryId = 23 });
            lOutput.Add(new AirPort() { AirPortID = 347, Name = "AirPort4 (" + AIRPORTCODE4 + ")", Code = AIRPORTCODE4, CountryId = 23 });
            lOutput.Add(new AirPort() { AirPortID = 348, Name = "AirPort5 (" + AIRPORTCODE5 + ")", Code = AIRPORTCODE5, CountryId = 23 });
            lOutput.Add(new AirPort() { AirPortID = 349, Name = "AirPort6 (" + AIRPORTCODE6 + ")", Code = AIRPORTCODE6, CountryId = 23 });
            lOutput.Add(new AirPort() { AirPortID = 10, Name = "AirPort7 (" + AIRPORTCODE7+ ")", Code = AIRPORTCODE7 });
            lOutput.Add(new AirPort() { AirPortID = 150, Name = "Los Angles (" + AIRPORTCODE8 + ")", Code = AIRPORTCODE8, City = "Los Angles",  State = "CA", CountryId = 23 });
            return lOutput;
        }

        internal static List<SKU> GetRoomTypes(RepositoryContext aContext)
        {
            var lOutput = new List<SKU>();
            var lHotels = aContext.Accommodations.Where(x => x.Id > 0).ToList();
            lOutput.Add(new RoomType() { Name = "Ocean View", AccommodiationID = lHotels[0].Id });
            lOutput.Add(new RoomType() { Name = "Junior View", AccommodiationID = lHotels[0].Id });
            lOutput.Add(new RoomType() { Name = "Presidual Suite", AccommodiationID = lHotels[1].Id });
            lOutput.Add(new RoomType() { Name = "Balcony Bay", AccommodiationID = lHotels[1].Id });
            lOutput.Add(new RoomType() { Name = "Silver Superior Suites", AccommodiationID = lHotels[2].Id });
            lOutput.Add(new RoomType() { Name = "Golden Suite", AccommodiationID = lHotels[2].Id });
            lOutput.Add(new RoomType() { Name = "Garden View King", AccommodiationID = lHotels[3].Id });
            lOutput.Add(new RoomType() { Name = "Ocean View 2 Doubles", AccommodiationID = lHotels[3].Id });

            return lOutput;
        }

        public static List<UIContact> CreateUIContacts()
        {
            var lLeads = new List<UIContact>();
            lLeads.Add(new UIContact() { First = "Wife", Last = "Tester", DOB = "1/2/70", Cell = "(919)123-3456", PrimaryEmail = "User2@gmail.com", RelationshipID = 2, Relationship = "Wife"});
            lLeads.Add( new UIContact() { First = "Father", Last = "Tester", DOB = "1/2/67", Cell = "(919)123-3456", PrimaryEmail = "User1@gmail.com", RelationshipID = 1, Relationship = "Husband" });
            lLeads.Add(new UIContact() { First = "Son", Last = "Tester", DOB = "1/2/03", PrimaryEmail = null, RelationshipID = 3, Relationship = "Son" });
            lLeads.Add(new UIContact() { First = "Daughter", Last = "Tester", DOB = "1/2/99", PrimaryEmail = null, RelationshipID = 4, Relationship = "Daughter" });
            return lLeads;
        }

        QuoteRequestResort CreateQuoteRequestResort()
        {
            QuoteRequestResort lOutput = new QuoteRequestResort();

            //lOutput.

            return lOutput;
        }

        public static UIQuoteRequest CreateUIQuoteRequest(int aRequestID = 105, string aDepart = AIRPORTCODE6, string aDest = AIRPORTCODE5)
        {
            return new UIQuoteRequest()
            {
                QuoteID = aRequestID,
                StartDate = START_DATE.ToShortDateString(),
                EndDate = END_DATE.ToShortDateString(),
                When = DateTime.Now.ToShortDateString(),
                DepartureCityCode = aDepart,
                DestinationCityCode =aDest,
                AgentId = GetAgents()[0].Id,
                RefferalId = 1,
                Contacts = CreateUIContacts()
            };
        }


        public static List<Resort> GetHotels()
        {
            var lOutput = new List<Resort>();
            lOutput.Add(new Resort() { Name = HOTEL1_NAME, CountryId = USCOUNTRYID, AirPortID = 344 });
            lOutput.Add(new Resort() { Name = HOTEL2_NAME, CountryId = USCOUNTRYID, AirPortID = 344 });
            lOutput.Add(new Resort() { Name = HOTEL3_NAME, CountryId = USCOUNTRYID, AirPortID = 344 });
            lOutput.Add(new Resort() { Name = HOTEL4_NAME, CountryId = USCOUNTRYID, AirPortID = 344 });
            return lOutput;
        }

        public static List<Client> GetClients()
        {
            List<Client> lOutput = new List<Client>();
            var lEmail11 = new Email() { Address = "AliciaKeys@gmail.com", EmailTypeID = 1, Preferred = true };
            var lEmail21 = new Email() { Address = "TracyMorgan@gmail.com", EmailTypeID = 1, Preferred = true };
            var lEmail31 = new Email() { Address = "StivelCambell@gmail.com", EmailTypeID = 1, Preferred = true };

            var lFather = new Client() { Id = "Father", Gender = Gender.Male, First = "Father", Last = "Tester", DOB = new DateTime(1967, 1, 2), PhoneNumbers = new List<Phone>() { new Phone() { PhoneNumber = "(919)123-3456" } }, Emails =  new List<Email>() { new Email() { Address = "User1@gmail.com" } }, RelationshipID = 1 };
            var lContact1 = new Client() { Id = "Alicia", Gender = Gender.Female, First = "Alicia", Last = "Keys", Emails = new List<Email>() { lEmail11 } };
            var lContact2 = new Client() { Id = "Tracy", Gender = Gender.Female,  First = "Tracy", Last = "Morgan", Emails = new List<Email>() { lEmail21 } };
            var lContact3 = new Client() { Id = "Stivel", Gender = Gender.Male, First = "Stivel", Last = "Cambell", Emails = new List<Email>() { lEmail31 } };
            lOutput.Add(lFather);
            lOutput.Add(lContact1);
            lOutput.Add(lContact2);
            lOutput.Add(lContact3);
            return lOutput;
        }
        public static List<UIContact> GetUIContacts()
        {
            List<UIContact> lOutput = new List<UIContact>();
            var lEmail11 = new Email() { Address = "AliciaKeys@gmail.com", EmailTypeID = 1, Preferred = true };
            var lEmail21 = new Email() { Address = "TracyMorgan@gmail.com", EmailTypeID = 1, Preferred = true };
            var lEmail31 = new Email() { Address = "StivelCambell@gmail.com", EmailTypeID = 1, Preferred = true };

            var lFather = new UIContact() { Id = "Father", First = "Father", Last = "Tester", DOB = "5/11/1960", PrimaryEmail ="User1@gmail.com"};
            var lContact1 = new UIContact() { Id = "Alicia", First = "Alicia", Last = "Keys" };
            lOutput.Add(lFather);
            lOutput.Add(lContact1);
            return lOutput;
        }

        public static List<TourOperator> GetTourOperators()
        {
            List<TourOperator> lOutput = new List<TourOperator>();
            lOutput.Add(new TourOperator() { Id = DataLake.AA_AIRLINESID, Name = TourOperator.AA_VACATIONS, BusinessTypeID = 9});
            lOutput.Add(new TourOperator() { Id = DataLake.DELTA_AIRLINESID, Name = TourOperator.DELTA_VACATIONS, BusinessTypeID = 9 });
            lOutput.Add(new TourOperator() { Id = DataLake.VE_AIRLINESID, Name = TourOperator.VACATION_EXPRESS, BusinessTypeID = 9 });

            return lOutput;
        }

        public static void LoadStages(RepositoryContext aContext)
        {
            aContext.Stages.AddRange(DataLake.GetStages());
            aContext.SaveChanges();
        }

        public static void LoadAgent(RepositoryContext aContext)
        {
            aContext.Agents.AddRange(DataLake.GetAgents());
            aContext.SaveChanges();
        }
        public static void LoadContacts(RepositoryContext aContext)
        {
            aContext.Contacts.AddRange(DataLake.GetClients());
            aContext.SaveChanges();
        }
        public static void LoadHotels(RepositoryContext aContext)
        {


            aContext.Accommodations.AddRange(DataLake.GetHotels());
            aContext.SaveChanges();

            var lCount = aContext.Accommodations.Count();
        }



        public static List<StagingHotelRoomQuote> GetRoomTypes() {
            var lOutput = new List<StagingHotelRoomQuote>();
            lOutput.Add(new StagingHotelRoomQuote() { Price = "$123,442.00", RateTime = "AI", RoomType = "Hotel 1.Room Type1" });
            lOutput.Add(new StagingHotelRoomQuote() { Price = "$3,982.33", RateTime = "AI", RoomType = "Hotel 1.Room Type2" });
            lOutput.Add(new StagingHotelRoomQuote() { Price = "\"442.00\"", RateTime = "EP", RoomType = "Hotel 2.Room Type21" });
            lOutput.Add(new StagingHotelRoomQuote() { Price = "$1442.00", RateTime = "EP", RoomType = "Hotel 2.Room Type22" });
            lOutput.Add(new StagingHotelRoomQuote() { Price = "$32242.00", RateTime = "AI", RoomType = "Hotel 3.Room Type31" });
            lOutput.Add(new StagingHotelRoomQuote() { Price = "$123,442.00", RateTime = "EP", RoomType = "Hotel 3.Room Type32" });
            lOutput.Add(new StagingHotelRoomQuote() { Price = "$123,442.00", RateTime = "EP", RoomType = "Hotel 4.Room Type41" });
            lOutput.Add(new StagingHotelRoomQuote() { Price = "$123,442.00", RateTime = "AI", RoomType = "Hotel 4.Room Type42" });
            return lOutput;
        }

        public static List<Staging.HotelRate> GetStagingHotelRate(QuoteGroup aGroup, int aBase = 1)
        {
            var lOutput = new List<Staging.HotelRate>();
            var lRates = DataLake.CreateHotelRates();
            var lRoomTypes = DataLake.GetRoomTypes();
            lOutput.Add(new Staging.HotelRate() { HotelStagingID = aBase, Price = lRoomTypes[0].Price, RateType = lRoomTypes[0].RateTime, RoomType = lRoomTypes[0].RoomType });
            lOutput.Add(new Staging.HotelRate() { HotelStagingID = aBase, Price = lRoomTypes[1].Price, RateType = lRoomTypes[1].RateTime, RoomType = lRoomTypes[1].RoomType });
            lOutput.Add(new Staging.HotelRate() { HotelStagingID = aBase + 1, Price = lRoomTypes[2].Price, RateType = lRoomTypes[2].RateTime, RoomType = lRoomTypes[2].RoomType });
            lOutput.Add(new Staging.HotelRate() { HotelStagingID = aBase + 1, Price = lRoomTypes[3].Price, RateType = lRoomTypes[3].RateTime, RoomType = lRoomTypes[3].RoomType });
            lOutput.Add(new Staging.HotelRate() { HotelStagingID = aBase + 2, Price = lRoomTypes[4].Price, RateType = lRoomTypes[4].RateTime, RoomType = lRoomTypes[4].RoomType });
            lOutput.Add(new Staging.HotelRate() { HotelStagingID = aBase + 2, Price = lRoomTypes[5].Price, RateType = lRoomTypes[5].RateTime, RoomType = lRoomTypes[5].RoomType });
            lOutput.Add(new Staging.HotelRate() { HotelStagingID = aBase + 3, Price = lRoomTypes[6].Price, RateType = lRoomTypes[6].RateTime, RoomType = lRoomTypes[6].RoomType });
            lOutput.Add(new Staging.HotelRate() { HotelStagingID = aBase + 3, Price = lRoomTypes[7].Price, RateType = lRoomTypes[7].RateTime, RoomType = lRoomTypes[7].RoomType });
            return lOutput;
        }
        public static List<Staging.Hotel> GetStagingHotels(QuoteGroup aQuoteGroup, int aBase = 1)
        {
            var lOutput = new List<Staging.Hotel>();
            var lRates = GetStagingHotelRate(aQuoteGroup);
            var lHotel1 = new Staging.Hotel() { HotelStagingID = aBase, Name = DataLake.HOTEL1_NAME, QuoteGroupId = aQuoteGroup.Id, Location = "Location : Test Location 1", TourOperatorID = DataLake.DELTA_AIRLINESID, HotelRateTypes = lRates.GetRange(0, 2) };

            lHotel1.Amenities.Add(new AmenityMap() { AccommodationID = aBase, AmenityID = (int)Amenity.AmenityTypes.AdultsOnly });
            lOutput.Add(lHotel1);
            
            lOutput.Add(new Staging.Hotel() { HotelStagingID = aBase + 1, Name = DataLake.HOTEL2_NAME, QuoteGroupId = aQuoteGroup.Id, Location = "Location : Test Location 2", TourOperatorID = DataLake.DELTA_AIRLINESID, HotelRateTypes = lRates.GetRange(2, 2) });

            var lHotel3 = new Staging.Hotel() { HotelStagingID = aBase + 2, Name = DataLake.HOTEL3_NAME, QuoteGroupId = aQuoteGroup.Id, Location = "Location : Test Location 3", TourOperatorID = DataLake.DELTA_AIRLINESID, HotelRateTypes = lRates.GetRange(4, 2) };
            lOutput.Add(lHotel3);
            var lHotel4 = new Staging.Hotel() { HotelStagingID = aBase + 3, Name = DataLake.HOTEL4_NAME, QuoteGroupId = aQuoteGroup.Id, Location = "Location : Test Location 4", TourOperatorID = DataLake.DELTA_AIRLINESID, HotelRateTypes = lRates.GetRange(6, 2) };
            lOutput.Add(lHotel4);

            return lOutput;
        }

        public static void LoadHotelStagging(RepositoryContext aContext, Dictionary<int, double> aData, QuoteGroup aQuoteGroup)
        {
            var lHotels = GetStagingHotels(aQuoteGroup);
            Staging.Hotel lHotelStaging1 = lHotels[0];
            Staging.Hotel lHotelStaging2 = lHotels[1];
            Staging.Hotel lHotelStaging3 = lHotels[2];
            Staging.Hotel lHotelStaging4 = lHotels[3];

            List<SKU> lRoomTypes = aContext.SKUs.ToList();

            aContext.Staging_Hotels.Add(lHotelStaging1);
            aContext.SaveChanges();
            Staging.HotelRate lRateStating11 = new Staging.HotelRate() { HotelStagingID = lHotelStaging1.HotelStagingID, Price = aData[0].ToString("C2"), RateType = "EP", RoomType = lRoomTypes[0].Name };
            Staging.HotelRate lRateStating12 = new Staging.HotelRate() { HotelStagingID = lHotelStaging1.HotelStagingID, Price = aData[1].ToString("C2"), RateType = "EP", RoomType = lRoomTypes[1].Name };
            aContext.Staging_HotelRates.Add(lRateStating11);
            aContext.Staging_HotelRates.Add(lRateStating12);
            aContext.SaveChanges();

            aContext.Staging_Hotels.Add(lHotelStaging2);
            aContext.SaveChanges();
            Staging.HotelRate lRateStating21 = new Staging.HotelRate() { HotelStagingID = lHotelStaging2.HotelStagingID, Price = aData[2].ToString("C2"), RateType = "EP", RoomType = lRoomTypes[2].Name };
            Staging.HotelRate lRateStating22 = new Staging.HotelRate() { HotelStagingID = lHotelStaging2.HotelStagingID, Price = aData[3].ToString("C2"), RateType = "EP", RoomType = lRoomTypes[3].Name };
            aContext.Staging_HotelRates.Add(lRateStating21);
            aContext.Staging_HotelRates.Add(lRateStating22);
            aContext.SaveChanges();

            aContext.Staging_Hotels.Add(lHotelStaging3);
            aContext.SaveChanges();
            Staging.HotelRate lRateStating31 = new Staging.HotelRate() { HotelStagingID = lHotelStaging3.HotelStagingID, Price = aData[4].ToString("C2"), RateType = "EP", RoomType = lRoomTypes[4].Name };
            Staging.HotelRate lRateStating32 = new Staging.HotelRate() { HotelStagingID = lHotelStaging3.HotelStagingID, Price = aData[5].ToString("C2"), RateType = "EP", RoomType = lRoomTypes[5].Name };
            aContext.Staging_HotelRates.Add(lRateStating31);
            aContext.Staging_HotelRates.Add(lRateStating32);
            aContext.SaveChanges();

            aContext.Staging_Hotels.Add(lHotelStaging4);
            aContext.SaveChanges();
            Staging.HotelRate lRateStating41 = new Staging.HotelRate() { HotelStagingID = lHotelStaging4.HotelStagingID, Price = aData[6].ToString("C2"), RateType = "EP", RoomType = lRoomTypes[6].Name };
            Staging.HotelRate lRateStating42 = new Staging.HotelRate() { HotelStagingID = lHotelStaging4.HotelStagingID, Price = aData[7].ToString("C2"), RateType = "EP", RoomType = lRoomTypes[7].Name };
            aContext.Staging_HotelRates.Add(lRateStating41);
            aContext.Staging_HotelRates.Add(lRateStating42);
            aContext.SaveChanges();

            int lStaggingID = lHotelStaging1.HotelStagingID;
        }

        public static void LoadRelationShips(RepositoryContext aContext)
        {
            aContext.Relationships.Add(new Relationship() {  RelationshipID = 2, Description = "Wife" } );
            aContext.Relationships.Add(new Relationship() { RelationshipID = 1, Description = "Husband" });
            aContext.Relationships.Add(new Relationship() { RelationshipID = 3, Description = "Son" });
            aContext.Relationships.Add(new Relationship() { RelationshipID = 4, Description = "Daughter" });
            aContext.SaveChanges();
        }

        public static void LoadRoomTypes(RepositoryContext aContext)
        {


            aContext.SKUs.AddRange(DataLake.GetRoomTypes(aContext));
            aContext.SaveChanges();

            int lCount = aContext.SKUs.Count();
        }


        public static void LoadAirPorts(RepositoryContext aContext)
        {
            aContext.AirPorts.AddRange(GetAirPorts());
            aContext.SaveChanges();
        }

        public static void LoadTourOps(RepositoryContext aContext)
        {
            aContext.TourOperators.AddRange(DataLake.GetTourOperators());
            aContext.SaveChanges();
        }

        public static void LoadCountries(RepositoryContext aContext)
        {
            aContext.Countries.AddRange(DataLake.GetCountries());
            aContext.SaveChanges();
        }

        private static List<Country> GetCountries()
        {
            var lOutput = new List<Country>();
            lOutput.Add(new Country() { Id = USCOUNTRYID, Name = "US" });
            return lOutput;
        }

        public static void LoadEmailTypes(RepositoryContext aContext)
        {

            aContext.EmailTypes.AddRange(DataLake.GetEmailTypes());
            aContext.SaveChanges();
        }

        public static Dictionary<int, double> CreateHotelRates()
        {
            Dictionary<int, double> lResortQuotes = new Dictionary<int, double>();
            lResortQuotes[0] = 123456.00;
            lResortQuotes[1] = 789123.00;
            lResortQuotes[2] = 11111.00;
            lResortQuotes[3] = 222.34;
            lResortQuotes[4] = 3333.55;
            lResortQuotes[5] = 445678.66;
            lResortQuotes[6] = 99999.99;
            lResortQuotes[7] = 34.56;
            return lResortQuotes;
        }

        public static  Email CreateEmail(RepositoryContext aContext)
        {
            return new Email() { Address = "User1@gmail.com", Preferred = true, Type = aContext.EmailTypes.FirstOrDefault(), EmailTypeID = 1 };
        }

        public static UIQuoteRequest CreateTestQuoteRequest(RepositoryContext aContext, string aDeptCode = DataLake.AIRPORTCODE1, string aDestCode = DataLake.AIRPORTCODE2)
        {
            if (aContext.Agents.Count() == 0)
                throw new InvalidDataException("Agent Table required to be populated before you can call this method");
            UIQuoteRequest lRequest = new UIQuoteRequest() { StartDate = "10/1/2020", EndDate = "10/10/2020", When = "8/1/2020" };
            lRequest.DepartureCityCode = aDeptCode;
            lRequest.DestinationCityCode = aDestCode;
            lRequest.Agent = ContactUIHelper.Convert( aContext.Agents.Where(x => x.Id == DataLake.GetAgents()[0].Id).FirstOrDefault());
            lRequest.AgentId = lRequest.Agent.Id;
            UIContact lLead = new UIContact() { RelationshipID = 1, First = "User1", Last = "Tester", DOB = "6/6/1960", PrimaryEmail = "User1.Tester@gmail.com" };
            lRequest.Contacts.Add(lLead);
            return lRequest;
        }

    }

}