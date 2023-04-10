using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.Models;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using WebApp.DataServices;
using System.Security.Claims;
using System.IO;
using BlitzerCore.Utilities;
using BlitzerCore.Models.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
using System;

namespace WebApp.Controllers
{
    public enum AppRoles { Admin, Agent, Client}

    public class BaseController : Controller
    {
        const string ClassName = "BaseController::";


        protected readonly IDbContext mContext;
        protected IDbContext DbContext { get; }
        protected readonly UserManager<BlitzerUser> _userManager;
        protected AppRoles UserRole { get; set; }

        public BaseController(IDbContext context, UserManager<BlitzerUser> userManager)
        {
            mContext = context;
            DbContext = context;
            _userManager = userManager;
        }

        public BaseController(IDbContext context)
        {
            mContext = context;
            DbContext = context;
        }

        protected bool IsDanger(Contact aUser)
        {
            // HttpContext is null with NUnitTest
            if (this.HttpContext == null)
                return false;
            try
            {
                string FuncName = $"{ClassName}IsDanger";
                var services = this.HttpContext.RequestServices;
                var lBlitzer = (IBlitzer)services.GetService(typeof(IBlitzer));
                ViewBag.IsDanger = false;

                if (aUser == null)
                    return ViewBag.IsDanger;

                if (aUser.Id.ToUpper() == Defines.EricKey.ToUpper() && lBlitzer.IsProdDb)
                    ViewBag.IsDanger = true;

                return ViewBag.IsDanger;
            } catch ( Exception  ) {
                return false;
            }
    }

        protected async System.Threading.Tasks.Task<bool> SecuritySetup()
        {
            var lUser = GetCurrentUser() as Agent;
            ViewBag.IsAdmin = await IsAdmin(lUser);
            return true;
        }

        protected bool IsAdmin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            return GetRole((BlitzerUser)null).Result == AppRoles.Admin;
        }

        private async System.Threading.Tasks.Task<AppRoles> GetRole(BlitzerUser aUserID)
        {
            // Get the roles for the user
            var roles = await _userManager.GetRolesAsync(aUserID);
            if (roles.Contains("Administrator"))
                return AppRoles.Admin;
            else if (roles.Contains("Agent"))
                return AppRoles.Agent;
            else
                return AppRoles.Client;

        }

        protected async System.Threading.Tasks.Task<bool> IsAdmin(Contact aUser)
        {
            string FuncName = $"{ClassName}IsAdmin";

            if (_userManager == null)
                return false;

            var user = await _userManager.FindByEmailAsync(aUser.PrimaryEmail);
            if (user != null)
            {
                UserRole = GetRole(user).Result;
                return UserRole == AppRoles.Admin;
            }
            else
            {
                Logger.LogWarning($"{FuncName} Failed to determine role for ${aUser.Name}");
                return false;
            }
        }

        protected string GetReferer()
        {
            return Request.Headers["Referer"].ToString();
        }

        public async Task<bool> SetReturnUrl ( BaseUI aUI)
        {
            aUI.returnUrl = GetReferer();
            await SecuritySetup();
            return true;
        }

        public ActionResult ReturnToCaller (BaseUI aUI )
        {
            if (aUI.returnUrl == null)
                return RedirectToAction("Index", "Portal");
            return Redirect(aUI.returnUrl);
        }

        public RepositoryContext GetContext()
        {
            return (RepositoryContext)mContext;
        }
        public Agent GetCurrentAgent(IDbContext aContext = null )
        {
            var lDbContext = aContext == null ? mContext : aContext;
            string FuncName = $"{ClassName}GetCurrentAgent";
            var lAgent =  new ContactDataAccess(lDbContext).GetAgent(GetCurrentUserID());
            IsDanger(lAgent);
            if ( lAgent == null )
            {
                Logger.LogWarning($"{FuncName} returned null agent");
                return null;
            }
            return lAgent;
        }

        public Contact GetCurrentUser()
        {
            string FuncName = $"{ClassName}GetCurrentUser";
            var lId = GetCurrentUserID();
            var lContact = new ContactBusiness(mContext).Get(lId);
            if ( lContact == null )
                Logger.LogWarning($"{FuncName} Failed to find contact for ${lId}");
            IsDanger(lContact);
            return lContact;
        }

        public string GetSystemId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

        }

        public string GetCurrentUserID()
        {
            string FuncName = $"{ClassName}GetCurrentUserID";
            if (HttpContext == null)
            {                
                Logger.LogWarning(FuncName + " returning hard coded userid because HttpConext is null");
                return "Pam";
            }

            var lSysId = "";
            try
            {
                lSysId = GetSystemId();
                Logger.LogDebug(FuncName + $":GetSystemId()=>{lSysId}");
                return new ContactDataAccess(mContext).GetBySystemId(lSysId);
            }
            catch ( Exception e )
            {
                Logger.LogException($"{FuncName}Could not Get System ID ", e);
            }

            return "";
        }
    }
}
