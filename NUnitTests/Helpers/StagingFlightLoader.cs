using System.Collections.Generic;
using BlitzerCore.Models;
using System;
using System.Linq;
using System.IO;

namespace NUnitTests.Helpers
{
    public class StagingFlightLoader
    {
        const string Flight_file_path = @"C:\Users\idewatson\source\repos\Blitzer\Data\FlightStaging.csv";
        public static List<Staging.Flight> Load()
        {
            return System.IO.File.ReadAllLines(Flight_file_path)
                               .Skip(1)
                               .Select(v => GetStagingFlight(v))
                               .ToList();
        }

        private static Staging.Flight GetStagingFlight(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Staging.Flight lOutput = new Staging.Flight();
            try
            {
                int i = 0;
                lOutput.FlightStagingID = Convert.ToInt32(values[i++]);
                lOutput.Carrier = values[i++];
                if (values[i].Contains('"') == false)
                    lOutput.DepartLocation = values[i++];
                else
                    lOutput.DepartLocation = values[i++] + values[i++];

                if (values[i].Contains('"') == false)
                    lOutput.DepartTime = values[i++];
                else
                {
                    lOutput.DepartTime = values[i++];
                    lOutput.DepartTime = lOutput.DepartTime.Replace("\"", "");
                }

                if (values[i].Contains('"') == false)
                    lOutput.ArrivalLocation = values[i++];
                else
                    lOutput.ArrivalLocation = values[i++] + values[i++];

                if (values[i].Contains('"') == false)
                    lOutput.ArrivalTime = values[i++];
                else
                {
                    lOutput.ArrivalTime = values[i++];
                    lOutput.ArrivalTime = lOutput.ArrivalTime.Replace("\"", "");
                }

                lOutput.Aircraft = values[i++];
                lOutput.NumberOfStop = values[i++];
                lOutput.QuoteGroupId = Convert.ToInt32(values[i++]);
                lOutput.Side = (Staging.Flight.SIDES)Convert.ToInt32(values[i++]);
                lOutput.TourOperatorID = Convert.ToInt32(values[i++]);
                lOutput.ArrivalDate = values[i++];
                lOutput.DepartDate = values[i++];
                lOutput.LegGUID = Guid.Parse( values[i++]);
            }
            catch ( Exception e)
            {
                string lErr = e.Message;
            }
            return lOutput;
        }
    }
}
