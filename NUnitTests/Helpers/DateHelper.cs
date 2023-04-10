using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;


namespace NUnitTests.Helpers
{
    [TestFixture]
    class DateHelper
    {
        [TestCase("56")]
        [TestCase("10")]
        [TestCase("0")]
        [TestCase("L")]

        public void Month(string lValue)
        {
            DateTime lResult = DateTime.Now;

            try
            {
                lResult = BlitzerCore.Helpers.DateHelper.ConvertDate(lValue + "/1/1960").Value;
            }
            catch (InvalidDataException e)
            {
                Assert.Pass("Valid Exception Thrown - " + e);
                return;
            }
            catch (Exception)
            {
                Assert.Fail("Unknow Exception Thrown");
                return;
            }

            Assert.AreEqual (new DateTime(1960,10,1, 0, 0, 0), lResult, "Converted Time Incorrectly" );
        }

        [TestCase("56")]
        [TestCase("1")]
        [TestCase("0")]
        [TestCase("L")]
        public void Day(string lValue)
        {
            DateTime lResult = DateTime.Now;

            try
            {
                lResult = BlitzerCore.Helpers.DateHelper.ConvertDate("10/"+lValue+"/1960").Value;
            }
            catch (InvalidDataException e)
            {
                Assert.Pass("Valid Exception Thrown - " + e);
                return;
            }
            catch (Exception)
            {
                Assert.Fail("Unknow Exception Thrown");
                return;
            }

            Assert.AreEqual(new DateTime(1960, 10, 1, 0, 0, 0), lResult, "Converted Time Incorrectly");
        }

        [TestCase("1960")]
        [TestCase("60")]
        [TestCase("19600000")]
        [TestCase("0")]
        [TestCase("L")]
        public void Year(string lValue)
        {
            DateTime lResult = DateTime.Now;

            try
            {
                lResult = BlitzerCore.Helpers.DateHelper.ConvertDate("10/1/" + lValue).Value;
            }
            catch (InvalidDataException e)
            {
                Assert.Pass("Valid Exception Thrown - " + e);
                return;
            }
            catch (Exception)
            {
                Assert.Fail("Unknow Exception Thrown");
                    return;
            }

            Assert.AreEqual(new DateTime(1960, 10, 1, 0, 0, 0), lResult, "Converted Time Incorrectly");
        }

    }
}
