using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class TileBusiness
    {
        public IDbContext DbContext { get; set; }

        public TileBusiness(IDbContext aContext)
        {
            DbContext = aContext;
        }
        public int Save(Tile aTile )
        {
            return new TileDataAccess(DbContext).Save(aTile);
        }
    }
}
