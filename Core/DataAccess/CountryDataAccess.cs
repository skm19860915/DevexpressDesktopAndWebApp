using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlitzerCore.DataAccess
{
    public class CountryDataAccess
    {
        IDbContext mContext = null;

        public Microsoft.EntityFrameworkCore.DbSet<Country> Table { get; set; }

        public CountryDataAccess(IDbContext aContext)
        {
            mContext = aContext;
            Table = mContext.Countries;
        }

        public Country Get(int aID)
        {
            return Table
                .FirstOrDefault(x => x.Id == aID);
        }

        public List<Country> GetAll()
        {
            return Table.ToList();
        }

        public int Save(Country aPage)
        {
            if (aPage.Id > 0)
                Table.Update(aPage);
            else
                Table.Add(aPage);

            return mContext.SaveChanges();
        }
    }
}

