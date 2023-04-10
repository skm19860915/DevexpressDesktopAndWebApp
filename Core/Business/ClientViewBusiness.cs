using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Utilities;
using Microsoft.AspNetCore.Http;

namespace BlitzerCore.Business
{
    public class ClientViewBusiness
    {
        const string ClassName = "ClientViewBusiness::";
        const string EZE2TRAVEL_IPADDR = "70.60.200.242";

        private ClientViewDataAccess DataAccess { get; set; }

        public IDbContext DbContext { get; set; }

        public ClientViewBusiness(IDbContext aContext)
        {
            DbContext = aContext;
            DataAccess = new ClientViewDataAccess(aContext);
        }

        public void Save (ClientView aView)
        {

        }

        public void PageView(QuoteGroup aQuoteGroup, string aPage, string aIP)
        {
            string FuncName = ClassName + $"PageView({aQuoteGroup.Id},{aPage}, {aIP})";

            if (aIP == EZE2TRAVEL_IPADDR || aIP.Length < 6)
                return;

            try
            {
                Logger.EnterFunction(FuncName);
                ClientView aView = new ClientView();
                aView.IPAddress = aIP;
                aView.When = DateTime.Now;
                aView.Page = aPage;
                aView.QuoteGroup = aQuoteGroup;
                DataAccess.Save(aView);
            } catch ( Exception e )
            {
                Logger.LogException($"{FuncName} Exception Failed", e);
            } finally
            {
                Logger.LeaveFunction(FuncName);
            }
        }
    }
}
