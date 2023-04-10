using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class AirPortDataAccess
    {
        IDbContext mContext = null;

        public AirPortDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public AirPort Get(string aAirPortCode)
        {
            return mContext.AirPorts.Where(x => x.Code.ToUpper() == aAirPortCode.ToUpper()).FirstOrDefault();
        }

        public AirPort Get(int aAirPortId)
        {
            return mContext.AirPorts
                .Include(x=>x.Country)
                .Where(x => x.AirPortID == aAirPortId).FirstOrDefault();
        }

        public IEnumerable<AirPort> GetAll()
        {
            if (mContext == null) // Testing
                return new List<AirPort>();

            var lIDs = mContext.AirPorts.GroupBy(x => x.Name)
                .Select(g => new
                {
                    g.Key,
                    MinId = g.Min(x => x.AirPortID)
                }
                ).Select(x => x.MinId);

            return mContext.AirPorts.Where(x => lIDs.Contains(x.AirPortID)).OrderBy(x=>x.Name);
        }

        public int Save(AirPort aNewAirPort)
        {
            if (aNewAirPort.AirPortID == 0)
                mContext.Airports.Add(aNewAirPort);
            else
                mContext.Airports.Update(aNewAirPort);
            return mContext.SaveChanges();
        }
    }
}
