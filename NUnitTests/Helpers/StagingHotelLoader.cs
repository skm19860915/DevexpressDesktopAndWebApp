using System.Collections.Generic;
using BlitzerCore.Models;
using System;
using System.Linq;
using System.IO;

namespace NUnitTests.Helpers
{
    public class StagingHotelLoader
    {
        const string Hotel_file_path = @"C:\Users\idewatson\source\repos\Blitzer\Data\HotelStaging.csv";
        public static List<Staging.Hotel> Load()
        {
            return System.IO.File.ReadAllLines(Hotel_file_path)
                               .Skip(1)
                               .Select(v => GetStagingHotel(v))
                               .ToList();
        }

        private static Staging.Hotel GetStagingHotel(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Staging.Hotel lOutput = new Staging.Hotel();
            try
            {
                int i = 0;
                lOutput.HotelStagingID = Convert.ToInt32(values[i++]);
                if (values[1].Contains('"') == false)
                    lOutput.Name = values[i++];
                else
                    lOutput.Name = values[i++] + values[i++];

                lOutput.Location = values[i++];
                lOutput.RequestID = Convert.ToInt32(values[i++]);
                lOutput.TourOperatorID = Convert.ToInt32(values[i++]);
            } catch ( Exception e)
            {
                string lErr = e.Message;
            }
            return lOutput;
        }
    }
}
