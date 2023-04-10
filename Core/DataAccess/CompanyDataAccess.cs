using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;


namespace BlitzerCore.DataAccess
{
    public class CompanyDataAccess
    {
        const string ClassName = "CompanyDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<TourOperator> Table { get; set; }

        public CompanyDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.TourOperators;

        }

        public int Save(TourOperator aCompany)
        {
            string FuncName = $"{ClassName}Save (Company = {aCompany.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aCompany.Id > 0)
                {
                    Table.Update(aCompany);
                    lAction = "Updated";
                }
                else
                    Table.Add(aCompany);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Company records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Company", e);
                throw e;
            }
            return lCount;

        }

        public int Save(Company aCompany, bool aCommit = true)
        {
            string FuncName = $"{ClassName}Save (Company = {aCompany.Id}";
            string lAction = "Added";
            var lCount = 0;

            if (aCompany.Id > 0)
            {
                DbContext.Companies.Update(aCompany);
                lAction = "Updated";
            }
            else
                DbContext.Companies.Add(aCompany);

            if (aCommit)
                lCount = DbContext.SaveChanges();

            Logger.LogInfo($"{FuncName} {lAction} {lCount} Company records");

            return lCount;

        }

        public Company Get(int aCompanyID)
        {
            if (Table.Find(aCompanyID) != null)
                return Table
                    .Include(x => x.Emails)
                    .Include(x => x.PhoneNumbers)
                    .Include(x => x.Bookings).ThenInclude(y => y.Trip).ThenInclude(y1=>y1.Agent)
                    .Include(x => x.Bookings).ThenInclude(y => y.Supplier)
                    .Include(x => x.BusinessType)
                    .Include(x => x.Contacts)
                    .FirstOrDefault(x => x.Id == aCompanyID);
            else
                return DbContext.Companies
                    .Include(x => x.Emails)
                    .Include(x => x.PhoneNumbers)
                    .Include(x => x.BusinessType)
                    .Include(x => x.Contacts)
                    .FirstOrDefault(x => x.Id == aCompanyID);
        }

        public Company Get(string aCompanyName)
        {
            return DbContext.Companies
                .Include(x => x.Emails)
                .Include(x => x.PhoneNumbers)
                .Include(x => x.BusinessType)
                .Include(x => x.Contacts)
                .FirstOrDefault(x => x.Name == aCompanyName);
        }

        public List<Company> GetAll()
        {
            return DbContext.Companies
                   .Include(x => x.Emails)
                   .Include(x => x.PhoneNumbers)
                   .Include(x => x.BusinessType)
                   .Include(x => x.Contacts).ToList();
        }

        public IEnumerable<TourOperator> GetTourOperators()
        {
            return DbContext.TourOperators
                .Include(x => x.Emails)
                .Include(x => x.PhoneNumbers)
                .Include(x => x.BusinessType)
                .OrderBy(y => y.Name);
        }

        public IEnumerable<TourOperator> GetTourOperators(Contact aContact)
        {
            return DbContext.TourOperators
                .Include(x => x.Emails)
                .Include(x => x.PhoneNumbers)
                .Include(x => x.BusinessType)
                .Where(x => x.Visiblity == Visibility.Public || x.OwnerId == aContact.Id).OrderBy(y => y.Name);
        }

        public IEnumerable<Company> GetSuppliers(Contact aContact)
        {
            return DbContext.Companies
                .Include(x => x.Emails)
                .Include(x => x.PhoneNumbers)
                .Include(x => x.BusinessType)
                .Where(x => (x.Visiblity == Visibility.Public || x.OwnerId == aContact.Id)
                    && x.BusinessType.Commissionable == true)
                .OrderBy(y => y.Name);
        }

        public IEnumerable<Company> GetAll(Contact aContact)
        {
            return DbContext.Companies
                .Include(x => x.Emails)
                .Include(x => x.PhoneNumbers)
                .Include(x => x.BusinessType)
                .Where(x => x.Visiblity == Visibility.Public || x.OwnerId == aContact.Id).OrderBy(y => y.Name);
        }
    }
}
