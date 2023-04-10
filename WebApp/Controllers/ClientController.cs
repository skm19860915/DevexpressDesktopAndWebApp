using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Models.ASP;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using BlitzerCore.DataAccess;
using WebApp.AspNetHelper;
using WebApp.DataServices;

namespace WebApp.Controllers
{
    public class ClientController : BaseController
    {
        const string ClassName = "ClientController::";
        IConfiguration Configuration { get; }
        public ClientController(IDbContext context, UserManager<BlitzerUser> userManager, IConfiguration aConfig) : base(context, userManager)
        {
            Configuration = aConfig;

        }

        // GET: FOPController/Create
        public ActionResult CreateFOP(string id)
        {
            string FuncName = ClassName + $"Create (string={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lFOP = new FOPBusiness(DbContext).Create(id, GetCurrentUser());
                var lUIFOP = FOPUIHelper.Convert(lFOP);
                return View(lUIFOP);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + "Failed to create CC", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return RedirectToAction("Index", "Portal");
        }

        // POST: FOPController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFOP([FromForm] UIFOP aFOP)
        {
            string FuncName = $"{ClassName}Create (UIFOP = {aFOP.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lFOPBiz = new FOPBusiness(DbContext);
                var lFOP = lFOPBiz.Save(aFOP, GetCurrentUser());
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + " Failed to create new credit card for client", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction("Index", "Client");
        }


        // GET: ClientController/Create
        [Authorize(Roles = Role.Client)]
        public ActionResult Index(string id)
        {
            var lClientPortal = new UIClientPortal();
            var lCurrentUser = GetCurrentUser();

            var lQuotes = new QuoteBusiness(DbContext).GetQuotes(lCurrentUser);
            lClientPortal.Clients = new List<UIContact>();
            foreach (var lMember in new ContactBusiness(DbContext).GetHouseHoldMembers(lCurrentUser))
                lClientPortal.Clients.Add(ContactUIHelper.Convert(new ContactBusiness(DbContext).Get(lMember.Member)));
            lClientPortal.Quotes = QuoteUIHelper.Convert(DbContext, lQuotes);
            lClientPortal.UserId = GetCurrentUserID();
            var lQuoteRequests = new QuoteRequestBusiness(DbContext).Get(lCurrentUser);
            lClientPortal.Clients[0].QuoteRequests = QuoteRequestUIHelper.Convert(DbContext, lQuoteRequests);
            var lTripBiz = new TripBusiness(DbContext);
            var lTrips = lTripBiz.Get(lCurrentUser, Trip.Statuses.Active);
            lClientPortal.Clients[0].TripsActive = new List<UITrip>();
            lClientPortal.Clients[0].TripsInActive = new List<UITrip>();
            foreach (var lTrip in lTrips.Where(x => x.TripStatus == Trip.Statuses.Active))
                lClientPortal.Clients[0].TripsActive.Add(TripUIHelper.Convert(lTripBiz.Get(lTrip.ID)));
            foreach (var lTrip in lTrips.Where(x => x.TripStatus != Trip.Statuses.Active && x.TripStatus != Trip.Statuses.Deleted))
                lClientPortal.Clients[0].TripsInActive.Add(TripUIHelper.Convert(lTripBiz.Get(lTrip.ID)));
            foreach (var lQuoteRequest in lQuoteRequests)
            {
                var lUIQR = lClientPortal.Clients[0].QuoteRequests.Where(x => x.Id == lQuoteRequest.QuoteRequestID).First();
                lUIQR.ActiveQuoteGroups = QuoteGroupUIHelper.Convert(mContext, new QuoteGroupBusiness(DbContext).Get(lQuoteRequest, QuoteGroupFilter.Active));
                if (lUIQR.ActiveQuoteGroups != null && lUIQR.ActiveQuoteGroups.Count() > 0)
                {
                    lUIQR.ActiveQuoteGroups.First().QuoteList = lUIQR.ActiveQuoteGroups.First().Quotes.SelectMany(x => x.Value).Where(y => y.Status == QuoteStatus.Ready || y.Status == QuoteStatus.Sent).ToList();
                    if (lUIQR.ActiveQuoteGroups.First().QuoteList == null ||
                        lUIQR.ActiveQuoteGroups.First().QuoteList.Count() == 0)
                        lClientPortal.Clients[0].QuoteRequests.Remove(lUIQR);
                }
            }

            return View(lClientPortal);
        }

