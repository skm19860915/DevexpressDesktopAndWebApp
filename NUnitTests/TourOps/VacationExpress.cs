using System;
using System.Collections.Generic;
using System.Text;
using NUnitTests.Business;
using NUnit.Framework;
using System.IO;
using Microsoft.EntityFrameworkCore;
using WebApp.DataServices;
using NUnitTests.Helpers;

namespace NUnitTests.TourOps
{
    public class VacationExpress
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

        private string GetVEScreenText()
        {
            //string lFilePath = @".\data\VEQuote.txt";
            string lFilePath = @".\data\AtelierVEQuote.txt";
            Directory.SetCurrentDirectory(@"..\..\..\..");
            var lCWD = Directory.GetCurrentDirectory();
            if (File.Exists(lFilePath) == true)
                return File.ReadAllText(lFilePath);
            else
                return "";
        }
    }
}
