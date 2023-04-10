using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.WebBots;

namespace BlitzerCore.Business
{
    public class TourOperatorRegistry
    {
        readonly IDbContext mContext;

        public TourOperatorRegistry(IDbContext aContext)
        {
            mContext = aContext;
        }

        public static WebBotBase GetWebBot(IDbContext aContext, int? aWebBotID )
        {
            var lWebBots = RegisterWebBots(aContext);
            if (aWebBotID != null )    
                return lWebBots.FirstOrDefault(x => x.TourOperatorID == aWebBotID);

            return null;
        }

        public static List<WebBotBase> RegisterWebBots(IDbContext aContext)
        {
            var lOutput = new List<WebBotBase>();
            lOutput.Add(new WorldAgentDirectBot(aContext));
            lOutput.Add(new AAVacationBot(aContext));
            lOutput.Add(new VacationExpressBot(aContext));
            return lOutput;
        }

        public static List<WebBotBase> GetAllWebBots(IDbContext aContext)
        {
            var lOutput = new List<WebBotBase>();
            lOutput.Add(new WorldAgentDirectBot(aContext));
            lOutput.Add(new AAVacationBot(aContext));
            lOutput.Add(new VacationExpressBot(aContext));
            return lOutput;
        }

        public static void UpdateDataBaseWithTourOperators(IDbContext aDbContext, List<WebBotBase> aWebBots)
        {
            var lTODA = new TourOperatorDataAccess(aDbContext);
            foreach (var lOperators in aWebBots)
            {
                if (lTODA.Get(lOperators.GetTourOperatorName()) == null) 
                    lTODA.Save(new TourOperator() { Name = lOperators.GetTourOperatorName(), BusinessTypeID = 1 });
            }
        }
    }
}
