using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Utilities;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Primitives;
using NUnitTests.Business;
using WebApp.Controllers;

namespace NUnitTests.Controllers
{
    class MediaControllerUT
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
