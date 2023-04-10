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

namespace NUnitTests.Business
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

    public class WebFormMock : IFormFile
    {
        const string mFileName = @"C:\Users\redop\source\repos\Blitzer\Data\TestMovie.mp4";
        string IFormFile.ContentDisposition => throw new System.NotImplementedException();

        string IFormFile.ContentType => throw new System.NotImplementedException();

        string IFormFile.FileName => mFileName;

        IHeaderDictionary IFormFile.Headers => throw new System.NotImplementedException();

        long IFormFile.Length => throw new System.NotImplementedException();

        string IFormFile.Name => throw new System.NotImplementedException();

        void IFormFile.CopyTo(Stream target)
        {
            using (FileStream source = File.Open(mFileName, FileMode.Open))
            {
                source.CopyTo(target);
            }
        }

        System.Threading.Tasks.Task IFormFile.CopyToAsync(Stream target, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        Stream IFormFile.OpenReadStream()
        {
            throw new System.NotImplementedException();
        }
    }

    public class MediaBizUT
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
        public void SaveVideo()
        {
            //IWebHostEnvironment lWebHostMock = new WebHostMock() { WebRootPath = @"D:\temp" };
            //IFormFile lMovie = new WebFormMock();
            //IConfiguration lConfigMock = new ConfigMock();
            //var lMediaBiz = new MediaBusiness(mContext, lConfigMock);

            //BlitzerCore.Models.UI.Media lMedia = new BlitzerCore.Models.UI.Media() { Title = "Temp Media" };
            //lMediaBiz.Save(lMedia);


            //lMedia.TempNewFile = lMovie;
            //lMedia.TempNewFileFormat = BlitzerCore.Models.UI.MediaFormats.MPEG;
            //lMediaBiz.Save(lMedia);
        }
    }
}
