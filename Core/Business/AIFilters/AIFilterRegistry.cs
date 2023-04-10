using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;

namespace BlitzerCore.Business.AIFilters
{
    public class AIFilterRegistry
    {
        readonly IDbContext mContext;

        public AIFilterRegistry(IDbContext mContext)
        {
            this.mContext = mContext;
        }

        public static AIFilter GetFilter(int? aFilterID )
        {
            var lFilters = RegisterFilters();
            if ( aFilterID != null )    
                return lFilters.Where(x => x.AIFilterID == aFilterID).FirstOrDefault();

            return null;
        }

        public static List<AIFilter> RegisterFilters()
        {
            var lFilters = new List<AIFilter>();
            lFilters.Add(new MinimizeDurationDepartAround8AM());
            lFilters.Add(new Return3ShortestFlights());
            return lFilters;
        }

        public static void UpdateDataBaseWithAIFilters(IDbContext mContext, List<AIFilter> aAvailableFilters)
        {
            var lFDA = new FilterDataAccess(mContext);
            foreach (var lAIFilter in aAvailableFilters)
            {
                lAIFilter.ID = lAIFilter.AIFilterID;
                lFDA.Save(lAIFilter);
            }
        }
    }
}
