using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;

namespace BlitzerCore.DataAccess
{
    public class QuoteGroupDataAccess
    {
        const string ClassName = "QuoteGroupDataAccess::";
        IDbContext DbContext { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<QuoteGroup> Table { get; set; }

        public QuoteGroupDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.QuoteGroups;
        }

        public string ConvertIDToGUID (int aID)
        {
            var lQGroup = Table.Where(x => x.Id == aID).FirstOrDefault();
            if (lQGroup == null)
                return null;

            return lQGroup.GUID;
        }

        public QuoteGroup Get(int aID)
        {
            var lGUID = ConvertIDToGUID(aID);
            if (lGUID == null)
                return null;

            return Get(lGUID);
        }

        public QuoteGroup Get(string aGUID)
        {
            var lCnt = Table.Include(x1 => x1.BotQuotes).Where(x => x.GUID == aGUID).Count();

            if (lCnt == 1)
            {
                return Table
                       .Include(quote => quote.QuoteRequest)
                       .Include(quote => quote.QuoteRequest).ThenInclude(sub => sub.Agent).ThenInclude(x => x.Emails)
                       .Include(quote => quote.QuoteRequest).ThenInclude(subx => subx.Opportunity).ThenInclude(x => x.Travelers).ThenInclude(y => y.User)
                       .Include(quote => quote.QuoteRequest).ThenInclude(sub => sub.DestinationAirPort)
                       .Include(quote => quote.QuoteRequest).ThenInclude(sub => sub.DepartureAirPort)
                       .Include(x1 => x1.Quotes).ThenInclude(sub => sub.AccommodationRoomType)
                       .Include(x1 => x1.Quotes).ThenInclude(sub => sub.Accommodation).ThenInclude(sub1 => sub1.Page)
                       .Include(x1 => x1.Quotes).ThenInclude(sub => sub.TourOperator)
                       .Include(x1 => x1.Flights).ThenInclude(x3 => x3.InBound).ThenInclude(x4 => x4.Flights).ThenInclude(x5 => x5.ArrivalAirPort)
                       .Include(x1 => x1.Flights).ThenInclude(x3 => x3.InBound).ThenInclude(x4 => x4.Flights).ThenInclude(x5 => x5.DepartAirPort)
                       .Include(x1 => x1.Flights).ThenInclude(x3 => x3.OutBound).ThenInclude(x4 => x4.Flights).ThenInclude(x5 => x5.ArrivalAirPort)
                       .Include(x1 => x1.Flights).ThenInclude(x3 => x3.OutBound).ThenInclude(x4 => x4.Flights).ThenInclude(x5 => x5.DepartAirPort)
                    .Include(quote => quote.QuoteRequest.Agent).ThenInclude(sub => sub.Emails)
                    .Include(quote => quote.QuoteRequest.Agent).ThenInclude(sub => sub.PhoneNumbers)
                    .Include(x1=>x1.SelectedQuoteRequestTicket)
                    .FirstOrDefault(x => x.GUID == aGUID);
            }
            else
            {

                return Table
                       .Include(quote => quote.QuoteRequest)
                       .Include(quote => quote.QuoteRequest).ThenInclude(sub => sub.Agent).ThenInclude(x => x.Emails)
                       .Include(quote => quote.QuoteRequest).ThenInclude(subx => subx.Opportunity).ThenInclude(x => x.Travelers).ThenInclude(y => y.User)
                       .Include(quote => quote.QuoteRequest).ThenInclude(sub => sub.DestinationAirPort)
                       .Include(quote => quote.QuoteRequest).ThenInclude(sub => sub.DepartureAirPort)
                       .Include(x1 => x1.Quotes).ThenInclude(sub => sub.AccommodationRoomType)
                       .Include(x1 => x1.Quotes).ThenInclude(sub => sub.Accommodation).ThenInclude(sub1 => sub1.Page)
                       .Include(x1 => x1.Quotes).ThenInclude(sub => sub.TourOperator)
                       .Include(x1 => x1.Flights).ThenInclude(sub => sub.InBound)
                       .Include(x1 => x1.Flights).ThenInclude(sub => sub.OutBound)
                       .Include(x1 => x1.BotQuotes).ThenInclude(x2 => x2.QuoteRequestResort).ThenInclude(x3 => x3.ResortRoomType)
                       .Include(x1 => x1.BotQuotes).ThenInclude(x2 => x2.QuoteRequestResort).ThenInclude(x3 => x3.Resort)
                       .Include(x1 => x1.BotQuotes).ThenInclude(x2 => x2.QuoteRequestResort).ThenInclude(x3 => x3.TourOperator)
                       .Include(x1 => x1.Flights).ThenInclude(x3 => x3.InBound).ThenInclude(x4 => x4.Flights).ThenInclude(x5 => x5.ArrivalAirPort)
                       .Include(x1 => x1.Flights).ThenInclude(x3 => x3.InBound).ThenInclude(x4 => x4.Flights).ThenInclude(x5 => x5.DepartAirPort)
                       .Include(x1 => x1.Flights).ThenInclude(x3 => x3.OutBound).ThenInclude(x4 => x4.Flights).ThenInclude(x5 => x5.ArrivalAirPort)
                       .Include(x1 => x1.Flights).ThenInclude(x3 => x3.OutBound).ThenInclude(x4 => x4.Flights).ThenInclude(x5 => x5.DepartAirPort)
                    .Include(quote => quote.QuoteRequest.Agent).ThenInclude(sub => sub.Emails)
                    .Include(quote => quote.QuoteRequest.Agent).ThenInclude(sub => sub.PhoneNumbers)
                    .FirstOrDefault(x => x.GUID == aGUID);

            }
        }

