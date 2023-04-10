using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class HouseHoldBusiness
    {
        const string ClassName = "HouseHoldBusiness::";
        IDbContext DbContext { get; set; }

        public HouseHoldBusiness(IDbContext aContext)
        {
            DbContext = aContext;
        }

        public HouseHold Get(int aHouseHoldID)
        {
            return new HouseHoldDataAccess(DbContext).Get(aHouseHoldID);
        }

        public HouseHold Get ( Contact aContact)
        {
            string FuncName = $"{ClassName}::Get(contact={aContact.Id})";
            if (aContact == null)
                return null;

            HouseHold lOutput = new HouseHoldDataAccess(DbContext).Get(aContact);
            if (lOutput != null)
            {
                var lMembers = GetMembers(lOutput);
                Logger.LogInfo($"{FuncName} - MemberCount = {lOutput.Members.Count} vs {lMembers.Count}");
                if (lMembers.Count != lOutput.Members.Count())
                    lOutput.Members = lMembers;
                return lOutput;
            }

            lOutput = new HouseHold() { Name = aContact.Last };
            Save(lOutput);
            lOutput.AddMember(aContact);
            new ContactDataAccess(DbContext).Save(aContact);
            return lOutput;
        }

        public int Save(HouseHold aHouseHold)
        {
            var lResults = new HouseHoldDataAccess(DbContext).Save(aHouseHold);
            return lResults;
        }

        public int AddMember ( HouseHold aHouseHold, Contact aContact)
        {
            aHouseHold.AddMember(aContact);
            return Save(aHouseHold);
        }
        public int RemoveMember(HouseHold aHouseHold, Contact aContact)
        {
            aHouseHold.RemoveMember(aContact);
            aContact.HouseHoldId = null;
            return Save(aHouseHold);
        }

        public List<Contact> GetMembers(HouseHold lHouseHold)
        {
            return new HouseHoldDataAccess(DbContext).GetMembers(lHouseHold);
        }
    }
}
