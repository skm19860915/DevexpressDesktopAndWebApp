using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using System.Security.Policy;
using Microsoft.Exchange.WebServices.Data;
using WebApp.Controllers;
using BlitzerCore.DataAccess;

namespace NUnitTests.DataAccess
{
    public class QuoteDataAccess
    {
        RepositoryContext mContext = null;
        DbContextOptions<WebApp.DataServices.RepositoryContext> mDBOptions = null;

        [SetUp]
        public void Initialize()
        {
            CreateInMemoryContext();
        }

        public RepositoryContext CreateInMemoryContext()
        {
            mDBOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            mContext = new RepositoryContext(mDBOptions);
            if (mContext != null)
            {
                mContext.Database.EnsureDeleted();
                mContext.Database.EnsureCreated();
            }

            return mContext;
        }
    }
}
