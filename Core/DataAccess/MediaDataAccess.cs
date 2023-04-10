using System;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlitzerCore.DataAccess
{
    public class MediaDataAccess
    {
        IDbContext mContext = null;

        public Microsoft.EntityFrameworkCore.DbSet<Media> Table { get; set; }

        public MediaDataAccess(IDbContext aContext)
        {
            mContext = aContext;
            Table = mContext.Medias;

        }

        public Media GetRO(int aID)
        {
            return Table
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == aID);

        }

        public List<SelectMedia> GetAllFlatten()
        {
            var lList = GetAll();
            var lOutput = new List<SelectMedia>();
            foreach (var lMed in lList)
                lOutput.Add(new SelectMedia(lMed));
            return lOutput;
        }

        public IQueryable<Media> GetAll()
        {
            return Table
                .Include(m => m.Size1920x1080)
                .Include(m => m.Size1600x1200)
                .Include(m => m.Size1024x640)
                .Include(m => m.Size560x460)
                .Include(m=>m.Category)
                .Include(m=>m.Country)
                .Include(m => m.ThumbNail)
                .Include(m => m.MPeg);
        }

        public Graphic GetGraphic(int id)
        {
            return mContext.Graphics.FirstOrDefault(x => x.ID == id);
        }

        public Media Get(int aID)
        {
            return Table
                .Include(m=>m.Size1920x1080)
                .Include(m=>m.Size1600x1200)
                .Include(m=>m.Size1024x640)
                .Include(m=>m.Size560x460)
                .Include(m => m.ThumbNail)
                .Include(m=>m.MPeg)
                .FirstOrDefault(x => x.Id == aID);

        }

        public int DeleteExistingMediaTypes(Media aMedia)
        {
            //throw new NotImplementedException();
            return 0;
        }

        public UIResortPage GetParent(Media aMedia)
        {
            return mContext.ResortPages.FirstOrDefault(x => x.HeaderImageID == aMedia.Id);
        }

        private int Delete(IEnumerable<Graphic> aGraphics )
        {
            mContext.Graphics.RemoveRange(aGraphics);
            return mContext.SaveChanges();
        }

        public int Save(Media aMedia)
        {
            if (aMedia.Id == 0)
                Table.Add(aMedia);
            else
                Table.Update(aMedia);

            return mContext.SaveChanges();
        }

        public List<Media> GetResortImagesByCategory(int aResortID, int aCategoryID)
        {
            return Table
                .Include(m => m.Size1920x1080)
                .Include(m => m.Size1600x1200)
                .Include(m => m.Size1024x640)
                .Include(m => m.Size560x460)
                .Include(m => m.ThumbNail)
                .Include(m => m.Category)
                .Include(m => m.Resort)
                .Where(x => x.PageID == aResortID && x.CategoryID == aCategoryID).ToList();
        }

        internal IEnumerable<Category> GetCategories()
        {
            return mContext.Category.ToList();
        }

        public int Save(Graphic aGraphic)
        {
            if (aGraphic.ID == 0)
                mContext.Graphics.Add(aGraphic);
            else
                mContext.Graphics.Update(aGraphic);

            return mContext.SaveChanges();
        }
    }
}
