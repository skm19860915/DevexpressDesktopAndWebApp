using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Models;
using BlitzerCore.Utilities;

namespace BlitzerCore.DataAccess
{
    public class QuoteRequestDataAccess
    {
        const string ClassName = "QuoteRequestDataAccess::";
        IDbContext mContext = null;

        public QuoteRequestDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public QuoteRequest Get(int aID, Agent aAgent = null)
        {
            if (aAgent == null)
                return mContext.QuoteRequests
                   .Include(quote => quote.Agent).ThenInclude(sub => sub.Emails)
                   .Include(quote => quote.Agent).ThenInclude(sub => sub.PhoneNumbers)
                   .Include(quote => quote.Opportunity).ThenInclude(sub => sub.Travelers).ThenInclude(sub1 => sub1.User.Emails)
                   .Include(quote => quote.Opportunity).ThenInclude(sub => sub.Travelers).ThenInclude(sub1 => sub1.User.PhoneNumbers)
                   .Include(quote => quote.QuoteGroups).ThenInclude(sub=>sub.SelectedQuoteRequestTicket)
                   .Include(quote => quote.QuoteGroups).ThenInclude(x1=>x1.Quotes).ThenInclude(sub => sub.AccommodationRoomType)
                   .Include(quote => quote.QuoteGroups).ThenInclude(x1 => x1.Quotes).ThenInclude(sub => sub.Accommodation)
                   .Include(quote => quote.QuoteGroups).ThenInclude(x1 => x1.BotQuotes).ThenInclude(x2 => x2.QuoteRequestResort).ThenInclude(x3 => x3.ResortRoomType)
                   .Include(quote => quote.QuoteGroups).ThenInclude(x1 => x1.BotQuotes).ThenInclude(x2 => x2.QuoteRequestResort).ThenInclude(x3 => x3.Resort)
                   .Include(quote => quote.QuoteGroups).ThenInclude(x1 => x1.BotQuotes).ThenInclude(x2 => x2.QuoteRequestResort).ThenInclude(x3 => x3.TourOperator)
                   .Include(quote => quote.DestinationAirPort)
                   .Include(quote => quote.DepartureAirPort)
                    .Where(x => x.QuoteRequestID == aID)
                    .FirstOrDefault();
            else
                return mContext.QuoteRequests
                   .Include(quote => quote.Agent).ThenInclude(sub => sub.Emails)
                   .Include(quote => quote.Agent).ThenInclude(sub => sub.PhoneNumbers)
                   .Include(quote => quote.Opportunity)
                   .Include(quote => quote.DestinationAirPort)
                   .Include(quote => quote.QuoteGroups).ThenInclude(x1 => x1.Quotes).ThenInclude(sub => sub.AccommodationRoomType)
                   .Include(quote => quote.QuoteGroups).ThenInclude(x1 => x1.Quotes).ThenInclude(sub => sub.Accommodation)
                   .Include(quote => quote.QuoteGroups).ThenInclude(x1 => x1.BotQuotes).ThenInclude(x2 => x2.QuoteRequestResort).ThenInclude(x3=>x3.ResortRoomType)
                   .Include(quote => quote.QuoteGroups).ThenInclude(x1 => x1.BotQuotes).ThenInclude(x2 => x2.QuoteRequestResort).ThenInclude(x3 => x3.Resort)
                   .Include(quote => quote.Opportunity).ThenInclude(sub => sub.Travelers).ThenInclude(sub1 => sub1.User.Emails)
                   .Include(quote => quote.Opportunity).ThenInclude(sub => sub.Travelers).ThenInclude(sub1 => sub1.User.PhoneNumbers)
                   .Include(quote => quote.QuoteGroups).ThenInclude(x1 => x1.BotQuotes).ThenInclude(x2 => x2.QuoteRequestResort).ThenInclude(x3 => x3.TourOperator)
                   .Include(quote => quote.DepartureAirPort)
                   .Where(x => x.QuoteRequestID == aID && x.AgentId == aAgent.Id)
                   .FirstOrDefault();


        }

        public List<QuoteRequestResort> GetAll ( QuoteGroup aQuoteGroup)
        {
            return mContext.QuoteRequestResorts.Where(x => x.QuoteRequestResortID == aQuoteGroup.Id).ToList();
        }

        public int Save(QuoteRequestResort aResort, bool aCommit = true)
        {
            string FuncName = ClassName + $"Save (QuoteRequestResort = {aResort.QuoteRequestResortID}, Commit = {aCommit}) - ";
            if (aResort.ResortId == 0)
                mContext.QuoteRequestResorts.Add(aResort);
            else
                mContext.QuoteRequestResorts.Update(aResort);

            try
            {
                if (aCommit == true)
                {
                    var lCnt = mContext.SaveChanges();
                    Logger.LogInfo(FuncName + $"Updated {lCnt} records");
                    return lCnt;
                }

                return 0;
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + "Failed to save QuoteRequestResort", e);
            }

            return 0;
        }

        public void ExludeQuotes ( List<int> aQuoteId )
        {

        }

        public int Save(QuoteRequest aRequest, bool aCommit = true)
        {
            string FuncName = ClassName + $"Save (QuoteRequestResort = {aRequest.QuoteRequestID}, Commit = {aCommit}) - ";
            string lAction = "Added";
            if (aRequest.QuoteRequestID == 0)
            {
                mContext.QuoteRequests.Add(aRequest);
            }
            else
            {
                mContext.QuoteRequests.Update(aRequest);
                lAction = "Updated";
            }

            try
            {
                if (aCommit == true)
                {
                    var lCnt = mContext.SaveChanges();
                    Logger.LogInfo(FuncName + $"{lAction} {lCnt} quote request records");
                    return lCnt;
                }

                return 0;
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + "Failed to save QuoteRequest", e);
            }

