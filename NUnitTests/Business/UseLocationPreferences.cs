using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;

namespace NUnitTests.Marketing
{
    class UseLocationPreferences
    {
        DbContextOptions<RepositoryContext> mDBOptions = null;
        string mUserID = "0654d61e-6df8-4e1c-83fd-77efe85b718e";
        
        public RepositoryContext CreateInMemoryContext()
        {
            mDBOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            var context = new RepositoryContext(mDBOptions);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        [Test]
        public void AddLocations()
        {

            List<int> lLocations = new List<int>();
            lLocations.Add(2);
            lLocations.Add(5);
            lLocations.Add(99);

            using (var lContext = CreateInMemoryContext())
            {
                var lUserLocPrefBiz = new UserLocationPreferencesBusiness(lContext);
                lUserLocPrefBiz.Save(mUserID, lLocations);

                var lLocationIDs = lUserLocPrefBiz.Get(mUserID).Select(x => x.UserPreference).ToList();
                Assert.AreEqual(2, lLocationIDs[0]);
                Assert.AreEqual(5, lLocationIDs[1]);
                Assert.AreEqual(99, lLocationIDs[2]);

            }
        }
    }
}
