using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desktop
{
    public class BlitzerDesktop
    {
        // Don't forget to modify ExtensionMethods::ViewExtensions::isRelease()
        //                        BlitzerCore.Models.DBVersion
        //                        WebApp.Services.BlitzerServices
        //                        Desktop.BlitzerDesktop.Label

        public static int Major { get { return 4; } }  // Major release on 1/19/21
        public static int Minor { get { return 0; } } // 

        public static string Label { get { return $"Blitzer Version {Major}.{Minor}"; } }

    }
}

