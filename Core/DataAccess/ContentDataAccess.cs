using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class ContentDataAccess
    {
        IDbContext mContext = null;

        public Microsoft.EntityFrameworkCore.DbSet<Content> Table { get; set; }

        public ContentDataAccess(IDbContext aContext)
        {
            mContext = aContext;
            Table = mContext.Contents;
        }

        public Content Get(int aID)
        {
            var lData = Table
                .Include(x => x.Paragraphs)
                .Include(x => x.Photo)
                .Include(x => x.Video)
                .FirstOrDefault(x => x.Id == aID);
            return lData;

        }

        public int Save(Content aContent)
        {
            if (aContent.Id == 0)
                Table.Add(aContent);
            else
                Table.Update(aContent);

            return mContext.SaveChanges();
        }

        public BlitzerCore.Models.UI.Page GetParent(Content aContent)
        {
            if (aContent == null)
                return null;

            return mContext.Pages
                .Include(x => x.HeaderImage)
                .ThenInclude (x1 => x1.Media)
                .ThenInclude (x2 => x2.Size1600x1200)
                .FirstOrDefault(x => x.ContentID == aContent.Id);
        }
    }
}

