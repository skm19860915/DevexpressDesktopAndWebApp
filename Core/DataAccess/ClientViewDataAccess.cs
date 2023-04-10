using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BlitzerCore.DataAccess
{
    public class ClientViewDataAccess
    {
        const string ClassName = "ClientViewDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<ClientView> Table { get; set; }

        public ClientViewDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.ClientViews;

        }

        public int Save(ClientView aClientView)
        {
            string FuncName = $"{ClassName}Save (ClientView = {aClientView.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aClientView.Id > 0)
                {
                    Table.Update(aClientView);
                    lAction = "Updated";
                }
                else
                    Table.Add(aClientView);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} ClientView records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save ClientView", e);
                throw e;
            }
            return lCount;

        }

        public ClientView Get(int aClientViewID)
        {
            return Table
                .FirstOrDefault(x => x.Id == aClientViewID);
        }
    }
}
