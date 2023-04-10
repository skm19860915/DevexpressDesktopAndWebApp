using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Helpers;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using System.IO;

namespace BlitzerCore.Business
{
    public class TripBusiness
    {
        const string ClassName = "TripBusiness::";
        const int FP_WARNING = 10;
        const int FP_CRITICAL = 5;
        const int TRANSFER_SUPPLIER_TYPEID = 13;
        IDbContext DbContext { get; set; }
        IConfiguration Configuration { get; }

        public TripBusiness(IDbContext aContext, IConfiguration aConfig = null)
        {
            DbContext = aContext;
            Configuration = aConfig;
        }

        public Trip Get(int aTripID)
        {
            var lOutput = new TripDataAccess(DbContext).Get(aTripID);
            if (lOutput != null)
                lOutput.Balance = Balance(lOutput);
            return lOutput;
        }

        public void RegisterBookings(Trip lTrip)
        {
            const string REGISTER_BOOKING = "Register Booking";
            // Does the Trip already of a print Docs Task
            if (lTrip.Tasks == null || lTrip.Tasks.Any(x => x.Name == REGISTER_BOOKING) == false)
            {
                if (lTrip.StartDate.AddDays(-31) < DateTime.Now)
                {
                    var lAgent = lTrip.Agent;
                    var lAgents = new AgentDataAccess(DbContext).GetAll(lTrip.Agent.EmployerId ?? 0);
                    if (lAgents.Any())
                        lAgent = lAgents.FirstOrDefault(x => x.Role == Roles.Assistant);
                    var lSupplierIds = lTrip.Bookings.Select(x => x.SupplierId);
                    foreach (var lSupplierId in lSupplierIds)
                    {
                        var lRegBookings = new BlitzerDataAccess(DbContext).GetRegisterBookings();
                        var lRegBooking = lRegBookings.FirstOrDefault(x => x.SupplyId == lSupplierId);
                        if (lRegBooking == null)
                            continue;
                        var lDescription = lRegBooking.Directions;
                        new TaskBusiness(DbContext).Create(lTrip, REGISTER_BOOKING, lDescription, lTrip.StartDate.AddDays(-14), lAgent);
                    }
                }
            }
        }

        public void CreatePrintDocsTask(Trip lTrip)
        {
            const string PRINT_TRAVEL_DOCS = "Print Travel Docs";
            // Does the Trip already of a print Docs Task
            if (lTrip.Tasks == null || lTrip.Tasks.Any(x => x.Name == PRINT_TRAVEL_DOCS) == false)
            {
                if (lTrip.StartDate.AddDays(-14) < DateTime.Now && lTrip.StartDate < DateTime.Now)
                {
                    var lAgent = lTrip.Agent;
                    var lAgents = new AgentDataAccess(DbContext).GetAll(lTrip.Agent.EmployerId ?? 0);
                    if (lAgents.Any())
                        lAgent = lAgents.FirstOrDefault(x => x.Role == Roles.Assistant);
                    new TaskBusiness(DbContext).Create(lTrip, PRINT_TRAVEL_DOCS, lTrip.StartDate.AddDays(-7), lAgent);
                }
            }
        }

        public static double Balance(Trip aTrip)
        {
            double lOutput = 0;
            if (aTrip == null)
                return lOutput;

            foreach (var lBooking in TripBusiness.ValidBookings(aTrip))
                lOutput += BookingBusiness.Balance(lBooking);

            return lOutput;
        }