        public List<int> GetUniqueHotels(QuoteGroup aQuoteGroup)
        {
            var lHotels = DbContext.QuoteRequestResorts.Where(x => x.QuoteGroupId == aQuoteGroup.Id).Select(x => x.ResortId);
            if (lHotels == null)
                return new List<int>();

            return lHotels.Distinct().ToList();
        }

        public IEnumerable<SKU> GetSKUs(QuoteGroup aQuoteGroup)
        {
            var lSKUIds = DbContext.QuoteRequestResorts.Where(x=>x.QuoteGroupId == aQuoteGroup.Id).Select(x => x.ResortRoomTypeID);
            return DbContext.SKUs
                .Include(x=>x.Provider)
                .Where(x => lSKUIds.Contains(x.SKUID));
        }

        public List<QuoteGroup> GetAll()
        {
            return Table
                .ToList();
        }

        public bool Exists(string aGUID)
        {
            return Table.Where(x => x.GUID == aGUID).Count() > 0;
        }


        public bool Exists(int id, string tck)
        {
            return Table.Where(x => x.Id == id && x.GUID == tck).Count() > 0;
        }

        public int Save(QuoteGroup aGroup, bool aCommit = true)
        {
            string FuncName = ClassName + "Save -";

            if (aGroup.Id > 0)
                Table.Update(aGroup);
            else
                Table.Add(aGroup);

            if (aCommit == false)
                return 0;

            try
            {
                var lCnt = DbContext.SaveChanges();
                Logger.LogInfo(FuncName + $" Updated {lCnt} quote group records");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $" Failed to save [{aGroup.Id}]", e);
            }

            return 0;
        }

        public IEnumerable<QuoteGroup> Get(QuoteRequest aQR, QuoteGroupFilter aFilter)
        {
            if (aFilter == QuoteGroupFilter.Active)
                return Table.Where(x => x.QuoteRequestID == aQR.QuoteRequestID && x.Status == QuoteGroupStatus.Open);
            else if (aFilter == QuoteGroupFilter.All)
                return Table.Where(x => x.QuoteRequestID == aQR.QuoteRequestID && x.Status != QuoteGroupStatus.Sent);

            return Table.Where(x => x.QuoteRequestID == aQR.QuoteRequestID && x.Status != QuoteGroupStatus.Deleted);
        }

        public void DeleteData(QuoteGroup aQuoteGroup)
        {
            int lQuoteGroupId = aQuoteGroup.Id;
            string FuncName = ClassName + $"DeleteData (id = {lQuoteGroupId})";

            try
            {
                string CWD = System.IO.Directory.GetCurrentDirectory();

                if (CWD.Contains("NUnitTests"))
                {
                    DbContext.QuoteToResultsMappers.RemoveRange(DbContext.QuoteToResultsMappers);
                    DbContext.FlightItineraries.RemoveRange(DbContext.FlightItineraries);
                    DbContext.QuoteRequestResorts.RemoveRange(DbContext.QuoteRequestResorts);
                    DbContext.Transportations.RemoveRange(DbContext.Transportations);
                    DbContext.FlightItineraries.RemoveRange(DbContext.FlightItineraries);
                }
                else
                {
                    DbContext.ExecCommand($"EXECUTE dbo.DeleteQuoteGroup {lQuoteGroupId}");
                }
                Logger.LogInfo($"{FuncName} - Successfully called Executed");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} - Failed to call StoredProc", e);
            }
        }

        internal void DeletePreviews(QuoteRequest aQuoteRequest)
        {
            string FuncName = ClassName + $"DeletePreviews (QuoteRequest ={aQuoteRequest.QuoteRequestID})";

            foreach (var lGroup in Get(aQuoteRequest, QuoteGroupFilter.All).Where(x => x.Status == QuoteGroupStatus.Open).ToList())
                Table.Remove(lGroup);

            try
            {
                var lCnt = DbContext.SaveChanges();
                Logger.LogInfo(FuncName + $" Deleted {lCnt} quote group records");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $" Failed to QuoteGroup previews", e);
            }
        }

        public Filter GetFilter(QuoteGroup aQuoteGroup)
        {
            return DbContext.Filters
                .Include(x=>x.Accommodations)
                .Where(x => x.QuoteGroupID == aQuoteGroup.Id).FirstOrDefault();
        }

        public void DeleteMappings(QuoteGroup aQuoteGroup)
        {
            string FuncName = ClassName + $"DeleteMappings (QuoteGroup ={aQuoteGroup.Id})";
            try
            {
                DbContext.QuoteToResultsMappers.RemoveRange(DbContext.QuoteToResultsMappers.Where(x => x.QuoteGroupID == aQuoteGroup.Id));
                var lCnt = DbContext.SaveChanges();
                Logger.LogInfo(FuncName + $" Deleted {lCnt} QuoteToResults Mappings");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $" Failed to delete mappings", e);
            }
        }
    }
}
