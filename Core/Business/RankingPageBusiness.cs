using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class RankingPageBusiness
    {
        public IDbContext DbContext { get; set; }

        public RankingPageBusiness(IDbContext aContext)
        {
            DbContext = aContext;
        }

        public Page Get(int aID)
        {
            var lPageDA = new RankingPageDataAccess(DbContext);
            BlitzerCore.Models.UI.UIRanking lPage = lPageDA.Get(aID);
            if (lPage == null)
                return lPage;
            lPage.Rankings = new RankingPageDataAccess(DbContext).GetAll(aID).ToList();
            foreach (var lRanking in lPage.Rankings)
                lRanking.ResortPage = new PageDataAccess(DbContext).GetResort(lRanking.ResortPageId) as UIResortPage;
            return lPage;
        }
    }
}
