using System.Collections.Generic;
using BlitzerCore.Models;
using System;
using System.Linq;
using System.IO;

namespace NUnitTests.Helpers
{
    public class StagingHotelRateLoader
    {
        const string Hotel_file_path = @"C:\Users\idewatson\source\repos\Blitzer\Data\HotelRateStaging.csv";
        public static List<Staging.HotelRate> Load()
        {
            return System.IO.File.ReadAllLines(Hotel_file_path)
                               .Skip(1)
                               .Select(v => GetStagingHotelRate(v))
                               .ToList();
        }

        private static Staging.HotelRate GetStagingHotelRate(string csvLine)
        {
            int i = 0;
            string[] values = csvLine.Split(',');
            Staging.HotelRate lOutput = new Staging.HotelRate();
            lOutput.HotelRateStagingID = Convert.ToInt32(values[i++]);
            lOutput.RateType = values[i++];
            lOutput.HotelStagingID = Convert.ToInt32(values[i++]);
            if (values[i].Contains('"') == false)
                lOutput.Price = values[i++];
            else
                lOutput.Price = values[i++] + values[i++];
            if (values[i].Contains('"') == false)
                lOutput.RoomType = values[i++];
            else
                lOutput.RoomType = values[i++] + values[i++];
            return lOutput;
        }
    }
}