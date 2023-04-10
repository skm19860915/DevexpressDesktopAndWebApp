using BlitzerCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BlitzerCore.Helpers
{
    public class AppHelper
    {
        public static string GetTempPath (string aContentPath)
        {
            string path = Path.Combine(aContentPath, Defines.TEMPORARYDIR);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
