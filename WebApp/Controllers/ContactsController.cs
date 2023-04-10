using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Models.ASP;
using BlitzerCore.Utilities;
using BlitzerCore.UIHelpers;
using WebApp.AspNetHelper;
using BlitzerCore.DataAccess;
using WebApp.DataServices;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize]
    public class ContactsController : BaseController
    {
        const string ClassName = "ContactsController::";
        IConfiguration mConfig;
        public ContactsController(IDbContext context, UserManager<BlitzerUser> userManager, IConfiguration aConfig) : base(context, userManager)
        {
            mConfig = aConfig;
        }

        // GET: /Contacts/5
        public async Task<ActionResult> Details(string id)
        {
            string FuncName = $"{ClassName}Details(id = {id}) -";
            try
            {
                var lContact = new ContactBusiness(mContext).Get(id);

                if (lContact == null)
                {
                    Logger.LogDebug($"{FuncName} Contact not found");
                    return NotFound();
                }

                Logger.LogDebug($"{FuncName} returned Contact");
                var lUIContact = ContactUIHelper.Convert(lContact);
                lUIContact.OpportunitiesActive = OpportunityUIHelper.Convert(DbContext, new OpportunityBusiness(DbContext).Get(lContact));
                lUIContact.OpportunitiesInActive = OpportunityUIHelper.Convert(DbContext, new OpportunityBusiness(DbContext).GetInActive(lContact));
                lUIContact.TripsActive = TripUIHelper.Convert(new TripBusiness(DbContext).GetActiveTrips(lContact));
                lUIContact.TripsInActive = TripUIHelper.Convert(new TripBusiness(DbContext).Get(lContact, Trip.Statuses.Completed));
                lUIContact.TripsInActive.AddRange( TripUIHelper.Convert(new TripBusiness(DbContext).Get(lContact, Trip.Statuses.Cancelled)));
                var tagBusiness = new TagBusiness(DbContext, null);
                var tags = TagUIHelper.Convert(tagBusiness.GetTags(lContact), tagBusiness.GetAll());
                lUIContact.Categories = CategoryUIHelper.Convert(tags);

                await SetReturnUrl(lUIContact);
                return View(lUIContact);
            }
            finally
            {
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([FromForm] UIContact aContact)
        {
            Note lNote = new Note()
            {
                Memo = aContact.Note_Text,
                ContactId = aContact.Id,
                When = DateTime.Now,
                Where = aContact.Note_Where,
                Who = aContact.Note_Who,
                WriterId = GetCurrentUserID()
            };
            new NoteBusiness(DbContext).Save(lNote);
            return ReturnToCaller(aContact);
        }


        private void PopulateLookups(ASPContact aASPContact)
        {
            IEnumerable<Company> lCompanies = new CompanyDataAccess(DbContext).GetAll(GetCurrentAgent());
            aASPContact.CarMemberShips = ListHelper.GetMemberShips(lCompanies.Where(x => x.BusinessTypeID == (int)MemberShipType.Car));
            aASPContact.HotelMemberShips = ListHelper.GetMemberShips(lCompanies.Where(x => x.BusinessTypeID == (int)MemberShipType.Hotel));
            aASPContact.FrequentFlyerMemberShips = ListHelper.GetMemberShips(lCompanies.Where(x => x.BusinessTypeID == (int)MemberShipType.AirLine));
            aASPContact.MemberShips = new List<MemberShip>();
            ViewBag.Employers = ListHelper.GetEmployers(lCompanies);
        }

        // : /Contacts/5
        public ActionResult Edit(string id)
        {
            string FuncName = $"{ClassName}Edit(id = {id}) -";
            try
            {
                var lContact = new ContactBusiness(mContext).Get(id);
                if (lContact == null)
                    lContact = new ContactBusiness(mContext).Create(GetCurrentAgent());

                var lUIContact = ContactUIHelper.ASPConvert(lContact);
                PopulateLookups(lUIContact);
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

        // : /Contacts/5
        public ActionResult Memberships(string id)
        {
            string FuncName = $"{ClassName}Memberships(id = {id}) -";
            try
            {
                var lContact = new ContactBusiness(mContext).Get(id);
                if (lContact == null)
                    lContact = new ContactBusiness(mContext).Create(GetCurrentAgent());

                var lUIContact = ContactUIHelper.ASPConvert(lContact);
                PopulateLookups(lUIContact);
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

        /// <summary>
        /// Send the contact a portal email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Portal(string id)
        {
            string FuncName = $"{ClassName}Portal(id = {id}) ";
            try
            {
                var lCBiz = new ContactBusiness(mContext);
                var lContact = lCBiz.Get(id);
                await new BusinessHelpers.ContactHelper(DbContext).CreateUser(_userManager, mConfig, lContact);
                lCBiz.SendPortalLink(lContact, mConfig);
                Logger.LogDebug($"{FuncName} returned Contact");
                return RedirectToAction(nameof(ContactsController.Details), new { id = id });
            }
            finally
            {
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromForm] ASPContact aContact)
        {
            string FuncName = $"{ClassName}Edit(Contact = {aContact.Id})";
            Logger.EnterFunction(FuncName);
            try
            {
                //UIContact lContact = ContactUIHelper.Convert(aContact);
                var lCnt = new ContactBusiness(mContext).Save(aContact, GetCurrentAgent());
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to save Country Page", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            if (aContact.Id == null || aContact.Id == "")
                return RedirectToAction("Index", "Portal");
            return RedirectToAction(nameof(ContactsController.Details), new { id = aContact.Id });
        }
        [HttpGet]
        public ActionResult ModifyMembers(string id)
        {
            var lContact = new ContactBusiness(DbContext).Get(id);
            return View(ContactUIHelper.Convert(lContact));
        }

        public ActionResult Index()
        {
            string FuncName = $"{ClassName}Index() -";
            try
            {
                var lContacts = new ContactBusiness(mContext).GetAll(GetCurrentAgent());
                Logger.LogDebug($"{FuncName} Returning {lContacts.Count()} rows");
                return View(lContacts);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }

    }
}
