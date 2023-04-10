using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.UIHelpers;
using BlitzerCore.Helpers;
using WebApp.AspNetHelper;
using WebApp.Services;
using WebApp.DataServices;

namespace WebApp.BusinessHelpers
{
    public class ContactHelper
    {
        const string ClassName = "ContactHelper::";
        protected IDbContext DbContext { get; }

        public ContactHelper (IDbContext context)
        {
            DbContext = context;
        }

        public async Task<IEnumerable<IdentityError>> CreateUser(UserManager<BlitzerUser> aUserManager, IConfiguration aConfig, Contact aUser)
        {
            string FuncName = $"{ClassName}CreateUser (UIContact={aUser.PrimaryEmail})";

            if ( aUser.SystemId != null )
            {
                Logger.LogInfo($"{FuncName} - No Need to Create User {aUser.Name} because they have a system id");
                return new List<IdentityError>();
            }

            var lNewUser = new BlitzerUser()
            {
                UserName = aUser.PrimaryEmail,
                Email = aUser.PrimaryEmail,
                FirstName = aUser.First,
                LastName = aUser.Last,
                EmailConfirmed = true
            };
            try
            {
                if (lNewUser != null && lNewUser.Email != null && lNewUser.Email.Trim().Length > 0)
                {
                    var lResult = await aUserManager.CreateAsync(lNewUser, ContactBusiness.NEWUSERPWD);
                    if (lResult.Succeeded || lResult.Errors.ElementAt(0).Code == "DuplicateUserName")
                    {
                        var lCDA = new ContactDataAccess(DbContext);
                        var lNewSysUser = lCDA.GetContactByEmail(aUser.PrimaryEmail);
                        lCDA.AddClientRole(lNewSysUser);
                        var lCBiz = new ContactBusiness(DbContext);
                        aUser.SystemId = lNewUser.Id;
                        new ContactDataAccess(DbContext).Save(aUser);
                    }
                    else
                    {
                        return lResult.Errors;
                    }
                }
                else
                {
                    Logger.LogInfo($"{FuncName} - Can't create new user account because email is blank");
                }
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to create new user", e);
            }
            finally
            {
                Logger.LeaveFunction(FuncName);
            }
            return new List<IdentityError>();
        }
    }
}
