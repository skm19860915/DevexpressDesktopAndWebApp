using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class TileDataAccess
    {
        IDbContext mContext = null;

        public Microsoft.EntityFrameworkCore.DbSet<Tile> Table { get; set; }

        public TileDataAccess(IDbContext aContext)
        {
            mContext = aContext;
            Table = mContext.Tiles;
        }

        public Tile Get(int aID)
        {
            return Table
                .Include(x => x.Media).ThenInclude(y=>y.ThumbNail)
                .FirstOrDefault(x => x.Id == aID);

        }

        public int Save(Tile aOperator)
        {
            if (aOperator.Id == 0)
                mContext.Tiles.Add(aOperator);
            else
                mContext.Tiles.Update(aOperator);
            return mContext.SaveChanges();
        }



    }
}
