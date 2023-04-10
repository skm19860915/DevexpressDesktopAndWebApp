using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Helpers;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.UIHelpers;

namespace BlitzerCore.Business
{
    public class FOPBusiness
    {
        readonly static string ClassName = "OpportunityBusiness::";
        IDbContext DbContext { get; }

        public FOPBusiness(IDbContext aContext)
        {
            DbContext = aContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aOwnerID">The Traveler who owns the Credit Card</param>
        /// <param name="aUser">The travel agent who input it into the system</param>
        /// <returns></returns>
        public FOP Create(string aOwnerID, Contact aUser)
        {
            var lCard = new FOP();
            lCard.CreatedById = aUser.Id;
            lCard.CreatedOn = DateTime.Now;
            lCard.UpdatedById = aUser.Id;
            lCard.UpdatedOn = DateTime.Now;
            lCard.OwnerID = aOwnerID;
            return lCard;
        }

        private void UpdateTracking(FOP aCard, Contact aUser)
        {
            Logger.LogTracing(ClassName + "UpdateTracking - UpdateTracking");
            if (aCard.Id == 0)
            {
                aCard.CreatedById = aUser.Id;
                aCard.CreatedOn = DateTime.Now;
            }
            aCard.UpdatedById = aUser.Id;
            aCard.UpdatedOn = DateTime.Now;
        }
        public FOP Save(UIFOP aCard, Contact aAgent)
        {
            var lFOP = FOPUIHelper.Convert(DbContext, aCard);
            int lCnt = Save(lFOP, aAgent);
            return lFOP;
        }
        public FOP Get(int aCardId)
        {
            return new FOPDataAccess(DbContext).Get(aCardId);
        }

        public int Save(FOP aCard, Contact aAgent)
        {
            UpdateTracking(aCard, aAgent);
            return new FOPDataAccess(DbContext).Save(aCard);
        }

        public IQueryable<FOP> GetCards(Contact aContact)
        {
            return new FOPDataAccess(DbContext).GetCards(aContact);
        }
    }
}
