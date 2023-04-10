using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlitzerCore.DataAccess
{
    public class CountryPageDataAccess
    {
        IDbContext mContext = null;

        public Microsoft.EntityFrameworkCore.DbSet<UICountry> Table { get; set; }

        public CountryPageDataAccess(IDbContext aContext)
        {
            mContext = aContext;
            Table = mContext.UICountries;
        }

        public UICountry Get(int aID)
        {
            return Table
                .Include(x => x.CenterContent).ThenInclude(y => y.Paragraphs)
                .Include(x => x.CenterContent).ThenInclude(y => y.Video)
                .Include(x => x.Blocks).ThenInclude(y => y.Block).ThenInclude(t => t.Media).ThenInclude(u => u.ThumbNail)
                .Include(x => x.Blocks).ThenInclude(y => y.Block).ThenInclude(t => t.Media).ThenInclude(u => u.Size560x460)
                .Include(x => x.Blocks).ThenInclude(y => y.Block).ThenInclude(t => t.Media).ThenInclude(u => u.Size1600x1200)
                .Include(x => x.Blocks).ThenInclude(y => y.Block).ThenInclude(t => t.PageMap).ThenInclude(u => u.Page).ThenInclude(f => f.CenterContent)
                .Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1600x1200)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080)
                //.Where(x => x.SelectMany(x.Blocks)
                .FirstOrDefault(x => x.Id == aID);
        }

        public List<UICountry> GetAll()
        {
            return Table
                .Include(x => x.CenterContent).ThenInclude(y => y.Paragraphs)
                .Include(x => x.CenterContent).ThenInclude(y => y.Video)
                .Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1600x1200)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080)
                .Where(x => x.Published == true).ToList();
        }

        public int Save(UICountry aPage)
        {
            if (aPage.Id > 0)
                Table.Update(aPage);
            else
                Table.Add(aPage);

            return mContext.SaveChanges();
        }
    }
}

