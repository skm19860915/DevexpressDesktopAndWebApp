using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Primitives;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using WebApp.DataServices;
using BlitzerCore.Utilities;

namespace NUnitTests.Utilities
{
    public class ConfigMock : IConfiguration
    {
        string IConfiguration.this[string key] { get => "DefaultEndpointsProtocol=https;AccountName=blitzerblobs;AccountKey=UBx2/MsaGHp90lfv5/g79TNjVnPuAaLgq05Mmom67IFqGIvoLfLnaTYzP6tNvu3fNpSmuTSOB93Zv6MQf9zisw==;EndpointSuffix=core.windows.net"; set => throw new System.NotImplementedException(); }

        IEnumerable<IConfigurationSection> IConfiguration.GetChildren()
        {
            throw new System.NotImplementedException();
        }

        IChangeToken IConfiguration.GetReloadToken()
        {
            throw new System.NotImplementedException();
        }

        IConfigurationSection IConfiguration.GetSection(string key)
        {
            throw new System.NotImplementedException();
        }
    }

    public class AzureStorageTest
    {
        RepositoryContext mContext = null;
        DbContextOptions<WebApp.DataServices.RepositoryContext> mDBOptions = null;

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
        }

        [Test]
        public void Upload()
        {
            //ConfigMock lConfig = new ConfigMock();
            //var lAzure = new AzureStorage("", mContext);
            //var lPic = @"D:\Eze2Travel\OneDrive - Eze2Travel\Pictures\1024x760\Test_21.jpg";
            //var lBlock = new BlockDataAccess(mContext).Get(100);
            //lAzure.UploadMediaToBlob(lPic, lBlock, MediaFormats.Size_560x460);
        }
    }
}
