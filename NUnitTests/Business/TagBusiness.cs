using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using NUnitTests.Helpers;
using NUnit.Framework;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Models.UI;
using WebApp.DataServices;
using WebApp.Controllers;
using BlitzerCore.Utilities;

namespace NUnitTests.Business
{
    public class TagBusiness
    {
        RepositoryContext mContext = null;
        DbContextOptions<RepositoryContext> mDBOptions = null;

        [SetUp]
        public void CreateInMemoryContext()
        {
            Logger.Init("BlitzerUnitTest");
            Logger.InitConsummer();
            Logger.ConnectionFactory = new ConcreteFactory();

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("UnitTesting.json")
                .AddEnvironmentVariables();

            var Configuration = configurationBuilder.Build();

            string lDB = Configuration["ConnectionString:Dev"];
            Logger.ConnectionString = lDB;

            mDBOptions = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(lDB)
                .Options;

            mContext = new RepositoryContext(mDBOptions);
        }

        [Test]
        public void CreateTag()
        {
            var lTagBiz = new BlitzerCore.Business.TagBusiness(mContext, null);
            var lCategoryName = "Body Function";
            var lTagName = "Fart";



            var lTag = lTagBiz.CreateTag(lCategoryName, lTagName);
            Assert.IsNotNull(lTag);

            var lTags = lTagBiz.GetAll();
            Assert.AreEqual(1, lTags.Count);
            Assert.AreEqual(lTagName, lTags.First().Name);
            Assert.AreEqual(1, lTags.First().TagCategories.Count);
            Assert.AreEqual(lCategoryName, lTags.First().TagCategories.First().Name);
        }

        [Test]
        public void AddTagToContact()
        {
            var lTagBiz = new BlitzerCore.Business.TagBusiness(mContext, null);
            var lContacts = new ContactBusiness(mContext).GetAll();
            var lContact = lContacts.First();
            var lCategoryName = "Body Function";
            var lTagName = "Fart";

            var lTags = lTagBiz.GetTags(lContact);
            Assert.AreEqual(0, lTags.Count);

            var lTag = lTagBiz.CreateTag(lCategoryName, lTagName);
            lTagBiz.Add(lContact, lTag);

            lTags = lTagBiz.GetTags(lContact);
            Assert.AreEqual(1, lTags.Count);
            Assert.AreEqual(lTagName , lTags.First().Name);
        }

        [Test]
        public void RemoveTagToContact()
        {
            var lTagBiz = new BlitzerCore.Business.TagBusiness(mContext, null);
            var lContacts = new ContactBusiness(mContext).GetAll();
            var lContact = lContacts.First();
            var lCategoryName = "Body Function";
            var lTagName = "Fart";


            var lTag = lTagBiz.CreateTag(lCategoryName, lTagName);
            lTagBiz.Add(lContact, lTag);
            lTagBiz.Remove(lContact, lTag);

           var  lTags = lTagBiz.GetTags(lContact);
            Assert.AreEqual(0, lTags.Count);
        }

    }
}