            return 0;
        }

        public int Save(Quote aResort, bool aCommit = true)
        {
            //string FuncName = ClassName + $"Save (QuoteRequestResort = {aResort.QuoteRequestResortID}, Commit = {aCommit}) - ";
            //if (aResort.AccommodationID == 0)
            //    mContext.QuoteRequestResorts.Add(aResort);
            //else
            //    mContext.QuoteRequestResorts.Update(aResort);

            //try
            //{
            //    if (aCommit == true)
            //    {
            //        var lCnt = mContext.SaveChanges();
            //        Logger.LogInfo(FuncName + $"Updated {lCnt} records");
            //        return lCnt;
            //    }

            //    return 0;
            //} catch ( Exception e )
            //{
            //    Logger.LogException(FuncName + "Failed to save QuoteRequestResort", e);
            //}

            return 0;
        }

        public List<QuoteRequestResort> GetAccommondationsByQRID(int aQuoteRequestID)
        {
            //return mContext.QuoteRequestResorts
            //    .Include(quote => quote.QuoteRequest)
            //    .Include(quote => quote.AccommodationRoomType)
            //    .Include(quote => quote.TourOperator)
            //    .Include(quote => quote.Accommodation)
            //    .Where(x => x.QuoteRequestID == aQuoteRequestID)
            //    .ToList();
            throw new NotImplementedException();
        }

        public List<FlightItinerary> GetTicketsByQRID(int aQuoteRequestID)
        {
            return mContext.FlightItineraries
                .Include(quote => quote.InBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.ArrivalAirPort)
                .Include(quote => quote.InBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.DepartAirPort)
                .Include(quote => quote.OutBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.ArrivalAirPort)
                .Include(quote => quote.OutBound).ThenInclude(Sub => Sub.Flights).ThenInclude(Flt => Flt.DepartAirPort)
                .Include(quote => quote.QuoteRequest)
                .Where(x => x.QuoteRequestID == aQuoteRequestID)
                .ToList();
        }

        public List<QuoteRequest> Get(Contact aUser)
        {
            var lOutput = mContext.QuoteRequests
               .SelectMany(x => x.Opportunity.Travelers).Where(x => x.User.Id == aUser.Id)
               .SelectMany(x => x.Opportunity.QuoteRequests)
               .Include(quote => quote.Agent).ThenInclude(sub => sub.Emails)
               .Include(quote => quote.Agent).ThenInclude(sub => sub.PhoneNumbers)
               .Include(quote => quote.Opportunity).ThenInclude(sub => sub.Travelers).ThenInclude(sub1 => sub1.User.Emails)
               .Include(quote => quote.DestinationAirPort)
               .Include(quote => quote.DepartureAirPort)
               .Include(quote => quote.QuoteGroups).ThenInclude(x1=>x1.Quotes).ThenInclude(sub => sub.AccommodationRoomType)
               .ToList();
            return lOutput;
        }

        public void DeleteStagingData(QuoteGroup aQuoteGroup)
        {
            int lQuoteGroupId = aQuoteGroup.Id;
            string FuncName = ClassName + $"DeleteStagingData (id = {lQuoteGroupId})";

            try
            {
                string CWD = System.IO.Directory.GetCurrentDirectory();

                if (CWD.Contains("NUnitTests"))
                {
                    mContext.Staging_Flights.RemoveRange(mContext.Staging_Flights);
                    mContext.Staging_Hotels.RemoveRange(mContext.Staging_Hotels);
                } else
                    mContext.ExecCommand($"EXECUTE dbo.DeleteQuoteRequestData {lQuoteGroupId}");
            } catch ( Exception e )
            {
                Logger.LogException($"{FuncName} - Failed to call StoredProc", e);
            }
        }

        public List<QuoteRequest> GetByOppId(int aOpportunityID)
        {
            var lOutput = mContext.QuoteRequests
               .Include(quote => quote.Agent).ThenInclude(sub => sub.Emails)
               .Include(quote => quote.Agent).ThenInclude(sub => sub.PhoneNumbers)
               .Include(quote => quote.Opportunity).ThenInclude(sub => sub.Travelers).ThenInclude(sub1 => sub1.User.Emails)
               .Include(quote => quote.DestinationAirPort)
               .Include(quote => quote.QuoteGroups).ThenInclude(x1=>x1.ClientViews)
               .Include(quote => quote.DepartureAirPort)
               .Include(quote => quote.QuoteGroups).ThenInclude(x1=>x1.ClientViews)
               .Include(quote => quote.QuoteGroups).ThenInclude(x1=>x1.Quotes).ThenInclude(sub => sub.AccommodationRoomType)
                .Where(x => x.OpportunityID == aOpportunityID)
                .ToList();
            return lOutput;
        }

        public List<string> GetLocations(QuoteGroup aQuoteGroup)
        {
            return (from QuoteResorts in mContext.QuoteRequestResorts
                    join Hotel in mContext.Accommodations on QuoteResorts.ResortId equals Hotel.Id
                    where QuoteResorts.QuoteGroupId == aQuoteGroup.Id
                    select Hotel.Area).Distinct().ToList();
        }

        public int GetIdFromQuoteRequestId(int aQRequestID)
        {
            return mContext.QuoteRequests.Where(x => x.QuoteRequestID == aQRequestID).Select(y => y.OpportunityID)
                .FirstOrDefault();
        }
    }
}
