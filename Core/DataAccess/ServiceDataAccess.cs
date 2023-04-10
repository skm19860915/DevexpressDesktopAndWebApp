using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    public class ServiceDataAccess
    {
        IDbContext mContext = null;

        public ServiceDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public Service Save(Service aService)
        {
            mContext.Services.Add(aService);
            mContext.SaveChangesAsync();

            return aService;
        }
        public IEnumerable<Service> GetAll()
        {
            return mContext.Services;

        }
        public List<Service> Get(int? aID)
        {
            if (aID == null)
                return mContext.Services.Where(x => x.ParentID == null).ToList();
            else
                return mContext.Services.Where(x => x.ParentID == aID.Value).ToList();
        }
    }
}
