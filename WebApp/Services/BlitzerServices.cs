using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;

namespace WebApp.Services
{
    public class BlitzerServices : IBlitzer
    {
        // Don't forget to modify ExtensionMethods::ViewExtensions::isRelease()
        //                        BlitzerCore.Models.DBVersion
        //                        WebApp.Services.BlitzerServices
        //                        Desktop.BlitzerDesktop.Label
        public static int Major { get { return 5; } }  // Major Release on 1/19/2021 Setup for Payments
        public static int Minor { get { return 1; } } // 
        public bool IsProdDb { get; set; }
        public static string Label { get { return $"WebApp Version {Major}.{Minor}"; } }

    }
}
