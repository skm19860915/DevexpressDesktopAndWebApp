using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BlitzerCore.DataAccess;
using NUnitTests.Helpers;
using BlitzerCore.Business.AIFilters;
using WebApp.DataServices;

namespace NUnitTests.AIFilters
{
    public class AITestFilter : BlitzerCore.Models.AIFilter
    {
        public override string Name { get { return "Hotel Room"; } }
        public override string Description { get; set; }
        public override int AIFilterID { get { return 978; } }
        public override IEnumerable<QuoteRequestResort> Apply(IEnumerable<QuoteRequestResort> aInput)
        {
            return aInput;
        }
        public override IEnumerable<FlightItinerary> Apply(IEnumerable<FlightItinerary> aInput)
        {
            return aInput;
        }
    }

    class AIFilter
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
                DataLake.Init(mContext);
            }
        }

        [Test]
        public void RegisterAIFilters()
        {
            AIFilterRegistry.UpdateDataBaseWithAIFilters(mContext, AIFilterRegistry.RegisterFilters());
            foreach (var lFilter in mContext.AIFilters)
            {
                var lTestFilter = new FilterDataAccess(mContext).GetAIFilter(lFilter.AIFilterID);
                Assert.IsNotNull(lTestFilter);
                Assert.AreEqual(1, mContext.AIFilters.Count(x=>x.AIFilterID == lFilter.AIFilterID));
                Assert.AreEqual(1, mContext.AIFilters.Count(x => x.Name == lFilter.Name));
            }
        }
    }
}
