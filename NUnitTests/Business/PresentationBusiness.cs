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
using BlitzerCore.Helpers;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.UIHelpers;
using WebApp.DataServices;
using WebApp.Controllers;
using BlitzerCore.WebBots;

namespace NUnitTests.Business
{
    public class PresentationBusiness
    {
        const int lKey1 = 1;
        RepositoryContext mContext = null;
        DbContextOptions<RepositoryContext> mDBOptions = null;
        const string CLIENT_NAME = "James007";

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
        public void RawSave()
        {

            var lPresentation = new Presentation()
            {
                Id = 0,
                ClientName = CLIENT_NAME,
                Created = DateTime.Now
            };

            var lWebPages = mContext.WebPages.ToList();
            lPresentation.Queue.Add(new PresentationQueueItem() { Id = 501, WebPage = lWebPages[0], Location = lPresentation.Queue.Count(), PresentationId = lKey1 });
            lPresentation.Queue.Add(new PresentationQueueItem() { Id = 502, WebPage = lWebPages[1], Location = lPresentation.Queue.Count(), PresentationId = lKey1 });
            lPresentation.Queue.Add(new PresentationQueueItem() { Id = 503, WebPage = lWebPages[2], Location = lPresentation.Queue.Count(), PresentationId = lKey1 });

            new BlitzerCore.Business.PresentationBusiness(mContext, null).Save(lPresentation);
            var lResult = mContext.Presentations.First();
            Assert.IsNotNull(lResult);
            Assert.AreEqual(lKey1, lResult.Id);
            Assert.AreEqual(CLIENT_NAME, lResult.ClientName);
            Assert.AreEqual(3, lResult.Queue.Count());
        }

        [Test]
        public void CreateFromName()
        {

            new BlitzerCore.Business.PresentationBusiness(mContext, null).CreatePresenation(CLIENT_NAME);
            var lResult = mContext.Presentations.First();
            Assert.IsNotNull(lResult);
            Assert.AreEqual(CLIENT_NAME, lResult.ClientName);
            Assert.AreEqual(0, lResult.Queue.Count());
        }

        [Test]
        public void Build()
        {
            var lPresBiz = new BlitzerCore.Business.PresentationBusiness(mContext, null);
            var lWebPages = lPresBiz.WebPages();

            Assert.AreEqual(3, lWebPages.Count);

            //Test EmptyBiz
            var lPresenation = lPresBiz.GetPresentationItem("2348asdfasf");
            Assert.IsNull(lPresenation);

            lPresBiz.CreatePresenation("Jack");
            var lPresentations = lPresBiz.Get();
            Assert.AreEqual(1, lPresentations.Count);
            Assert.AreEqual(Presentation.Statuses.NotReady, lPresentations.First().Status);

            lPresBiz.CreatePresenation("Bill");
            lPresentations = lPresBiz.Get().Where(x => x.ClientName == "Bill").ToList();
            Assert.AreEqual(1, lPresentations.Count);
            lPresBiz.AddToQueue(lPresentations.First(), lWebPages.First());
            Assert.AreEqual(Presentation.Statuses.NotReady, lPresentations.First().Status);
            Assert.AreEqual(null, lPresBiz.GetPresentationItem(lPresentations.First().Guid));
            // Activate Presenation
            lPresBiz.StartTour(lPresentations.First());
            var lElem = lPresBiz.GetPresentationItem(lPresentations.First().Guid);
            Assert.AreEqual(lWebPages[0].Url, lElem.WebPage.Url);
            // Presenation should not be complete
            var lCompleted = lPresBiz.Get().Where(x => x.ClientName == "Bill").First();
            Assert.AreEqual(Presentation.Statuses.Completed, lCompleted.Status);
        }


        [Test]
        public void UserCase()
        {
            const string  CLIENT = "UserCase";
            var lPresBiz = new BlitzerCore.Business.PresentationBusiness(mContext, null);
            var lWebPages = lPresBiz.WebPages();

            Assert.AreEqual(3, lWebPages.Count);

            var lPresentation = lPresBiz.CreatePresenation(CLIENT);
            lPresBiz.AddToQueue(lPresentation, lWebPages[2]);
            lPresBiz.AddToQueue(lPresentation, lWebPages[0]);
            lPresBiz.AddToQueue(lPresentation, lWebPages[1]);

            lPresBiz.StartTour(lPresentation);
            var lElem = lPresBiz.GetPresentationItem(lPresentation.Guid);
            Assert.AreEqual(lWebPages[2].Url, lElem.WebPage.Url);

            lElem = lPresBiz.GetPresentationItem(lPresentation.Guid);
            Assert.AreEqual(lWebPages[0].Url, lElem.WebPage.Url);

            lElem = lPresBiz.GetPresentationItem(lPresentation.Guid);
            Assert.AreEqual(lWebPages[1].Url, lElem.WebPage.Url);

            // Presenation should not be complete
            var lCompleted = lPresBiz.Get().Where(x => x.ClientName == CLIENT).First();
            Assert.AreEqual(Presentation.Statuses.Completed, lCompleted.Status);
        }
    }
}
