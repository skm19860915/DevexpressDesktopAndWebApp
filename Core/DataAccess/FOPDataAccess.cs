using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;


namespace BlitzerCore.DataAccess
{
    class FOPDataAccess
    {
        const string ClassName = "FOPDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<FOP> Table { get; set; }

        public FOPDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.CreditCards;

        }

        public int Save(FOP aCard)
        {
            string FuncName = $"{ClassName}Save (Card = {aCard.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aCard.Id > 0)
                {
                    Table.Update(aCard);
                    lAction = "Updated";
                }
                else
                    Table.Add(aCard);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} card records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save card", e);
                throw e;
            }
            return lCount;

        }

        public FOP Get(int aCardID)
        {
            return Table
                .FirstOrDefault(x => x.Id == aCardID);
        }

        public IQueryable<FOP> GetCards(Contact aContact)
        {
            return Table
                .Where(x => x.OwnerID == aContact.Id);
        }
    }
}
