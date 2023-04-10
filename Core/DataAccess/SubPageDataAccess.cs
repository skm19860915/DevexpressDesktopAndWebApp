using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class SubPageDataAccess
    {
        IDbContext mContext = null;

        public Microsoft.EntityFrameworkCore.DbSet<SubPage> Table { get; set; }

        public SubPageDataAccess(IDbContext aContext)
        {
            mContext = aContext;
            Table = mContext.SubPages;
        }

        public SubPage Get(int aID)
        {
            return Table
                .Include(x => x.CenterContent).ThenInclude(y => y.Paragraphs)
                .Include(x => x.CenterContent).ThenInclude(y => y.Video)
                .Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1600x1200)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080)
                .FirstOrDefault(x => x.Id == aID);
        }

        public List<SubPage> GetAll()
        {
            return Table
                .Include(x => x.CenterContent).ThenInclude(y => y.Paragraphs)
                .Include(x => x.CenterContent).ThenInclude(y => y.Video)
                .Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1600x1200)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080).ToList();
        }

        public int Save(SubPage aPage)
        {
            try
            {
                if (aPage.Id == 0)
                    Table.Add(aPage);
                else
                    Table.Update(aPage);

                int lCount = mContext.SaveChanges();
                Logger.LogDebug($"Saved/Updated {lCount} SubPages records");
            } catch ( Exception e )
            {
                Logger.LogException("Failed to save SubPage Page", e);
                throw (e);
            }

            return 0;
        }
    }
}

