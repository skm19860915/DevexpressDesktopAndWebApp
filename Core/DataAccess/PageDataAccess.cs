using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class PageDataAccess 
    {
        IDbContext mContext = null;

        public Microsoft.EntityFrameworkCore.DbSet<UIResortPage> Table { get; set; }

        public PageDataAccess(IDbContext aContext)
        {
            mContext = aContext;
            Table = mContext.ResortPages;
        }
        public Page Get ( int aId )
        {
            return mContext.Pages
                .Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1600x1200)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080)
                .Include(x=>x.CenterContent)
                .FirstOrDefault(x => x.Id == aId);
        }
        public IQueryable<Page> GetPage(int aPageID, int aSize = 20)
        {
            return Table
                //.Include(x=>x.Author)
                //.Include(x=>x.HeaderImage).ThenInclude(y=>y.Graphics)
                .Skip((aPageID - 1) * aSize).Take(aSize);
        }
        internal List<PageType> GetPageTypes()
        {
            return mContext.PageTypes.ToList();
        }

        public UIResortPage GetResort(Hotel aHotel)
        {
            return Table
                .Include(x=>x.Comparables)
                .Include(x=>x.CenterContent).ThenInclude(y=>y.Paragraphs)
                .Include(x => x.CenterContent).ThenInclude(y => y.Video)
                .Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                .Include(x=>x.HeaderImage).ThenInclude(y=>y.Media).ThenInclude(m=>m.Size1600x1200)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080)
                .Include(x=>x.LeftPanel).ThenInclude(y=>y.Comparables).ThenInclude(m=>m.Media)
                .Include(x=>x.RightPanel).ThenInclude(y=>y.Tiles).ThenInclude(m=>m.Media)
                .FirstOrDefault(x => x.Id == aHotel.PageId);
        }

        public UIResortPage GetResort(int aHotelPageId)
        {
            var lOutput = Table
                .Include(x => x.Comparables)
                .Include(x => x.CenterContent).ThenInclude(y => y.Paragraphs)
                .Include(x => x.CenterContent).ThenInclude(y => y.Video)
                .Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1600x1200)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080)
                .Include(x => x.LeftPanel).ThenInclude(y => y.Comparables).ThenInclude(m => m.Media)
                .Include(x => x.RightPanel).ThenInclude(y => y.Tiles).ThenInclude(m => m.Media)
                .FirstOrDefault(x => x.Id == aHotelPageId);

            return lOutput;
        }

        public UIResortPage GetResort(Page aPage)
        {
            return Table
                .Include(x => x.Comparables)
                .Include(x => x.CenterContent).ThenInclude(y => y.Paragraphs)
                .Include(x => x.CenterContent).ThenInclude(y => y.Video)
                .Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1600x1200)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080)
                .Include(x => x.LeftPanel).ThenInclude(y => y.Comparables).ThenInclude(m => m.Media)
                .Include(x => x.RightPanel).ThenInclude(y => y.Tiles).ThenInclude(m => m.Media)
                .FirstOrDefault(x => x.Id == aPage.Id);
        }

        public List<Page> GetAll()
        {
            return mContext.Pages.ToList();
        }

        public List<UIResortPage> GetResortsByCountry(int aCountryID)
        {
            return Table
                .Include(x => x.Comparables)
                .Include(x => x.CenterContent).ThenInclude(y => y.Paragraphs)
                .Include(x => x.CenterContent).ThenInclude(y => y.Video)
                .Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1600x1200)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080)
                .Include(x => x.LeftPanel).ThenInclude(y => y.Comparables).ThenInclude(m => m.Media)
                .Include(x => x.RightPanel).ThenInclude(y => y.Tiles).ThenInclude(m => m.Media)
                .Where(x => x.Published == true 
                && x.CountryId == aCountryID).ToList();
        }

        public UICountry GetCountry(int aID)
        {
            return mContext.UICountries
                .Include(x => x.CenterContent).ThenInclude(y => y.Paragraphs)
                .Include(x => x.CenterContent).ThenInclude(y => y.Video)
                .Include(x => x.MainImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size560x460)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1600x1200)
                .Include(x => x.HeaderImage).ThenInclude(y => y.Media).ThenInclude(m => m.Size1920x1080)
                .FirstOrDefault(x => x.Id == aID);
        }

        public int Save(UIResortPage aPage)
        {
            if (aPage.Id == 0)
                Table.Add(aPage);
            else
                Table.Update(aPage);

            return mContext.SaveChanges();
        }
        public int Save(Page aPage)
        {
            if (aPage.Id == 0)
                mContext.Pages.Add(aPage);
            else
                mContext.Pages.Update(aPage);

            return mContext.SaveChanges();
        }
    }
}

