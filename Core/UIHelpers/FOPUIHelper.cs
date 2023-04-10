using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Business;
using System.Linq;

namespace BlitzerCore.UIHelpers
{
    public class FOPUIHelper
    {
        public static List<UIFOP> Convert(List<FOP> aFOPs)
        {
            var lOutput = new List<UIFOP>();
            if (aFOPs == null)
                return lOutput;

            foreach (var lFOP in aFOPs)
                lOutput.Add(Convert(lFOP));
            return lOutput;
        }

        public static UIFOP Convert ( FOP aFOP )
        {
            var lUIFOP = new UIFOP();
            lUIFOP.CVN = aFOP.CVN;
            lUIFOP.Expiration = aFOP.Expiration;
            lUIFOP.Number = Obscure(aFOP.Number);
            lUIFOP.UserID = aFOP.OwnerID;
            return lUIFOP;
        }

        public static FOP Convert(IDbContext aContext, UIFOP aUIFOP)
        {
            FOP lFOP = new FOPBusiness(aContext).Get(aUIFOP.Id);
            if (lFOP == null)
                lFOP = new FOP();
            lFOP.CVN = aUIFOP.CVN;
            lFOP.Expiration = aUIFOP.Expiration;
            lFOP.Number = aUIFOP.Number;
            lFOP.OwnerID = aUIFOP.UserID;
            return lFOP;
        }

        public static string Obscure(string aNumber)
        {
            if (aNumber == null)
                return "";
            var lLen = aNumber.Length;
            return "XXXX-XXXX-XXXX-" + aNumber.Substring(aNumber.Length - 4);
        }
    }
}