        public string GetIPAddress()
        {
            // This is for Unit Testing
            if (HttpContext == null)
                return "127.0.0.1";

            return HttpContext.Connection.RemoteIpAddress.ToString();
        }

        // GET: QuoteGroupController/Quote/5
        [HttpGet]
        public ActionResult Quote(string id)
        {
            string FuncName = ClassName + $"Quote(int = {id})";
            Logger.EnterFunction(FuncName);
            QuoteGroup lQuoteGroup = null;
            try
            {
                var lQGBiz = new QuoteGroupBusiness(mContext);
                var lQRBiz = new QuoteRequestBusiness(mContext);

                int lQuoteId = 0;
                bool IntParse = int.TryParse(id, out lQuoteId);
                if (IntParse == true)
                    lQuoteGroup = lQGBiz.Get(lQuoteId);
                else
                    lQuoteGroup = lQGBiz.Get(id);
                Logger.LogDebug($"QuoteGruop ID ={lQuoteGroup}");

                if (lQuoteGroup == null)
                {
                    Logger.LogWarning(FuncName + "Can't dislay QuoteView because Quote was null for id = " + id);
                    return RedirectToAction(nameof(HomeController.Index), "Index");
                }
                new ClientViewBusiness(DbContext).PageView(lQuoteGroup, "ClientController::Quote", GetIPAddress());

                var lQuotes = lQGBiz.GetBestQuotesByTourOperator(lQuoteGroup, GetCurrentAgent());
                var lFilterQuotes = lQuotes.GroupBy(x => x.AccommodationRoomTypeID).Select(x => x.OrderBy(y => y.Total)).Select(x => x.First()).ToList();
                var lTopQuotes = lQGBiz.GetTop5Resorts(lFilterQuotes);
                UIQuoteRequestWrapper lQuoteWrapper = lQRBiz.GetQuoteInfo(lQuoteGroup, GetCurrentAgent());
                lQuoteWrapper.Quotes = QuoteRequestEditUIHelper.Convert(mContext, lTopQuotes, GetCurrentUser(), false).ToList();
                return View(lQuoteWrapper);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retrieve quote view", e);
                return View("InvalidQuote", ContactUIHelper.Convert(lQuoteGroup.QuoteRequest.Agent));
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

        public ActionResult Quote(int id)
        {
            string FuncName = ClassName + $"Quote(int = {id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lQGBiz = new QuoteGroupBusiness(mContext);
                var lQuoteGroup = lQGBiz.Get(id);
                if (lQuoteGroup == null)
                {
                    Logger.LogWarning(FuncName + "Can't dislay QuoteView because Quote was null for id = " + id);
                    return RedirectToAction(nameof(HomeController.Index), "Index");
                }
                var lQuotes = lQGBiz.GetBestQuotesByTourOperator(lQuoteGroup, GetCurrentAgent());
                var lTopQuotes = lQGBiz.GetTop5Resorts(lQuotes);
                var lUIQRE = QuoteRequestEditUIHelper.Convert(mContext, lTopQuotes, GetCurrentUser(), false).ToList();
                if (lUIQRE.Count > 0)
                    lUIQRE[0].CountryData = new CountryPageDataAccess(mContext).Get(18);
                return View(lUIQRE);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to retrieve quote view", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            throw new InvalidOperationException();
        }

        // POST: ClientController/Quote
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Quote([FromForm] UIQuoteRequestWrapper aQuoteWrapper)
        {
            var lUIQuote = aQuoteWrapper.Quotes.Where(x => x.Booked == true).First();
            var lTrip = new TripBusiness(mContext, Configuration).Book(lUIQuote, GetCurrentUser());
            QuoteRequest lQR = null;

            if (lUIQuote.Id < 0)
            {
                var lMap = new QuoteDataAccess(DbContext).GetMapper(lUIQuote.Id * -1);
                lQR = lMap.QuoteGroup.QuoteRequest;
            }
            else
            {
                var lQuote = new QuoteBusiness(mContext).Get(lUIQuote.Id);
                lQR = lQuote.QuoteRequest;
            }


            Contact lContact = null;
            if (lQR != null && lQR.Opportunity != null)
                lContact = lQR.Opportunity.Travelers[0].User;

            if (GetCurrentAgent() != null)
                return RedirectToAction("Index", "Portal");
            else if (new ContactBusiness(null).PassesProfileCheck(lContact) == true)
                return RedirectToAction("Index", "Client");
            else
                return RedirectToAction("Activate", "Client", new { id = lContact.Id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Booking Number</param>
        /// <returns></returns>
        // GET: PaymentController/Create
        [Authorize(Roles = Role.Client)]
        public ActionResult Pay(int id)
        {
            string FuncName = $"{ClassName}Create (id={id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lBooking = new BookingBusiness(DbContext, Configuration).Get(id);
                var lUIPayment = new PaymentBusiness(DbContext, Configuration).Create(lBooking);
                var lTrip = new TripBusiness(DbContext).Get(lBooking.TripID);
                ViewBag.Travelers = ListHelper.GetTravelers(lTrip.Travelers);
                ViewBag.Cards = ListHelper.GetCreditCards(DbContext, lTrip.Travelers);
                return View(lUIPayment);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $" Failed to create UIPayment", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction("Edit", "Booking", new { id = id });
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Client)]
        public ActionResult Pay([FromForm] UIPayment aPayment)
        {
            string FuncName = $"{ClassName}Create (UIPayment={aPayment.PaymentId})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lPayment = new PaymentBusiness(DbContext, Configuration).Save(aPayment, GetCurrentUser());
                new NotificationBusiness(DbContext, Configuration).SendPayment(lPayment);
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $" Failed to Save UIPayment", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction("Index", "Client");
        }

        // GET: ClientController/Create
        [Authorize(Roles = Role.Client)]
        public ActionResult Profile(string id)
        {
            string FuncName = $"{ClassName}Edit(id = {id}) -";
            try
            {
                if (id == null)
                    id = GetCurrentUserID();

                var lContact = new ContactBusiness(mContext).Get(id);
                if (lContact == null)
                    lContact = new ContactBusiness(mContext).Create(GetCurrentAgent());

                var lUIContact = ContactUIHelper.ASPConvert(lContact);
                //PopulateLookups(lUIContact);
                if (lUIContact == null)
                {
                    Logger.LogDebug($"{FuncName} Contact not found");
                    return NotFound();
                }

                Logger.LogDebug($"{FuncName} returned Contact");
                return View(lUIContact);
            }
            finally
            {
            }
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Client)]
        public ActionResult Profile([FromForm] UIContact aContact)
        {
            string FuncName = $"{ClassName}Profile (UIContact = {aContact.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lCBiz = new ContactBusiness(mContext);
                var lNewContact = lCBiz.Save(aContact, GetCurrentUser());
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Country Page", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ClientController/Create
        [Authorize(Roles = Role.Client)]
        public ActionResult AddProfile(string id)
        {
            string FuncName = $"{ClassName}AddProfile(id = {id}) -";
            try
            {
                var lUser = new ContactBusiness(DbContext).Get(id);

                var lContact = new ContactBusiness(mContext).Create(lUser.OwnedBy);

                var lUIContact = ContactUIHelper.ASPConvert(lContact);
                lUIContact.RootMemberId = id;
                lUIContact.AgentId = lUser.OwnedById;
                ViewBag.RelationShips = ListHelper.GetRelationShips(new RelationshipDataAccess(mContext).GetRelationships());

                if (lUIContact == null)
                {
                    Logger.LogDebug($"{FuncName} Contact not found");
                    return NotFound();
                }

                Logger.LogDebug($"{FuncName} returned Contact");
                return View(lUIContact);
            }
            finally
            {
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Role.Client)]
        public ActionResult AddProfile([FromForm] UIContact aContact)
        {
            string FuncName = $"{ClassName}AddProfile (UIContact = {aContact.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                var lCBiz = new ContactBusiness(mContext);
                var lPrimary = lCBiz.Get(aContact.RootMemberId);
                var lNewContact = lCBiz.Save(aContact, lCBiz.Get(aContact.AgentId));
                lCBiz.AddHouseHoldMember(lPrimary, lNewContact);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Country Page", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ClientController/Edit/5
        [AllowAnonymous]
        public ActionResult Activate(string id)
        {
            var lUser = new ContactBusiness(mContext).Get(id);
            if (lUser == null || lUser.ActivationDate != null)
                return RedirectToAction("Index", "Home");
            UIRegister lUI = new UIRegister();
            lUI.Id = id;
            return View(lUI);
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Activate([FromForm] UIRegister aRegister)
        {
            string FuncName = $"{ClassName}Activate (UIRegister = {aRegister.Id})";
            try
            {
                if (aRegister.NewPassword != aRegister.ConfirmationPassword)
                {
                    ViewBag.ErrorMsg = "Your confirmation password doesn't match";
                    Logger.LogWarning($"{FuncName} - Password doesn't match");
                    return View(aRegister);
                }
                else if (aRegister.NewPassword.Length < 8)
                {
                    ViewBag.ErrorMsg = "Password must be at least 8 characters long";
                    Logger.LogWarning($"{FuncName} - Password must be at least 8 characters long");
                    return View(aRegister);
                }
                else if (aRegister.NewPassword.Any(c => char.IsDigit(c)) == false)
                {
                    ViewBag.ErrorMsg = "Password must contain at least 1 number";
                    Logger.LogWarning($"{FuncName} - Password must contain a digit");
                    return View(aRegister);
                }
                else if (aRegister.NewPassword.All(c => char.IsLetterOrDigit(c)) == true)
                {
                    ViewBag.ErrorMsg = "Password must contain non alpha numeric";
                    Logger.LogWarning($"{FuncName} - Password must contain non alpha numeric");
                    return View(aRegister);
                }
                else if (aRegister.NewPassword.Any(c => char.IsUpper(c)) == false)
                {
                    ViewBag.ErrorMsg = "Password must contain uppercase letter";
                    Logger.LogWarning($"{FuncName} - Password must contain uppercase");
                    return View(aRegister);
                }

                //todo Security issue here.  Can't allow people to just keep hitting the site and guessing at userid
                var lCbiz = new ContactBusiness(mContext);
                var lUser = lCbiz.Get(aRegister.Id);
                var lIdUser = _userManager.FindByEmailAsync(lUser.PrimaryEmail).Result;
                var lResult = _userManager.ChangePasswordAsync(lIdUser, ContactBusiness.NEWUSERPWD, aRegister.NewPassword).Result;
                if (lResult.Succeeded)
                {
                    lUser.ActivationDate = DateTime.Now;
                    lCbiz.Save(lUser, GetCurrentUser());
                    Logger.LogInfo($"{FuncName} Successfully changed password to " + aRegister.NewPassword);
                    //return RedirectToAction("Login", "Account", new { Areas = "Identity" });
                    return LocalRedirect("/Identity/Account/Login");
                }
                else
                {
                    string lErrMsg = "";
                    foreach (var lError in lResult.Errors)
                        lErrMsg += lError.Description;

                    Logger.LogError($"{FuncName} Failed to change password: {lErrMsg}");
                }
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName, e);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
