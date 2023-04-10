using System;
using System.Collections.Generic;
using NUnit.Framework;
using BlitzerCore.Business;
using BlitzerCore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApp.DataServices;
using NUnitTests.Helpers;

namespace NUnitTests.Business
{
    public class Air
    {
        RepositoryContext mContext = null;
        DbContextOptions<WebApp.DataServices.RepositoryContext> mDBOptions = null;


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
                Helpers.DataLake.LoadAirPorts(mContext);
            }

            return mContext;
        }

        public static List<Staging.Flight> CreateFlightWithNullArrivalDate()
        {
            var lLegs = new List<Staging.Flight>();
            lLegs.Add(new Staging.Flight()
            {
                DepartDate = "Mon 09/21/2020 ",
                ArrivalDate = "Mon 09/21/2020 ",
                DepartLocation = "Atlanta, GA (ATL) ",
                DepartTime = " 8:30 AM ",
                ArrivalLocation = "Cancun, Mexico (CUN)",
                ArrivalTime = " 10:00 AM",
                QuoteGroupId = 100,
                LegGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC607359"),
                Side = Staging.Flight.SIDES.DEPARTURE
            });
            lLegs.Add(new Staging.Flight()
            {
                DepartDate = " Fri 09/25/2020 ",
                ArrivalDate = "Fri 09/25/2020 ",
                DepartLocation = "Cancun, Mexico (CUN)  ",
                DepartTime = "  2:00 PM ",
                ArrivalLocation = "Atlanta, GA (ATL)",
                ArrivalTime = "  5:30 PM",
                QuoteGroupId = 100,
                LegGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC607359"),
                Side = Staging.Flight.SIDES.RETURN
            });

            return lLegs;
        }

        public List<Staging.Flight> CreateNonStopRountTrip(BlitzerCore.Models.QuoteGroup aQuoteGroup, string aDepartAirPort, string aDestAirPort)
        {
            var lLegs = new List<Staging.Flight>();

            var lDepart = DataLake.GetAirPorts().Where(x => x.Code == aDepartAirPort).FirstOrDefault();
            var lDest = DataLake.GetAirPorts().Where(x => x.Code == aDestAirPort).FirstOrDefault();

            lLegs.Add(new Staging.Flight()
            {
                DepartDate = "Mon 09/21/2020 ",
                ArrivalDate = "Mon 09/21/2020 ",
                DepartLocation = lDepart.Name + " ",
                DepartTime = " 8:30 AM ",
                ArrivalLocation = lDest.Name + " ",
                ArrivalTime = " 10:00 AM",
                QuoteGroupId = aQuoteGroup.Id,
                LegGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC607359"),
                Side = Staging.Flight.SIDES.DEPARTURE
            });
            lLegs.Add(new Staging.Flight()
            {
                DepartDate = " Fri 09/25/2020 ",
                ArrivalDate = "Fri 09/25/2020 ",
                DepartLocation = lDest.Name + " ",
                DepartTime = "  2:00 PM ",
                ArrivalLocation = lDepart.Name + " ",
                ArrivalTime = "  5:30 PM",
                QuoteGroupId = aQuoteGroup.Id,
                LegGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC607359"),
                Side = Staging.Flight.SIDES.RETURN
            });

            return lLegs;
        }

        public static List<Staging.Flight> Create1StopRountTrip(BlitzerCore.Models.QuoteGroup aQuoteGroup)
        {
            var lLegs = new List<Staging.Flight>();
            lLegs.Add(new Staging.Flight()
            {
                DepartDate = "Mon 09/21/2020 ",
                ArrivalDate = "Mon 09/21/2020 ",
                DepartLocation = "Atlanta, GA (ATL) ",
                DepartTime = " 8:30 AM ",
                ArrivalLocation = "Cancun, Mexico (CUN)",
                ArrivalTime = " 10:00 AM",
                QuoteGroupId = aQuoteGroup.Id,
                TourOperatorID = 1,
                LegGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC607359"),
                ItineraryGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC609999"),
                Side = Staging.Flight.SIDES.DEPARTURE
            });
            lLegs.Add(new Staging.Flight()
            {
                DepartDate = "Mon 09/21/2020 ",
                ArrivalDate = "Mon 09/21/2020 ",
                DepartLocation = "Atlanta, GA (ATL) ",
                DepartTime = " 11:45 AM ",
                ArrivalLocation = "Cancun, Mexico (CUN)",
                ArrivalTime = " 2:00 PM",
                QuoteGroupId = aQuoteGroup.Id,
                TourOperatorID = 1,
                LegGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC607359"),
                ItineraryGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC609999"),
                Side = Staging.Flight.SIDES.DEPARTURE
            });
            lLegs.Add(new Staging.Flight()
            {
                DepartDate = " Fri 09/25/2020 ",
                ArrivalDate = "Fri 09/25/2020 ",
                DepartLocation = "Cancun, Mexico (CUN)  ",
                DepartTime = "  2:00 PM ",
                ArrivalLocation = "Atlanta, GA (ATL)",
                ArrivalTime = "  5:30 PM",
                QuoteGroupId = aQuoteGroup.Id,
                TourOperatorID = 1,
                LegGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC601111"),
                ItineraryGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC609999"),
                Side = Staging.Flight.SIDES.RETURN
            });

            return lLegs;
        }

        public List<Staging.Flight> Create2ndNonStopRoundTrip(BlitzerCore.Models.QuoteGroup aQuoteGroup, string aDepartAirPort, string aDestAirPort)
        {
            var lLegs = new List<Staging.Flight>();

            var lDepart = DataLake.GetAirPorts().Where(x => x.Code == aDepartAirPort).FirstOrDefault();
            var lDest = DataLake.GetAirPorts().Where(x => x.Code == aDestAirPort).FirstOrDefault();

            lLegs.Add(new Staging.Flight()
            {
                DepartDate = "Mon 09/21/2020 ",
                ArrivalDate = "Mon 09/21/2020 ",
                DepartLocation = lDepart.Name + " ",
                DepartTime = "3:30 PM ",
                ArrivalLocation = lDest.Name + " ",
                ArrivalTime = " 7:00 PM",
                QuoteGroupId = aQuoteGroup.Id,
                LegGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC607359"),
                ItineraryGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC609999"),
                Side = Staging.Flight.SIDES.DEPARTURE
            });
            lLegs.Add(new Staging.Flight()
            {
                DepartDate = " Fri 09/25/2020 ",
                ArrivalDate = "Fri 09/25/2020 ",
                DepartLocation = lDest.Name + " ",
                DepartTime = "  6:00 PM ",
                ArrivalLocation = lDepart.Name + " ",
                ArrivalTime = "  8:30 PM",
                QuoteGroupId = aQuoteGroup.Id,
                LegGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC601111"),
                ItineraryGUID = new Guid("D1B65BE2-F14A-4615-B2CA-05B9DC609999"),
                Side = Staging.Flight.SIDES.RETURN
            });

            return lLegs;
        }

        [Test]
        public void ParseDate()
        {
            var lsDate = "Fri 09/25/2020 5:38 PM";
            var lDate = DateTime.Now;
            Assert.AreEqual(true, DateTime.TryParse(lsDate, out lDate));

            lsDate = "Fri 09/25/2020 10:00 AM";
            Assert.AreEqual(true, DateTime.TryParse(lsDate, out lDate));
        }

        [Test]
        public void ParseAirPort()
        {
            using (var lContext = CreateInMemoryContext())
            {
                string lAirPort = "Test Eric Watson(EJW)";
                var lAirBiz = new BlitzerCore.Business.AirBusiness(lContext, null);
                var lProd = lAirBiz.GetAirPort(lAirPort);

                Assert.AreEqual(344, lProd.AirPortID);

            }
        }

        [Test]
        public void AddNewAirPort()
        {
            using (var lContext = CreateInMemoryContext())
            {
                string lAirPort = "NEW Airport (NW1)";
                var lAirBiz = new BlitzerCore.Business.AirBusiness(lContext, null);
                var lProd = lAirBiz.GetAirPort(lAirPort);
            }
        }

        [Test]
        public void ConvertNullArrivalDate()
        {
            using (var lContext = CreateInMemoryContext())
            {
                var lStaging = CreateFlightWithNullArrivalDate();
                var lAirBiz = new BlitzerCore.Business.AirBusiness(lContext, null);
                var lProd = lAirBiz.Convert(lStaging, new BlitzerCore.Models.QuoteGroup());

                Assert.AreEqual(1, lProd.Count());
                Assert.AreEqual(1, lProd[0].OutBound.Flights.Count());
                Assert.AreEqual(1, lProd[0].InBound.Flights.Count());

                DateTime lOutBoundDepart = new DateTime(2020, 9, 21, 8, 30, 0);
                DateTime lOutBoundArrive = new DateTime(2020, 9, 21, 10, 00, 0);
                DateTime lInBoundDepart = new DateTime(2020, 9, 25, 14, 00, 0);
                DateTime lInBoundArrive = new DateTime(2020, 9, 25, 17, 30, 0);

                Assert.AreEqual(lOutBoundDepart, lProd[0].OutBound.Start);
                Assert.AreEqual(lOutBoundArrive, lProd[0].OutBound.End);
                Assert.AreEqual(lInBoundDepart, lProd[0].InBound.Start);
                Assert.AreEqual(lInBoundArrive, lProd[0].InBound.End);
            }
        }

        [Test]
        public void ConvertNonStopRoundTrip()
        {
            using (var lContext = CreateInMemoryContext())
            {
                var lQuoteGroup = new BlitzerCore.Models.QuoteGroup() { Id = 788 };
                var lStaging = CreateNonStopRountTrip(lQuoteGroup, DataLake.AIRPORTCODE1, DataLake.AIRPORTCODE2);
                var lAirBiz = new BlitzerCore.Business.AirBusiness(lContext, null);
                var lProd = lAirBiz.Convert(lStaging, lQuoteGroup);

                Assert.AreEqual(1, lProd.Count());
                Assert.AreEqual(1, lProd[0].OutBound.Flights.Count());
                Assert.AreEqual(1, lProd[0].InBound.Flights.Count());

                DateTime lOutBoundDepart = new DateTime(2020, 9, 21, 8, 30, 0);
                DateTime lOutBoundArrive = new DateTime(2020, 9, 21, 10, 00, 0);
                DateTime lInBoundDepart = new DateTime(2020, 9, 25, 14, 00, 0);
                DateTime lInBoundArrive = new DateTime(2020, 9, 25, 17, 30, 0);

                Assert.AreEqual(lOutBoundDepart, lProd[0].OutBound.Start);
                Assert.AreEqual(lOutBoundArrive, lProd[0].OutBound.End);
                Assert.AreEqual(lInBoundDepart, lProd[0].InBound.Start);
                Assert.AreEqual(lInBoundArrive, lProd[0].InBound.End);
            }
        }


        [Test]
        public void Convert1StopRoundTrip()
        {
            using (var lContext = CreateInMemoryContext())
            {
                var lQuoteGroup = new BlitzerCore.Models.QuoteGroup() { Id = 788 };
                var lStaging = Create1StopRountTrip(lQuoteGroup);
                var lAirBiz = new BlitzerCore.Business.AirBusiness(lContext, null);
                var lProd = lAirBiz.Convert(lStaging, lQuoteGroup);

                Assert.AreEqual(1, lProd.Count());
                Assert.AreEqual(2, lProd[0].OutBound.Flights.Count());
                Assert.AreEqual(1, lProd[0].InBound.Flights.Count());

                DateTime lOutBoundDepart = new DateTime(2020, 9, 21, 8, 30, 0);
                DateTime lOutBoundArrive = new DateTime(2020, 9, 21, 14, 00, 0);
                DateTime lInBoundDepart = new DateTime(2020, 9, 25, 14, 00, 0);
                DateTime lInBoundArrive = new DateTime(2020, 9, 25, 17, 30, 0);

                Assert.AreEqual(lOutBoundDepart, lProd[0].OutBound.Start);
                Assert.AreEqual(lOutBoundArrive, lProd[0].OutBound.End);
                Assert.AreEqual(lInBoundDepart, lProd[0].InBound.Start);
                Assert.AreEqual(lInBoundArrive, lProd[0].InBound.End);
                Assert.AreEqual(1, lProd[0].OutBound.Stops);
                Assert.AreEqual(0, lProd[0].InBound.Stops);
            }
        }

        [Test]
        public void Convert2StopRoundTrips()
        {
            using (var lContext = CreateInMemoryContext())
            {
                var lQuoteGroup = new BlitzerCore.Models.QuoteGroup() { Id = 788 };
                var lStaging = Create1StopRountTrip(lQuoteGroup);
                lStaging.AddRange(Create2ndNonStopRoundTrip(lQuoteGroup, DataLake.GetAirPorts().ElementAt(3).Code, DataLake.GetAirPorts().ElementAt(4).Code));

                var lAirBiz = new BlitzerCore.Business.AirBusiness(lContext, null);
                var lProd = lAirBiz.Convert(lStaging, lQuoteGroup);

                Assert.AreEqual(1, lProd.Count());
                Assert.AreEqual(3, lProd[0].OutBound.Flights.Count());
                Assert.AreEqual(2, lProd[0].InBound.Flights.Count());

                DateTime lOutBoundDepart = new DateTime(2020, 9, 21, 8, 30, 0);
                DateTime lOutBoundArrive = new DateTime(2020, 9, 21, 19, 00, 0);
                DateTime lInBoundDepart = new DateTime(2020, 9, 25, 14, 00, 0);
                DateTime lInBoundArrive = new DateTime(2020, 9, 25, 20, 30, 0);

                Assert.AreEqual(lOutBoundDepart, lProd[0].OutBound.Start);
                Assert.AreEqual(lOutBoundArrive, lProd[0].OutBound.End);
                Assert.AreEqual(lInBoundDepart, lProd[0].InBound.Start);
                Assert.AreEqual(lInBoundArrive, lProd[0].InBound.End);
                Assert.AreEqual(2, lProd[0].OutBound.Stops);
                Assert.AreEqual(1, lProd[0].InBound.Stops);
            }
        }
    }
}
