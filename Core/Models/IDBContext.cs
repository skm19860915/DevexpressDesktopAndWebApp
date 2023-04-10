using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Models.UI;

namespace BlitzerCore.Models
{
    public interface IDbContext : IDisposable
    {
        DbSet<LogMsg> LogMsgs { get; set; }
        DbSet<Opportunity> Opportunities { get; set; }
        DbSet<Comparable> Comparables { get; set; }
        DbSet<WebSrvLogin> WebSrvLogins { get; set; }
        DbSet<Contact> AppUsers { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<RelationshipMap> RelationshipMaps { get; set; }
        DbSet<Company> Companies { get; set; }
        DbSet<MemberShip> MemberShips { get; set; }
        DbSet<Note> Notes { get; set; }
        DbSet<FOP> CreditCards { get; set; }
        DbSet<UserTracking> UserTrackings { get; set; }
        DbSet<Graphic> Graphics { get; set; }
        DbSet<Media> Medias { get; set; }
        DbSet<DataMap> DataMaps { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<Post> Posts { get; set; }
        DbSet<SubPage> SubPages { get; set; }
        DbSet<BlitzerCore.Models.UI.Gallary> GallaryPages { get; set; }
        DbSet<BlitzerCore.Models.UI.UIResortPage> ResortPages { get; set; }
        DbSet<BlitzerCore.Models.UI.UIRanking> RankingPages { get; set; }
        DbSet<BlitzerCore.Models.Ranking> ResortRankings { get; set; }
        DbSet<BlitzerCore.Models.PageToBlockMap> PageToBlockMap { get; set; }
        DbSet<BlitzerCore.Models.BlockToPageMap> BlockToPageMap { get; set; }
        DbSet<BlitzerCore.Models.UI.Block> Blocks { get; set; }
        DbSet<BlitzerCore.Models.UI.Page> Pages { get; set; }
        DbSet<BlitzerCore.Models.UI.UICountry> UICountries { get; set; }
        DbSet<Tile> Tiles { get; set; }
        DbSet<PageType> PageTypes { get; set; }
        DbSet<Activity> Activities { get; set; }
        DbSet<ActivityType> ActivityTypes { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Region> Regions { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<Video> Videos { get; set; }
        DbSet<Content> Contents { get; set; }
        DbSet<QuoteToResultsMapper> QuoteToResultsMappers { get; set; }
        DbSet<Filter> Filters { get; set; }
        DbSet<UserMap> UserMaps { get; set; }
        DbSet<ErrorMsg> ErrorMsgs { get; set; }
        DbSet<Amenity> Amenities { get; set; }
        DbSet<AmenityMap> AmenityMaps { get; set; }
        DbSet<AgentAirPortPreference> AgentAirPortPreferences { get; set; }
        DbSet<AIFilter> AIFilters { get; set; }
        DbSet<AIDefaultFilter> AIDefaultFilters { get; set; }
        DbSet<AIFilterMAP> AIFilterMaps { get; set; }
        DbSet<FilteredAccommodation> FilteredAccommodations { get; set; }
        DbSet<Stage> Stages { get; set; }
        DbSet<Client> Leads { get; set; }
        DbSet<Email> Emails { get; set; }
        DbSet<Quote> Quotes { get; set; }
        DbSet<Trip> Trips { get; set; }
        DbSet<Relationship> Relationships { get; set; }
        DbSet<AirPort> AirPorts { get; set; }
        DbSet<QuoteRequest> QuoteRequests { get; set; }
        DbSet<AccountStatus> GetAccountStatuses { get; set; }
        DbSet<Ad> Ads { get; set; }
        DbSet<Agent> Agents { get; set; }
        DbSet<EmailType> EmailTypes { get; set; }
        DbSet<Hotel> Accommodations { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<MerchantServices> MerchantServices { get; set; }
        DbSet<TripComponent> TripComponents { get; set; }
        DbSet<SavedAd> SavedAds { get; set; }
        DbSet<UserLocationPreference> UserLocationPreferences { get; set; }
        DbSet<Transportation> Transportations { get; set; }
        DbSet<UserLocation> UserLocations { get; set; }
        DbSet<Blob> Blobs { get; set; }
        DbSet<MarketingAd> MarketingAds { get; set; }
        DbSet<Merchant> Merchants { get; set; }
        DbSet<AirPort> Airports { get; set; }
        DbSet<Staging.Hotel> Staging_Hotels { get; set; }
        DbSet<Hotel> RequestedHotels { get; set; }
        DbSet<Staging.HotelRate> Staging_HotelRates { get; set; }
        DbSet<Staging.Flight> Staging_Flights { get; set; }
        DbSet<Flight> RequestedFlights { get; set; }
        DbSet<BlitzerCore.Models.QuoteRequestResortFilter> QuoteRequestResortFilter { get; set; }
        DbSet<BlitzerCore.Models.QuoteRequestResort> QuoteRequestResorts { get; set; }
        DbSet<BlitzerCore.Models.FlightItinerary> FlightItineraries { get; set; }
        DbSet<BlitzerCore.Models.SKU> SKUs { get; set; }
        DbSet<BlitzerCore.Models.TourOperator> TourOperators { get; set; }
        DbSet<Franchise> Franchises { get; set; }
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        int SaveChanges();
        // Version 3.1 12/30/2020
        DbSet<ContactType> ContactTypes { get; set; }
        DbSet<ContactSubType> ContactSubTypes { get; set; }
        // Version 3.2 1/1/2021
        DbSet<HouseHold> HouseHolds { get; set; }
        DbSet<QuoteGroup> QuoteGroups { get; set; }
        DbSet<DBVersion> DBVersions { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<Task> Tasks { get; set; }
        DbSet<BusinessType> BusinessTypes { get; set; }
        DbSet<TeamMember> TeamMembers { get; set; }
        DbSet<UserStory> UserStories { get; set; }
        DbSet<Sprint> Sprints { get; set; }
        DbSet<NewsFeed> NewsFeeds { get; set; }
        DbSet<Feature> Features { get; set; }
        DbSet<BlitzSystem> Systems { get; set; }
        DbSet<ReferralSource> Referrals { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        int ExecCommand(string aCmd);
        DbSet<Credit> Credits { get; set; }
        DbSet<Phone> PhoneNumbers { get; set; }
        DbSet<AgentProfile> AgentProfiles { get; set; }
        DbSet<QuoteSKUs> QuoteSKUs { get; set; }
        DbSet<RoomType> RoomTypes { get; set; }
        DbSet<Cruise> Cruises { get; set; }
        DbSet<Tour> Tours { get; set; }
        DbSet<ClientView> ClientViews { get; set; }
        DbSet<NameReplacement> NameReplacements { get; set; }
        DbSet<CruiseLine> CruiseLines { get; set; }
        DbSet<RegisterBooking> RegisterBookings { get; set; }
        // 11/8/2021
        DbSet<File> Files { get; set; }
        DbSet<FileType> FileTypes { get; set; }
        // 11/12/2021
        DbSet<Tag> Tags { get; set; }
        DbSet<TaggedObject> TaggedObjects { get; set; }
        DbSet<TagCategory> TagCategories { get; set; }
        DbSet<TagCategoryMap> TagCategoryMap { get; set; }
        DbSet<Presentation> Presentations { get; set; }
        DbSet<PresentationQueueItem> PresentationQueueItems { get; set; }
        DbSet<WebPage> WebPages { get; set; }
    }
}
