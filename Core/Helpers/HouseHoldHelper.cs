using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;

namespace BlitzerCore.Helpers
{
    public class HouseHoldHelper
    {
        static string ClassName = "HouseHoldHelper::";
        public static List<HouseHoldMember> GetMembers(IDbContext aContext, Contact aPrimary)
        {
            string FuncName = ClassName + $"GetMember (DbContext, Contact = {aPrimary.Id}) - ";
            var lOutput = new List<HouseHoldMember>();
            if ( aPrimary.HouseHold == null )
            {
                Logger.LogWarning(FuncName + $"There is no household for {aPrimary.Name}");
            }

            var lMembers = new HouseHoldDataAccess(aContext).GetMembers(aPrimary.HouseHold);
            foreach (var lMember in lMembers)
            {
                HouseHoldMember lHMember = new HouseHoldMember() { MemberId = lMember.Id, Member = lMember, HouseHoldID = aPrimary.HouseHold.Id };
                lOutput.Add(lHMember);

                if (lHMember.Relationship != RelationShips.Unknown)
                    continue;

                if (lMember.Gender == Gender.Male && lMember.MaritalStatus == MaritalStatuses.Married)
                    lHMember.Relationship = RelationShips.Husband;
                else if (lMember.Gender == Gender.Male && lMember.MaritalStatus == MaritalStatuses.Relationship)
                    lHMember.Relationship = RelationShips.Boyfriend;
                else if (lMember.Gender == Gender.Female && lMember.MaritalStatus == MaritalStatuses.Relationship)
                    lHMember.Relationship = RelationShips.Girlfriend;
                else if (lMember.Gender == Gender.Female && lMember.MaritalStatus == MaritalStatuses.Married)
                    lHMember.Relationship = RelationShips.Wife;
                else if (lMember.Gender == Gender.Male && ContactBusiness.Age(lMember) > 21 &&
                    (lMember.MaritalStatus == MaritalStatuses.Divoiced
                    || lMember.MaritalStatus == MaritalStatuses.Seperated
                    || lMember.MaritalStatus == MaritalStatuses.Single))
                    lHMember.Relationship = RelationShips.Father;
                else if (lMember.Gender == Gender.Female && ContactBusiness.Age(lMember) > 21 &&
                    (lMember.MaritalStatus == MaritalStatuses.Divoiced
                    || lMember.MaritalStatus == MaritalStatuses.Seperated
                    || lMember.MaritalStatus == MaritalStatuses.Single))
                    lHMember.Relationship = RelationShips.Mother;
                else if (lMember.Gender == Gender.Male && ContactBusiness.Age(lMember) < 21)
                    lHMember.Relationship = RelationShips.Son;
                else if (lMember.Gender == Gender.Female && ContactBusiness.Age(lMember) < 21)
                    lHMember.Relationship = RelationShips.Daughter;
                else
                    lHMember.Relationship = RelationShips.Unknown;
            }
            return lOutput;
        }
    }
}
