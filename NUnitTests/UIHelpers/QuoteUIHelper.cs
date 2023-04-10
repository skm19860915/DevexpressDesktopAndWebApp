using System.Collections.Generic;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using System.Security.Policy;
using Microsoft.Exchange.WebServices.Data;
using WebApp.Controllers;
using System;
using BlitzerCore.DataAccess;
using NUnitTests.Helpers;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.UIHelpers;

namespace NUnitTests.UIHelpers
{
    public class QuoteUIHelper
    {
        [TestCase("1130")]
        [TestCase("11:30")]
        [TestCase("11:30 am")]
        [TestCase("11:30 Am")]
        [TestCase("1130 Am")]
        public void ConvertMorningTime(string aValue)
        {
            Assert.AreEqual("11:30 AM", BlitzerCore.UIHelpers.QuoteUIHelper.GetTime(aValue));

        }
        [TestCase("1730")]
        [TestCase("17:30")]
        [TestCase("17:30 am")]
        [TestCase("17:30 Am")]
        [TestCase("1730 Pm")]
        [TestCase("530 Pm")]
        [TestCase("5:30 Pm")]
        [TestCase("5:30 PM")]
        public void ConvertEveningTime(string aValue)
        {
            Assert.AreEqual("5:30 PM", BlitzerCore.UIHelpers.QuoteUIHelper.GetTime(aValue));

        }
    }
}
