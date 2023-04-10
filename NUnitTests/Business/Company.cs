using System;
using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using NUnitTests.Helpers;


namespace NUnitTests.Business
{
    public class Company
    {
        RepositoryContext mContext = null;
        DbContextOptions<WebApp.DataServices.RepositoryContext> mDBOptions = null;

        [SetUp]
        public void Setup()
        {
            CreateInMemoryContext();
        }

        public RepositoryContext CreateInMemoryContext()
        {
            mDBOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            mContext = new RepositoryContext(mDBOptions);
            if (mContext != null)
            {
                mContext.Database.EnsureDeleted();
                mContext.Database.EnsureCreated();
                Helpers.DataLake.Init(mContext);
            }

            return mContext;
        }

        [Test]
        public void CreateCruiseLine()
        {
            var lName = "Test Company";
            var lCompany = new BlitzerCore.Models.Company() { Name = lName, BusinessTypeID = 4 };
            var lAgent = mContext.Agents.First();

            var lNewCompany = new CompanyBusiness(mContext).Save(lCompany, lAgent);

            Assert.AreEqual(1, 1);
            Assert.AreEqual(lName, lCompany.Name);
        }
    }
}