        public ICollection<DaySummary> GetSummary(int aTripID)
        {
            Uri lTakeOff = new Uri("https://blitzerblobs.blob.core.windows.net/icons/FlightDepart.jpg");
            Uri lLanding = new Uri("https://blitzerblobs.blob.core.windows.net/icons/FlightLanding.jpg");
            Uri lTakeOff2 = new Uri("https://blitzerblobs.blob.core.windows.net/icons/FlightDepart.Tif");
            Uri lLanding2 = new Uri("https://blitzerblobs.blob.core.windows.net/icons/FlightLanding.Tif");
            Uri lHotel = new Uri("https://blitzerblobs.blob.core.windows.net/icons/Bed.tif");



            TripSummary lSummary = new TripSummary();
            List<DaySummary> lResults = new List<DaySummary>();
            lSummary.DayEvents = lResults;

            DaySummary lDay = new DaySummary();
            lDay.Location = "Raleigh, NC";
            lDay.Date = new DateTime(2020, 6, 10);
            lDay.EventTypes = new List<Uri>() { lTakeOff, lLanding, lHotel };
            lResults.Add(lDay);

            lDay = new DaySummary();
            lDay.Location = "Cancun, Mexico";
            lDay.Date = new DateTime(2020, 6, 11);
            lDay.EventTypes = new List<Uri>() { lHotel };
            lResults.Add(lDay);

            lDay = new DaySummary();
            lDay.Location = "Cancun, Mexico";
            lDay.Date = new DateTime(2020, 6, 12);
            lDay.EventTypes = new List<Uri>() { lHotel };
            lResults.Add(lDay);

            lDay = new DaySummary();
            lDay.Location = "Cancun, Mexico";
            lDay.Date = new DateTime(2020, 6, 13);
            lDay.EventTypes = new List<Uri>() { lHotel };
            lResults.Add(lDay);

            lDay = new DaySummary();
            lDay.Location = "Raleigh, NC";
            lDay.Date = new DateTime(2020, 6, 14);
            lDay.EventTypes = new List<Uri>() { lTakeOff2, lLanding2 };
            lResults.Add(lDay);

            return lResults;
        }

        public static Models.UI.FinalPaymentStatus FinalPaymentStatus(Trip aTrip)
        {
            foreach (var lBooking in aTrip.Bookings)
            {
                if (lBooking.FinalPayment == null)
                    continue;

                var lDaysLeft = lBooking.FinalPayment.Value.Subtract(DateTime.Now).TotalDays;
                if (Math.Abs(BookingBusiness.Balance(lBooking)) > .01 && lDaysLeft < FP_CRITICAL)
                    return Models.UI.FinalPaymentStatus.Critical;
                if (Math.Abs(BookingBusiness.Balance(lBooking)) > .01 && lDaysLeft < FP_WARNING)
                    return Models.UI.FinalPaymentStatus.Warning;
            }

            return Models.UI.FinalPaymentStatus.Good;
        }

        public static double GrossCommission(Trip aTrip)
        {
            return ValidBookings(aTrip)
                .Sum(x => x.GrossCommission);
        }

        private static IEnumerable<Booking> ValidBookings(Trip aTrip)
        {
            return aTrip.Bookings
                .Where(x => x.Status != BookingStatus.Cancelled && x.Status != BookingStatus.PendingCancellation && x.Status != BookingStatus.Deleted);
        }

        public static double ICCommission(Trip aTrip)
        {
            return ValidBookings(aTrip)
                .Sum(x => x.ICCommission);
        }

        public static double Total(Trip aTrip)
        {
            return ValidBookings(aTrip).Sum(x => x.Amount);
        }

