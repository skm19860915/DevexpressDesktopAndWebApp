using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Models.UI;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.DataAccess;
using WebApp.DataServices;
using WebApp.Controllers;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Utilities;

namespace NUnitTests.Controllers
{
    public class BlockControllerUT
    {
        RepositoryContext mContext = null;
        DbContextOptions<RepositoryContext> mDBOptions = null;

        [SetUp]
        public void CreateSqlServerContext()
        {
            Logger.Init("BlitzerUnitTest");
            Logger.InitConsummer();
            Logger.ConnectionFactory = new ConcreteFactory();

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("UnitTesting.json")
                .AddEnvironmentVariables();

            var Configuration = configurationBuilder.Build();

            string lDB = Configuration["ConnectionString:TEST"];
            Logger.ConnectionString = lDB;

            mDBOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(lDB)
                .Options;

            mContext = new RepositoryContext(mDBOptions);
            if (mContext != null)
            {
                //mContext.Database.EnsureDeleted();
                //mContext.Database.EnsureCreated();
                //DataLake.Init(mContext);
            }
        }
    }
}
