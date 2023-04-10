using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business
{
    public class CompanyBusiness
    {
        public const string ClassName = "CompanyBusiness";
        protected IDbContext DbContext { get; set; }
        private CompanyDataAccess DataAccess { get; set; }
        public CompanyBusiness(IDbContext mContext)
        {
            this.DbContext = mContext;
            DataAccess = new CompanyDataAccess(mContext);
        }

        public virtual Company Get(int? aID)
        {
            if (aID == null)
                return null;

            return DataAccess.Get(aID.Value);
        }

        public Company Get(int aID)
        {
            var lComp = DataAccess.Get(aID);

            return lComp;
        }

        public List<Agent> GetAgents(Company aCompany)
        {
            return new AgentDataAccess(DbContext).GetAgents(aCompany);
        }

        public Company Get(string aName)
        {
            var lComp = DataAccess.Get(aName);

            return lComp;
        }


        public int Save(UICompany aCompany, Agent aAgent)
        {
            switch (aCompany.BusinessTypeID)
            {
                //this is all fucked up.  Need to have hotel business create they damn hotel from the uic ompany
                case 3:
                    var lHotel = HotelUIHelper.Convert(DbContext, aCompany);
                    return Save(lHotel, aAgent);
                case 4:
                    var lCruiseLine = CruiseLineUIHelper.Convert(DbContext, aCompany);
                    return Save(lCruiseLine, aAgent);
                default:
                    var lBase = CompanyUIHelper.Convert(DbContext, aCompany, aAgent);
                    return Save(lBase, aAgent);
            }
        }

        public int Save(Hotel aHotel, Agent aAgent)
        {
            string FuncName = $"{ClassName} -Save (Hotel Name = {aHotel.Name}) ";
            try
            {
                var lCnt = new HotelBusiness(DbContext).Save(aHotel, aAgent);
                return lCnt;
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Hotel", e);
                throw e;
            }
        }

        public int Save(Company aCompany, Agent aAgent, bool aCommit = true)
        {
            string FuncName = $"{ClassName} -Save (Company Name = {aCompany.Name}) ";
            try
            {
                UpdateTracking(aCompany, aAgent);
                var lCnt = DataAccess.Save(aCompany, aCommit);
                return lCnt;
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Company", e);
                throw e;
            }
        }
        private static void UpdateTracking(Company aCompany, Agent aAgent)
        {

            if (aCompany.CreatedById == null || aCompany.CreatedById == "")
            {
                aCompany.CreatedById = aAgent.Id;
                aCompany.CreatedOn = DateTime.Now;
            }
            aCompany.UpdatedById = aAgent.Id;
            aCompany.UpdatedOn = DateTime.Now;
        }

        public void Init(Agent aAgent, Company aCompany)
        {
            aCompany.CreatedById = aAgent.Id;
            aCompany.CreatedOn = DateTime.Now;
            aCompany.UpdatedById = aAgent.Id;
            aCompany.UpdatedOn = DateTime.Now;
            aCompany.OwnerId = aAgent.Id;
        }

        public Company Create(Agent aAgent, UICompany aInput)
        {
            Company lOutput = new Company();
            if (aInput.TourOperator == true)
                lOutput = new TourOperator();
            else
                lOutput = new Company();
            Init(aAgent, lOutput);
            Populate(lOutput, aInput);
            return lOutput;
        }

        public static string Initials(Company aCompany)
        {
            if (aCompany == null)
                return "";

            var lWords = aCompany.Name.Split(' ');
            var lOutput = "";
            if (lWords.Count() == 0)
                return aCompany.Name[0] + "";

            foreach (var lWord in lWords)
                lOutput += lWord[0];

            return lOutput;
        }

        protected void Populate(Company aOutput, UICompany aInput)
        {
            bool lFromDetailScreen = aInput.Name == null && aInput.Address1 == null && aInput.Address2 == null && aInput.WebSite == null;
            // Detail Screen only has the memo field
            if (lFromDetailScreen == false)
            {
                aOutput.Name = aInput.Name;
                aOutput.Street = aInput.Address1;
                aOutput.Street2 = aInput.Address2;
                aOutput.City = aInput.City;
                aOutput.Id = aInput.Id;
                aOutput.BusinessTypeID = aInput.BusinessTypeID;
                aOutput.Visiblity = aInput.Visibility;
                aOutput.State = aInput.State;
                aOutput.ZipCode = aInput.ZipCode;
                aOutput.Website = aInput.WebSite;
                aOutput.BusinessTypeID = aInput.BusinessTypeID;
            }

            aOutput.Memo = aInput.Memo;
        }

    }
}
