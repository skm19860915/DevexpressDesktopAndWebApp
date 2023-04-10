using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.ExtensionMethods
{
    public static class ViewExtensions
    {
        // Don't forget to modify ExtensionMethods::ViewExtensions::isRelease()
        //                        BlitzerCore.Models.DBVersion
        //                        WebApp.Services.BlitzerServices

        public static bool isRelease ()
        {
            return true;
        }
    }
}
