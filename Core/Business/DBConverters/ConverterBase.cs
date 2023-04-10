using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BlitzerCore.Business.DBConverters
{
    public class ConverterBase 
    {
        protected const string ADULT_ONLY = " - Adult Only";
        protected const string ADULTS_ONLY = " - Adults Only";

        protected string RemoveAdultsOnly(string aName)
        {
            var lName = aName;
            var lOutput = "";

            // Remove Adults Only in any form
            var lIndex = lName.IndexOf(ADULT_ONLY);
            var lIndex2 = lName.IndexOf(ADULTS_ONLY);
            if (lIndex < 1 && lIndex2 < 1)
            {
                lOutput = lName;
                return lOutput;
            }

            if (lIndex2 >= 0)
                lOutput = lName.Substring(0, lIndex2);
            else
                lOutput = lName.Substring(0, lIndex);

            return lOutput;
        }

    }
}