        public void RemoveContact(int aTripId, string aContactId)
        {
            string FuncName = ClassName + $"RemoveContact (TripId={aTripId}, ContactId={aContactId})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lTrip = new TripDataAccess(DbContext).Get(aTripId);
                var lContact = new ContactDataAccess(DbContext).Get(aContactId);
                var lUserMap = lTrip.Travelers;
                if (lTrip.Travelers != null)
                {
                    var lTargetMap = lTrip.Travelers.FirstOrDefault(x => x.OpportunityID == aTripId && x.UserID == aContactId);
                    if (lTargetMap != null)
                        Logger.LogInfo("Found UserMap matching TripID and UserID");
                    lTrip.Travelers.Remove(lTargetMap);
                    Save(lTrip);
                }
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to AddContact", e);

            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        public void AddContact(int aTripId, string aContactId)
        {
            string FuncName = ClassName + $"AddContact (TripId={aTripId}, ContactId={aContactId})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lTrip = new TripDataAccess(DbContext).Get(aTripId);
                var lContact = new ContactDataAccess(DbContext).Get(aContactId);
                var lUserMap = lTrip.Travelers;
                if (lTrip.Travelers == null) lTrip.Travelers = new List<UserMap>();
                lTrip.Travelers.Add(new UserMap() { OpportunityID = aTripId, UserID = aContactId });
                Save(lTrip);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to AddContact", e);

            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        public object GetKanbanTasks(Agent lAgent)
        {
            throw new NotImplementedException();
        }

        public List<Trip> Get(Contact aUser, Opportunity.OpportunityStatus aStatus = Opportunity.OpportunityStatus.Active)
        {
            var lAllTrips = new TripDataAccess(DbContext).Get(aUser);
            var lTrips = lAllTrips.Where(x => x.Status == aStatus)
                .ToList();
            foreach (var lTrip in lTrips)
                lTrip.Balance = Balance(lTrip);
            return lTrips;
        }

        public List<Trip> Get(Contact aUser, Trip.Statuses aStatus)
        {
            var lAllTrips = new TripDataAccess(DbContext).Get(aUser);
            var lTrips = lAllTrips.Where(x => x.TripStatus == aStatus)
                .ToList();
            foreach (var lTrip in lTrips)
                lTrip.Balance = Balance(lTrip);
            return lTrips;
        }

        public List<Trip> Get(Agent aAgent)
        {
            return new TripDataAccess(DbContext).Get(aAgent);
        }

        public List<Trip> GetActiveTrips(Contact aContact)
        {
            return new TripDataAccess(DbContext).GetActiveTrips(aContact);
        }

        public List<Trip> GetActiveTrips(Agent aAgent)
        {
            return new TripDataAccess(DbContext).GetActiveTrips(aAgent);
        }

        public List<Trip> GetActiveTrips()
        {
            return new TripDataAccess(DbContext).GetActiveTrips();
        }


        public int SaveNote(Note aNote, Trip aTrip)
        {
            var lName = new OpportunityBusiness(DbContext).GetDefaultName(aTrip);
            var lOldTrip = Get(aTrip.ID);

            return new NoteBusiness(DbContext).Save(aNote, aTrip);
        }
        public List<Trip> GetAll(Agent aAgent)
        {
            return new TripDataAccess(DbContext).GetAll(aAgent);
        }

        public ICollection<TripComponent> GetDetail(int aTripID, string aWhen)
        {
            throw new NotImplementedException();

        }
        public Opportunity Book(UIQuoteRequestEdit aQuote, Contact aUser)
        {
            string FuncName = ClassName + $"Book (UIQuoteRequestEdit = {aQuote.Id})";
            Logger.EnterFunction(FuncName);
            Opportunity lTrip = null;
            if (aUser == null)
            {
                Logger.LogInfo("Didn't book trip because user was null");
                return null;
            }

            try
            {
                var lOppID = new QuoteDataAccess(DbContext).GetOpportunityIdFromQuote(aQuote.Id * -1);
                lTrip = ConvertToTrip(lOppID);

                if (aQuote.Id < 0)
                {
                    var lMap = new QuoteDataAccess(DbContext).GetMapper(aQuote.Id * -1);
                    var lQuote = BookQuote(lMap, aUser, lOppID);
                    SendBookingNotice(lMap);
                }
                else
                {
                    var lQuote = new QuoteBusiness(DbContext).Get(aQuote.Id);
                    BookQuote(lQuote, aUser, lTrip.ID);
                    SendBookingNotice(lQuote);
                }

                return lTrip;
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " - Failed to book quote", e);
                throw e;
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        private void CreateAssignSeatsTask(Opportunity aOpp)
        {
            new TaskBusiness(DbContext).Create(aOpp, "Assign Seats", null);
        }

        public static double Payments(Trip aTrip) =>  aTrip.Bookings.Sum(x => BookingBusiness.Payments(x));
        
        private void SendBookingNotice(Quote aQuote)
        {
            var lOppName = aQuote.QuoteRequest.Opportunity.Name;
            var lResort = new CompanyBusiness(DbContext).Get(aQuote.SupplierId.Value).Name;
            var lRoom = aQuote.AccommodationRoomType?.Name;
            var lTo = new List<string>() { aQuote.QuoteRequest.Agent.PrimaryEmail };
            var lCC = new List<string>() { "Info@eze2travel.com" };
            var lSubject = "ACTION REQUIRED : Need to BOOK -> " + lOppName;
            var lMsg = "Hello " + aQuote.QuoteRequest.Agent.First + ",<br><br>";
            lMsg += "Closed the " + lOppName + " opportunity, they just booked at the " + lResort + " for " + DataHelper.ConvertToCurrency(aQuote.PackagePrice);
            new BlitzerCore.Helpers.CoreEmailHelper(Configuration).SendEmail(lTo, lCC, lSubject, lMsg);
        }

        public static bool IsIssuer(Models.Task aTask, Contact aContact) =>  aTask.IssuerID == aContact.Id;

        private void SendBookingNotice(QuoteToResultsMapper aMapper)
        {
            var lOppName = aMapper.QuoteGroup.QuoteRequest.Opportunity.Name;
            var lResort = aMapper.QuoteRequestResort.Resort?.Name;
            var lRoom = aMapper.QuoteRequestResort.ResortRoomType?.Name;
            var lTo = new List<string>() { aMapper.QuoteGroup.QuoteRequest.Agent.PrimaryEmail };
            var lCC = new List<string>() { "Info@eze2travel.com" };
            var lSubject = "ACTION REQUIRED : Need to BOOK -> " + lOppName;
            var lMsg = "Hello " + aMapper.QuoteGroup.QuoteRequest.Agent.First + ",<br><br>";
            lMsg += lRoom + " just booked at the " + lResort + " for " + DataHelper.ConvertToCurrency(aMapper.QuoteRequestResort.Price);
            new BlitzerCore.Helpers.CoreEmailHelper(Configuration).SendEmail(lTo, lCC, lSubject, lMsg);
        }

        private bool CreateBotBooking(Quote aQuote)
        {
            if (aQuote.TourOperatorID == null)
                return false;

            var lTourOperator = aQuote.TourOperator ?? new TourOperatorDataAccess(DbContext).Get(aQuote.TourOperatorID.Value);
            var lWebBot = new BlitzerBusiness(DbContext, Configuration).GetWebService(aQuote.TourOperator);
            if (lWebBot != null)
                return lWebBot.CreateBooking(aQuote);

            return false;
        }

        /// <summary>
        /// Called when a user decides which Airline Tickets and Hotel to Book
        /// </summary>
        public Opportunity Book(UIQuote aQuote, Contact aCurrentUser)
        {
            string FuncName = ClassName + $"Book (UIQuote = {aQuote.QuoteID})";

            if (aQuote.QuoteID < 1)
                return null;

            Logger.EnterFunction(FuncName);
            Opportunity lTrip = null;
            try
            {
                var lOppID = new QuoteRequestDataAccess(DbContext).GetIdFromQuoteRequestId(aQuote.QuoteRequestID);
                lTrip = ConvertToTrip(lOppID);
                var lQuote = BookQuote(aQuote, aCurrentUser, lOppID);

                return lTrip;

            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " - Failed to book quote", e);
                throw e;
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        internal void Update(int lTripID)
        {
            var lTrip = Get(lTripID);
            Save(lTrip);
        }


        private Quote BookQuote(QuoteToResultsMapper aMap, Contact aUser, int aTripId)
        {
            var lQBiz = new QuoteBusiness(DbContext);
            var lQuote = lQBiz.Create(aMap, aUser);
            lQBiz.Save(lQuote);
            AddTripBookings(aTripId, lQuote.BookedBy, lQuote);
            return lQuote;
        }
        private Quote BookQuote(UIQuote aQuote, Contact aUser, int aTripId)
        {
            var lQBiz = new QuoteBusiness(DbContext);
            var lQuote = lQBiz.Get(aQuote.QuoteID);
            CreateBotBooking(lQuote);
            return BookQuote(lQuote, aUser, aTripId);
        }

        private Quote BookQuote(Quote aQuote, Contact aUser, int aTripId)
        {
            aQuote.Status = QuoteStatus.Booked;
            aQuote.BookedBy = new ContactBusiness(DbContext).Get(aUser.Id);
            aQuote.BookedOn = DateTime.Now;
            aQuote.BookedById = aUser.Id;
            aQuote.Booked = true;
            new QuoteBusiness(DbContext).Save(aQuote);
            AddTripBookings(aTripId, aQuote.BookedBy, aQuote);
            return aQuote;
        }

        Booking CreateBooking(Quote aQuote, Contact aUser, int aTripId)
        {
            var lBooking = new Booking()
            {
                TourOperatorID = aQuote.TourOperatorID,
                SupplierId = aQuote.SupplierId,
                Amount = aQuote.Total,
                BookingNumber = "TBD",
                FinalPayment = new DateTime(1999, 12, 31),
                TripID = aTripId
            };
            BookingBusiness.UpdateTracking(lBooking, aUser);
            return lBooking;
        }

        private Trip.Statuses GetStatus(Trip aTrip)
        {
            if (aTrip.Bookings != null && aTrip.Bookings.Count() > 0 && aTrip.Bookings.All(x => x.Status == BookingStatus.Cancelled))
                return Trip.Statuses.Cancelled;

            if (aTrip.TripStage == TripStage.Cancelled)
                return Trip.Statuses.Cancelled;

            if (aTrip.TripStatus == Trip.Statuses.Deleted)
                return aTrip.TripStatus;

            if (aTrip.TripStage == TripStage.Traveled)
                return Trip.Statuses.Completed;

            return Trip.Statuses.Active;
        }

        public TripStage GetStage(Trip aTrip)
        {
            if (aTrip.Bookings != null && aTrip.Bookings.All(x => x.Status == BookingStatus.Cancelled))
                return TripStage.Cancelled;

            // If trip invlaid, it had to be completed
            if (aTrip.EndDate < DateTime.Now && aTrip.TripStatus != Trip.Statuses.Cancelled)
                return TripStage.Traveled;

            if (PassesProfileCheck(aTrip) == false)
                return TripStage.CompleteProfile;
            else if (PassesBalanceCheck(aTrip) == false)
                return TripStage.BalanceOutstanding;
            else if (OpenTransfer(aTrip))
                return TripStage.BookTransfer;
            else if (DocumentsPrinted(aTrip) == false)
                return TripStage.SendDocuments;
            else if (OpenTasks(aTrip) == true)
                return TripStage.OpenTasks;
            else if (PassesRTTCheck(aTrip) == false)
                return TripStage.ReadyForTravel;

            return TripStage.Traveled;
        }

        private bool OpenTasks(Trip aTrip)
        {
            return aTrip.Tasks.Any(x => x.Status == TaskStatusTypes.INPROGRESS
            || x.Status == TaskStatusTypes.NEW
            || x.Status == TaskStatusTypes.ONHOLD
            || x.Status == TaskStatusTypes.REVIEW);
        }

        private bool DocumentsPrinted(Trip aTrip)
        {
            return aTrip.DocumentsPrintedOn != null;
        }

        private bool OpenTransfer(Trip aTrip)
        {
            return aTrip.Bookings.Any(x => x.Supplier != null && x.Supplier.BusinessTypeID == TRANSFER_SUPPLIER_TYPEID && x.BookingNumber == "TBD");
        }

        public static bool HasTransfer(Trip aTrip) => aTrip.Bookings.Any(x => x.Supplier != null && x.Supplier.BusinessTypeID == TRANSFER_SUPPLIER_TYPEID);

        private bool PassesBalanceCheck(Trip aTrip) => (aTrip.Balance < 1);
        private bool PassesRTTCheck(Trip aTrip) => (aTrip.EndDate < DateTime.Now);

        private bool PassesProfileCheck(Trip aTrip)
        {
            if (aTrip.Travelers == null || aTrip.Travelers.Any() == false)
                return false;

            foreach (var lContact in aTrip.Travelers)
            {
                if (lContact.UserID != null && lContact.User == null)
                    return false;

                if (new ContactBusiness(DbContext).PassesProfileCheck(lContact.User) == false)
                    return false;
            }

            return true;
        }

        private void AddTripBookings(int aTripId, Contact aUser, Quote aQuote)
        {
            var lBooking = CreateBooking(aQuote, aUser, aTripId);
            new BookingBusiness(DbContext, Configuration).Save(lBooking, aUser);
        }

        private void ValidateBooking(UIQuoteGroup aQuote, string aCurrentUserID)
        {
            var lCount = aQuote.Quotes.SelectMany(x => x.Value).Count(x => x.Booked == true);
            if (lCount == 0)
                throw new InvalidDataException("You Must select a quote to book");
            if (lCount > 1)
                throw new InvalidDataException("You can only selecta single quote to book");
        }

        /// <summary>
        /// We are changing the Object type in the Db directly from an Opp to a Trip
        /// and have to be very careful because we need this transaction to end before 
        /// we can deal with the trip
        /// </summary>
        /// <param name="aOpportunity"></param>
        /// <param name="aCommit"></param>
        public Opportunity ConvertToTrip(int aOppId, bool aCommit = true)
        {
            // New to place before Convert so that it doesn't create another Opporutnit in Unit Test
            ShortTermWordAround(new OpportunityDataAccess(DbContext).Get(aOppId));

            // This call a Stored Proc to chane the Opp to a Trip Type, 
            // , but we in the same DB context so the DB doesn't see the change behind the scenes
            //  BRUTE Force
            var lShouldBeTripButOpportunity = new TripDataAccess(DbContext).ConvertToTrip(aOppId);

            return lShouldBeTripButOpportunity;
        }

        private void ShortTermWordAround(Opportunity aOpportuity)
        {
            aOpportuity.Stage = OpportunityStages.Won;
            aOpportuity.Status = Opportunity.OpportunityStatus.Inactive;
            aOpportuity.OppClosedDate = DateTime.Now;

            new OpportunityBusiness(DbContext).CloseCreateQuoteTask(aOpportuity);
            CreateAssignSeatsTask(aOpportuity);
        }

        public int Save(Opportunity aOpp, bool aCommit = true)
        {
            return Save(aOpp as Trip, aCommit);
        }


        public int Save(Trip aTrip, bool aCommit = true)
        {
            if (aTrip == null)
            {
                Logger.LogWarning("Attempted to save trip which was null");
                return 0;
            }

            var lTripDA = new TripDataAccess(DbContext);

            // House keeping to because somethign is changes the status on opps
            aTrip.Status = Opportunity.OpportunityStatus.Inactive;
            var lCnt = lTripDA.Save(aTrip, aCommit);

            // Trip can be passed in without any bookings or users
            var lTrip = lTripDA.Get(aTrip.ID);
            lTrip.Balance = TripBusiness.Balance(lTrip);
            lTrip.Name = new OpportunityBusiness(DbContext).GetDefaultName(lTrip);
            lTrip.TripStage = GetStage(lTrip);
            lTrip.TripStatus = GetStatus(lTrip);
            lTripDA.Save(lTrip, aCommit);

            return lCnt;
        }

        public QuoteRequestResort Convert(Staging.Hotel aStaging)
        {
            var lHotel = new QuoteRequestResort();
            //lHotel.Name = aStaging.Name;
            //lHotel.Type = Accommodation.Types.HOTEL;
            return lHotel;
        }

        public Flight Convert(Staging.Flight aFlight)
        {

            Flight lFlight = new Flight();
            string lData = aFlight.DepartDate + " " + aFlight.DepartTime;
            lFlight.Depart = DateTime.Parse(lData);
            lData = aFlight.DepartDate + " " + aFlight.ArrivalTime;
            lFlight.Arrive = DateTime.Parse(lData);
            AirPortDataAccess lAirDA = new AirPortDataAccess(DbContext);

            int lStart = aFlight.DepartLocation.IndexOf("(");
            lFlight.DepartAirPort = lAirDA.Get(aFlight.DepartLocation.Substring(lStart + 1, 3));
            if (lFlight.DepartAirPort == null)
                Logger.LogError("Depature Airport can't be null");
            lStart = aFlight.ArrivalLocation.IndexOf("(");
            lFlight.ArrivalAirPort = lAirDA.Get(aFlight.ArrivalLocation.Substring(lStart + 1, 3));
            if (lFlight.ArrivalAirPort == null)
                Logger.LogError("Arrival Airport can't be null");
            lFlight.Type = Transportation.Types.AIRLINE;
            lFlight.Carrier = aFlight.Carrier;
            lFlight.LegGUID = aFlight.LegGUID;
            lFlight.TourOperatorId = aFlight.TourOperatorID;
            return lFlight;
        }
    }
}
