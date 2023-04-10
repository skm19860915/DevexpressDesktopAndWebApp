using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class RankingPageDataAccess
    {
        IDbContext mContext = null;

        public Microsoft.EntityFrameworkCore.DbSet<UIRanking> Table { get; set; }

        public RankingPageDataAccess(IDbContext aContext)
        {
            mContext = aContext;
            Table = mContext.RankingPages;
        }

        public UIRanking Get(int aID)
        {
            return Table
                //.Include(x => x.Comparables)
                //.Include(x => x.CenterContent).ThenInclude(y => y.Paragraphs)
                //.Include(x => x.CenterContent).ThenInclude(y => y.Video)
                //.Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                //.Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1600x1200)
                //.Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080)
                //.Include(x => x.LeftPanel).ThenInclude(y => y.Comparables).ThenInclude(m => m.Media)
                //.Include(x => x.RightPanel).ThenInclude(y => y.Tiles).ThenInclude(m => m.Media)
                .FirstOrDefault(x => x.Id == aID);
        }

        public List<Ranking> GetAll(int aId)
        {
            return Table.SelectMany(x => x.Rankings).Where(y => y.RankingPageId == aId).ToList();
        }

        public int Save(UIRanking aPage)
        {
            try
            {
                if (aPage.Id == 0)
                    Table.Add(aPage);
                else
                    Table.Update(aPage);

                int lCount = mContext.SaveChanges();
                Logger.LogDebug($"Updated {lCount} Ranking Pages");
            } catch ( Exception e )
            {
                Logger.LogException("Failed to save Ranking Page", e);
                throw (e);
            }

            return 0;
        }
    }
}

