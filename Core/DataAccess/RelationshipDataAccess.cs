using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    public class RelationshipDataAccess
    {
        IDbContext mContext = null;
        public RelationshipDataAccess(IDbContext aContext)
        {
            mContext = aContext;
        }

        public List<Relationship> GetRelationships()
        {
            if (mContext.RelationshipMaps == null)
                return new List<Relationship>();

            return mContext.Relationships.ToList();
        }
    }
}
