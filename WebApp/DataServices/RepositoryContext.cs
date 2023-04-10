using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;

namespace WebApp.DataServices
{
    public class RepositoryContext :  IdentityDbContext<BlitzerUser, IdentityRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>, IDbContext
    {
        public DbSet<LogMsg> LogMsgs { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public override DbSet<BlitzerUser> Users { get; set; }
        public DbSet<Contact> AppUsers { get; set; }
        public DbSet<UserMap> UserMaps { get; set; }
        public DbSet<Comparable> Comparables { get; set; }
        public DbSet<UserTracking> UserTrackings { get; set; }
        public DbSet<BlitzerCore.Models.UI.Gallary> GallaryPages { get; set; }
        public DbSet<BlitzerCore.Models.UI.UIRanking> RankingPages { get; set; }
        public DbSet<BlitzerCore.Models.Ranking> ResortRankings { get; set; }
        public DbSet<BlitzerCore.Models.UI.UIResortPage> ResortPages { get; set; }
        public DbSet<BlitzerCore.Models.PageToBlockMap> PageToBlockMap { get; set; }
        public DbSet<BlitzerCore.Models.BlockToPageMap> BlockToPageMap { get; set; }
        public DbSet<BlitzerCore.Models.UI.UICountry> UICountries { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<BlitzerCore.Models.DataMap> DataMaps { get; set; }
        public DbSet<PageType> PageTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Graphic> Graphics { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Tile> Tiles { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<AmenityMap> AmenityMaps { get; set; }
        public DbSet<AIDefaultFilter> AIDefaultFilters { get; set; }
        public DbSet<AIFilterMAP> AIFilterMaps { get; set; }
        public DbSet<ErrorMsg> ErrorMsgs { get; set; }
        public DbSet<AgentAirPortPreference> AgentAirPortPreferences { get; set; }
        public DbSet<AIFilter> AIFilters { get; set; }
        public DbSet<WebSrvLogin> WebSrvLogins { get; set; }
        public DbSet<QuoteToResultsMapper> QuoteToResultsMappers { get; set; }

        public DbSet<FilteredAccommodation> FilteredAccommodations { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Client> Leads { get; set; }
        public DbSet<AirPort> AirPorts { get; set; }
        public DbSet<QuoteRequest> QuoteRequests { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<AccountStatus> GetAccountStatuses { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }
        public DbSet<Hotel> Accommodations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<MerchantServices> MerchantServices { get; set; }
        public DbSet<TripComponent> TripComponents { get; set; }
        public DbSet<SavedAd> SavedAds { get; set; }
        public DbSet<UserLocationPreference> UserLocationPreferences { get; set; }
        public DbSet<Transportation> Transportations { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<SubPage> SubPages { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<MarketingAd> MarketingAds { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<AirPort> Airports { get; set; }
        public DbSet<Hotel> RequestedHotels { get; set; }
        public DbSet<Staging.Hotel> Staging_Hotels { get; set; }
        public DbSet<Staging.HotelRate> Staging_HotelRates { get; set; }
        public DbSet<Staging.Flight> Staging_Flights { get; set; }
        public DbSet<Flight> RequestedFlights { get; set; }

        public RepositoryContext()
        {
            this.Database.EnsureCreated();
        }
        public System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

//        public string ConnectionString
//        {
//            get
//            {
                
//                var appSettings = ConfigurationManager.GetSection("connectionStrings");
//#if DEBUG
//                return ConfigurationManager.ConnectionStrings["Beta"].ConnectionString;
//                //return ConfigurationManager.ConnectionStrings["Blitzer"].ConnectionString;
//#else
//                return ConfigurationManager.ConnectionStrings["Blitzer"].ConnectionString;
//#endif
//            }
//        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var lCS = @"server=tcp:blitzersrv.database.windows.net;user id=devuser;password=Blitz#8888;database=Beta;";
            //optionsBuilder.UseSqlServer(lCS);
            //optionsBuilder.UseSqlServer(lCS, b => b.MigrationsAssembly("WebApp"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SavedAd>().HasKey(x => new { x.UserID, x.AdID });
            builder.Entity<UserLocationPreference>().HasKey(x => new { x.UserID, x.UserPreference });
            builder.Entity<ContactTrip>          ().HasKey(x => new { x.ContactId, x.TripID });
            builder.Entity<TripComponent>().HasKey(x => new { x.TransportationID, x.AccommodationID });
            builder.Entity<MerchantServices>().HasKey(x => new { x.MerchantID, x.ServiceID });
            builder.Entity<QuoteRequestResortFilter>().HasKey(x => new { x.QuoteRequestID, x.AccommodationID });
            builder.Entity<QuoteRequestTourOperatorFilter>().HasKey(x => new { x.QuoteRequestID, x.TourOperatorID });
            builder.Entity<Ad>().HasMany(u => u.Blobs).WithOne(e => e.Ad);
            builder.Entity<QuoteRequest>().HasMany(u => u.Tickets).WithOne(e => e.QuoteRequest);
            //builder.Entity<QuoteGroup>().HasMany(u => u.TransportationFilters).WithOne(e => e.QuoteGroup);
            builder.Entity<QuoteRequest>().HasMany(u => u.QuoteGroups).WithOne(e => e.QuoteRequest);
            builder.Entity<QuoteRequest>().HasMany(u => u.HotelFilters).WithOne(e => e.QuoteRequest);
            builder.Entity<Booking>().HasMany(u => u.Payments).WithOne(e => e.Booking);
            builder.Entity<Booking>().HasMany(u => u.Credits).WithOne(e => e.OriginalBooking);
            builder.Entity<Trip>().HasMany(u => u.Bookings).WithOne(e => e.Trip);
            builder.Entity<Team>().HasMany(u => u.Agents).WithOne(e => e.PrimaryTeam);
            builder.Entity<Opportunity>().HasMany(u => u.Tasks).WithOne(e => e.Opportunity);
            builder.Entity<TourOperator>().HasMany(u => u.Bookings).WithOne(e => e.TourOperator);
            builder.Entity<Company>().HasMany(u => u.Contacts).WithOne(e => e.Employer);
            builder.Entity<Sprint>().HasMany(u => u.UserStories).WithOne(e => e.Sprint);
            builder.Entity<UserStory>().HasMany(u => u.Work).WithOne(e => e.UserStory);
            builder.Entity<Feature>().HasMany(u => u.UserStories).WithOne(e => e.Feature);
            builder.Entity<BlitzSystem>().HasMany(u => u.Features).WithOne(e => e.System);
            builder.Entity<HouseHold>().HasMany(u => u.Members).WithOne(e => e.HouseHold);
            builder.Entity<Company>().HasMany(u => u.NoteEntries).WithOne(e => e.Company);
            builder.Entity<Opportunity>().HasMany(u => u.NoteEntries).WithOne(e => e.Opportunity);
            builder.Entity<Contact>().HasMany(u => u.Cards).WithOne(e => e.Owner);
            builder.Entity<Contact>().HasMany(u => u.Credits).WithOne(e => e.Traveler);
            builder.Entity<Contact>().HasMany(u => u.MemberShips).WithOne(e => e.Contact);
            builder.Entity<Contact>().HasMany(u => u.NoteEntries).WithOne(e => e.Contact);
            builder.Entity<Contact>().HasMany(u => u.PhoneNumbers).WithOne(e => e.User);
            builder.Entity<Contact>().HasMany(u => u.Emails).WithOne(e => e.User);
            builder.Entity<Contact>().HasOne(u => u.OwnedBy).WithMany(e => e.Clients);
            builder.Entity<QuoteGroup>().HasMany(u => u.ClientViews).WithOne(e => e.QuoteGroup);
            builder.Entity<QuoteGroup>().HasMany(u => u.Quotes).WithOne(e => e.QuoteGroup);
            builder.Entity<QuoteGroup>().HasMany(u => u.BotQuotes).WithOne(e => e.QuoteGroup);
            builder.Entity<QuoteGroup>().HasMany(u=>u.Flights).WithOne(e=>e.QuoteGroup);
            builder.Entity<AgentAirPortPreference>().HasMany(u => u.PreferredHotels).WithOne(e => e.Preference);
            builder.Entity<FilteredAccommodation>().HasMany(u => u.IncludedSKUs).WithOne(e => e.FilteredAccommodation);
            builder.Entity<Filter>().HasMany(u => u.Accommodations).WithOne(e => e.Filter);
            builder.Entity<Presentation>().HasMany(u => u.Queue).WithOne(e => e.Presentation);
        }
        public DbSet<BlitzerCore.Models.MarketingAd> MarketingAd { get; set; }

        public DbSet<BlitzerCore.Models.Merchant> Merchant { get; set; }

        public DbSet<BlitzerCore.Models.Phone> PhoneNumbers { get; set; }

        public DbSet<BlitzerCore.Models.Invoice> Invoice { get; set; }
        public DbSet<BlitzerCore.Models.QuoteRequestResortFilter> QuoteRequestResortFilter { get; set; }
        public DbSet<BlitzerCore.Models.QuoteRequestResort> QuoteRequestResorts { get; set; }
        public DbSet<BlitzerCore.Models.FlightItinerary> FlightItineraries { get; set; }
        public DbSet<BlitzerCore.Models.Flight> Flights { get; set; }
        public DbSet<BlitzerCore.Models.SKU> SKUs { get; set; }
        public DbSet<BlitzerCore.Models.TourOperator> TourOperators { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<BlitzerCore.Models.Booking> Booking { get; set; }
        public DbSet<BlitzerCore.Models.Resort> Resort { get; set; }
        public DbSet<BlitzerCore.Models.UI.Content> Content { get; set; }
        public DbSet<BlitzerCore.Models.UI.Block> Block { get; set; }

        // Added in Version 2.1
        public DbSet<RelationshipMap> RelationshipMaps { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<FOP> CreditCards { get; set; }

        // Version 3.1 12/30/2020
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<ContactSubType> ContactSubTypes { get; set; }
        // Version 3.2 1/1/2021
        public DbSet<HouseHold> HouseHolds { get; set; }
        public DbSet<QuoteGroup> QuoteGroups { get; set; }
        public DbSet<BlitzerCore.Models.UI.UIContact> UIContact { get; set; }
        // Version 3.3 1/9/2021
        public DbSet<DBVersion> DBVersions { get; set; }
        // Version 3.3 1/9/2021
        public DbSet<Payment> Payments { get; set; }
        // Pulled in Tasks and Teams from TOT
        public DbSet<Team> Teams { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<NewsFeed> NewsFeeds { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<BlitzSystem> Systems { get; set; }
        // 2/15/2021
        public DbSet<ReferralSource> Referrals { get; set; }

        public void BeginTransaction()
        {
            this.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void EndTransaction()
        {

        }

        public int ExecCommand (string aCmd)
        {
            return this.Database.ExecuteSqlRaw(aCmd);
        }

        public void UnTrack(Opportunity aOpp)
        {
            this.Entry(aOpp).State = EntityState.Detached;
            //Logger.LogInfo("XXXX <--- UnTracking of " + aOpp.Activity);

        }
        // 2/27/2021
        public DbSet<Invoice> Invoices { get; set; }

        //4/13/2021
        public DbSet<Credit> Credits { get; set; }
        // 5/4/2021
        public DbSet<AgentProfile> AgentProfiles { get; set; }
        // 5/4/2021
        public DbSet<BlitzerCore.Models.TaskTemplate> TaskTemplate { get; set; }
        //8/22/2021
        public DbSet<QuoteSKUs> QuoteSKUs { get; set; }
        public DbSet <RoomType> RoomTypes { get; set; }
        public DbSet <Cruise> Cruises { get; set; }
        public DbSet<Tour> Tours { get; set; }
        // 8/25/2021
        public DbSet<ClientView> ClientViews { get; set; }
        // 10/7/2021
        public DbSet<NameReplacement> NameReplacements { get; set; }
        // 10/27/2021
        public DbSet<CruiseLine> CruiseLines { get; set; }
        // 10/31/2021
        public DbSet<RegisterBooking> RegisterBookings { get; set; }
        // 11/8/2021
        public DbSet<File> Files { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        // 11/12/2021
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TaggedObject> TaggedObjects { get; set; }
        public DbSet<TagCategory> TagCategories { get; set; }
        public DbSet<TagCategoryMap> TagCategoryMap { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<PresentationQueueItem> PresentationQueueItems { get; set; }
        public DbSet<WebPage> WebPages { get; set; }
    }
}