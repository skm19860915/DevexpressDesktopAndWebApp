using System;
using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;

namespace NUnitTests.Marketing
{

    class AdEngineUT
    {

        [Test]
        // Test with a list of lest than 5
        public void ShortHead()
        {
            var lTestie = new AdEngine();
            int lStart = 0;
            int lLength = 3;
            int? lStartIndex = 0;
            int? lEndIndex = 0;

            lTestie.GetInitialRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(1, lStartIndex.Value, "Start Index is wrong");
            Assert.AreEqual(2, lEndIndex.Value, "End Index is wrong");

            lTestie.GetEndRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(0, lStartIndex, "Start Index is wrong");
            Assert.AreEqual(0, lEndIndex, "End Index is wrong");
        }

        [Test]
        public void Head ()
        {
            var lTestie = new AdEngine();
            int lStart = 0;
            int lLength = 15;
            int? lStartIndex = 0;
            int? lEndIndex = 0;

            lTestie.GetInitialRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(1, lStartIndex.Value, "Start Index is wrong");
            Assert.AreEqual(5, lEndIndex.Value, "End Index is wrong");

            lTestie.GetEndRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(null, lStartIndex, "Start Index is wrong");
            Assert.AreEqual(null, lEndIndex, "End Index is wrong");
        }

        [Test]
        public void ShortList()
        {
            var lTestie = new AdEngine();
            int lStart = 14;
            int lLength = 15;
            int? lStartIndex = 0;
            int? lEndIndex = 0;

            lTestie.GetInitialRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(0, lStartIndex.Value, "Start Index is wrong");
            Assert.AreEqual(4, lEndIndex.Value, "End Index is wrong");

            lTestie.GetEndRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(null, lStartIndex, "Start Index is wrong");
            Assert.AreEqual(null, lEndIndex, "End Index is wrong");
        }


        [Test]
        public void End()
        {
            var lTestie = new AdEngine();
            int lStart = 0;
            int lLength = 15;
            int? lStartIndex = 0;
            int? lEndIndex = 0;

            lTestie.GetInitialRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(1, lStartIndex.Value, "Start Index is wrong");
            Assert.AreEqual(5, lEndIndex.Value, "End Index is wrong");

            lTestie.GetEndRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(null, lStartIndex, "Start Index is wrong");
            Assert.AreEqual(null, lEndIndex, "End Index is wrong");
        }
        [Test]
        public void Middle()
        {
            var lTestie = new AdEngine();
            int lStart = 4;
            int lLength = 9;
            int? lStartIndex = 0;
            int? lEndIndex = 0;

            lTestie.GetInitialRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(5, lStartIndex.Value, "Start Index is wrong");
            Assert.AreEqual(8, lEndIndex.Value, "End Index is wrong");

            lTestie.GetEndRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(0, lStartIndex, "Start Index is wrong");
            Assert.AreEqual(0, lEndIndex, "End Index is wrong");
        }

        [Test]
        public void ShortMiddleBreak()
        {
            var lTestie = new AdEngine();
            int lStart = 0;
            int lLength = 3;
            int? lStartIndex = 0;
            int? lEndIndex = 0;

            lTestie.GetInitialRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(1, lStartIndex.Value, "Start Index is wrong");
            Assert.AreEqual(2, lEndIndex.Value, "End Index is wrong");

            lTestie.GetEndRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(0, lStartIndex.Value, "Start Index is wrong");
            Assert.AreEqual(0, lEndIndex.Value, "End Index is wrong");

        }

        [Test]
        public void MiddleBreak()
        {
            var lTestie = new AdEngine();
            int lStart = 11;
            int lLength = 15;
            int? lStartIndex = 0;
            int? lEndIndex = 0;

            lTestie.GetInitialRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(12, lStartIndex.Value, "Start Index is wrong");
            Assert.AreEqual(14, lEndIndex.Value, "End Index is wrong");

            lTestie.GetEndRange(out lStartIndex, out lEndIndex, lStart, lLength);
            Assert.AreEqual(0, lStartIndex.Value, "Start Index is wrong");
            Assert.AreEqual(1, lEndIndex.Value, "End Index is wrong");

        }
    }
}
