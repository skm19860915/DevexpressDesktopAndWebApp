using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;



namespace WebApp.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {
        IDbContext mContext;
        public UserProfileViewComponent(IDbContext aContext)
        {
            mContext = aContext;
        }

        public IViewComponentResult Invoke(string aUserID)
        {
            Contact lUser = new ContactDataAccess(mContext).Get(aUserID);
            return View(lUser);
        }

    }
}
