using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using WebApp.Controllers;
using NUnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using NUnitTests.Bots;
using BlitzerCore.WebBots;
using BlitzerCore.UIHelpers;
using BlitzerCore.Utilities;
using BlitzerCore.Business;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;

namespace NUnitTests.Controllers
{
    public class Company
    {
        RepositoryContext mContext = null;
        DbContextOptions<RepositoryContext> mDBOptions = null;

        [SetUp]
        public void CreateInMemoryContext()
        {
            mDBOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            mContext = new RepositoryContext(mDBOptions);
            if (mContext != null)
            {
                mContext.Database.EnsureDeleted();
                mContext.Database.EnsureCreated();
                DataLake.Init(mContext);
            }
        }

        [Test]
        public void CreateCompany()
        {
            string lName = "Hotel Test";
            double lRating = 3.5;
            string lWebSite = "http://www.hands.com";
            int lBusinessType = 3;
            var lUICompany = new UICompany()
                {BusinessTypeID = lBusinessType, Name = lName, Rating = lRating, WebSite = lWebSite};

            new CompanyBusiness(mContext).Save(lUICompany,
                new AgentDataAccess(mContext).Get(DataLake.GetAgents()[0].Id));

            var lHotel = new HotelBusiness(mContext).GetAll().First();

            Assert.IsNotNull( lHotel as Hotel);
            Assert.AreEqual(lWebSite, lHotel.Website);
            Assert.AreEqual(lName, lHotel.Name);
            Assert.AreEqual(lRating, lHotel.Rating);
        }
    }
}
