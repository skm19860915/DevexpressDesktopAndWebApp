using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class LocationBusiness
    {
        IDbContext mContext = null;

        public LocationBusiness(IDbContext aContext)
        {
            mContext = aContext;
        }

        public Location Get(int Id)
        {
            return new LocationDataAccess(mContext).Get(Id);
        }

        public IEnumerable<Location> GetAll()
        {
            return new LocationDataAccess(mContext).GetAll();
        }

        public Location Save(Location aLead)
        {
            return new LocationDataAccess(mContext).Save(aLead);
        }

        Location Add(int aParentID, Location aNewLocation)
        {
            return new LocationDataAccess(mContext).Add(aParentID, aNewLocation);
        }
    }
}
