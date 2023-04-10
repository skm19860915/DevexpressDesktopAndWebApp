using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using System.IO;
using WebApp.Controllers;
using BlitzerCore.DataAccess;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnitTests.Helpers;
using Microsoft.Extensions.Configuration;

namespace NUnitTests.Business
{
    public class Accommodation
    {
        const int REQUEST_ID = 100;
        RepositoryContext mContext = null;
        DbContextOptions<WebApp.DataServices.RepositoryContext> mDBOptions = null;

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

                DataLake.LoadHotels(mContext);
                DataLake.LoadRoomTypes(mContext);
                DataLake.LoadAgent(mContext);
                DataLake.LoadContacts(mContext);
                DataLake.LoadEmailTypes(mContext);
                DataLake.LoadTourOps(mContext);
                DataLake.LoadStages(mContext);
                DataLake.LoadAirPorts(mContext);
            }
        }
    }
}
