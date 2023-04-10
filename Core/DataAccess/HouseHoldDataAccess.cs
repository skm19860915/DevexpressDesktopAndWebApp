using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using System;

namespace BlitzerCore.DataAccess
{
    public class HouseHoldDataAccess
    {
        const string ClassName = "HouseHoldDataAccess";
        IDbContext DbContext { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<HouseHold> Table { get; set; }

        public HouseHoldDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.HouseHolds;
        }

        public HouseHold Get(int aID)
        {
            return Table
                .FirstOrDefault(x => x.Id == aID);
        }
        public HouseHold Get(Contact aContact)
        {
            return DbContext.AppUsers
                .Include(x=>x.HouseHold)
                .FirstOrDefault(x => x.Id.Trim().ToUpper() == aContact.Id.Trim().ToUpper()).HouseHold;
        }

        public List<Contact> GetMembers ( int aHouseHoldID )
        {
            return DbContext.AppUsers.Where(x => x.HouseHoldId == aHouseHoldID).ToList();
        }

        public List<Contact> GetMembers(HouseHold aHouseHold)
        {
            if (aHouseHold == null)
                return new List<Contact>();

            var lOutput = DbContext.AppUsers
                .Include(x=>x.PhoneNumbers)
                .Include(x=>x.Emails)
                .Where(x => x.HouseHoldId == aHouseHold.Id);
            if (lOutput != null && lOutput.Count() > 0 )
                return lOutput.ToList();
            else
                return new List<Contact>();
        }

        public List<HouseHold> GetAll()
        {
            return Table.ToList();
        }

        public int Save(HouseHold aHouseHold)
        {
            string FuncName = ClassName + "Save ";
            if (aHouseHold.Id > 0)
                Table.Update(aHouseHold);
            else
                Table.Add(aHouseHold);

            try
            {
                var lCnt = DbContext.SaveChanges();
                Logger.LogInfo(FuncName + $"Updated {lCnt} household records");
            } catch ( Exception e)
            {
                Logger.LogException(FuncName + $" Failed to save household [{aHouseHold.Id}]", e);
            }

            return 0;
        }

        internal int Delete(int aHouseHoldId)
        {
            string FuncName = ClassName + "Delete ";
            if (aHouseHoldId == 0)
                return 0;

            var lHouseHold = Get(aHouseHoldId);
            Table.Remove(lHouseHold);

            try
            {
                var lCnt = DbContext.SaveChanges();
                Logger.LogInfo(FuncName + $"Updated {lCnt} household records");
            }
            catch (Exception e)
            {
                Logger.LogException(FuncName + $"Failed to delete household [{aHouseHoldId}]", e);
            }

            return 0;
        }
    }
}
