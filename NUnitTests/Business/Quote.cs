using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using System.IO;
using WebApp.Controllers;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnitTests.Helpers;
using NUnitTests.ServiceStubs;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.UIHelpers;
using BlitzerCore.WebBots;
using BlitzerCore.Utilities;

namespace NUnitTests.Business
{
    class Quote
    {
        const string ClassName = "NUnitTests::Quote::";
        const int REQUEST_ID = 100;

        RepositoryContext mContext = null;
        DbContextOptions<RepositoryContext> mDBOptions = null;
        //string mUserID = "0654d61e-6df8-4e1c-83fd-77efe85b718e";

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
    }
}
