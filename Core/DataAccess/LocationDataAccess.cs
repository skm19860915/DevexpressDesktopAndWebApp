using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    public class LocationDataAccess
    {
        IDbContext mContext = null;

        public LocationDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public Location Save(Location aLocation)
        {
            mContext.Locations.Add(aLocation);
            mContext.SaveChangesAsync();

            return aLocation;
        }
        public IEnumerable<Location> GetAll()
        {
            return mContext.Locations;

        }
        public Location Get(int aID)
        {
            return mContext.Locations.Where(x => x.LocationID == aID).FirstOrDefault();

        }

        public Location Add(int aParentID, Location aNewLocation)
        {
            aNewLocation.ParentID = aParentID;
            return Save(aNewLocation);
        }
    }
}
