using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using System.IO;
using WebApp.Controllers;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NUnitTests.Helpers;
using NUnitTests.ServiceStubs;
using Microsoft.AspNetCore.Mvc;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.UIHelpers;
using BlitzerCore.WebBots;
using BlitzerCore.Utilities;

namespace NUnitTests.Business
{
    public class Task
    {
        const string ClassName = "NUnitTests::Quote::";
        const int REQUEST_ID = 100;

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

        [Test]
        public void EscalateDeadLineTasks()
        {
            DateTime lToday = new DateTime(2011, 12, 12, 15, 3, 2);
            mContext.Tasks.Add(new BlitzerCore.Models.Task() {Name = "No Deadline", PriorityType = TaskPriorityTypes.Normal, Id = 1});
            mContext.Tasks.Add(new BlitzerCore.Models.Task(){Name = "Before Today", PriorityType = TaskPriorityTypes.Normal, Id = 2, Deadline = new DateTime(2010, 12,12)});
            mContext.Tasks.Add(new BlitzerCore.Models.Task() { Name = "Today", PriorityType = TaskPriorityTypes.Normal, Id = 3, Deadline = new DateTime(2011, 12, 12) });
            mContext.Tasks.Add(new BlitzerCore.Models.Task() { Name = "In the Future", PriorityType = TaskPriorityTypes.Normal, Id = 4, Deadline = new DateTime(2012, 12, 12) });
            mContext.SaveChanges();

            new TaskBusiness(mContext).EscalteDeadLineTasks(lToday);

            Assert.AreEqual(mContext.Tasks.First(x=>x.Id == 1).PriorityType, BlitzerCore.Models.TaskPriorityTypes.Normal);
            Assert.AreEqual(mContext.Tasks.First(x => x.Id == 2).PriorityType, BlitzerCore.Models.TaskPriorityTypes.Important);
            Assert.AreEqual(mContext.Tasks.First(x => x.Id == 3).PriorityType, BlitzerCore.Models.TaskPriorityTypes.Important);
            Assert.AreEqual(mContext.Tasks.First(x => x.Id == 4).PriorityType, BlitzerCore.Models.TaskPriorityTypes.Normal);
        }

        [Test]
        public void GetTripTasks()
        {

            mContext.Tasks.Add(new BlitzerCore.Models.Task() { Name = "No Deadline", PriorityType = TaskPriorityTypes.Normal, Id = 1 });
            mContext.Tasks.Add(new BlitzerCore.Models.Task() { Name = "Before Today", PriorityType = TaskPriorityTypes.Normal, Id = 2, Deadline = new DateTime(2010, 12, 12) });
            mContext.Tasks.Add(new BlitzerCore.Models.Task() { Name = "Today", PriorityType = TaskPriorityTypes.Normal, Id = 3, Deadline = new DateTime(2011, 12, 12) });
            mContext.Tasks.Add(new BlitzerCore.Models.Task() { Name = "In the Future", PriorityType = TaskPriorityTypes.Normal, Id = 4, Deadline = new DateTime(2012, 12, 12) });

            var lTaskBiz = new BlitzerCore.Business.TaskBusiness(mContext);
            // Get All Active tasks that are associated with a trip
            // Get all Active tasks that are associated with an Opportunity
            //var lTrip = lTaskBiz.GetTripTasks(lAgent);

            //.AreEqual(lTrip.Bookings[0].BookingNumber, BOOK1NAME);
        }



    }
}
