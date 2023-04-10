using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;


namespace BlitzerCore.DataAccess
{
    public class HotelDataAccess
    {
        const string ClassName = "HotelDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<Hotel> Table { get; set; }

        public HotelDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Accommodations;

        }

        public List<AmenityMap> GetAmenities ( Hotel aHotel )
        {
            return DbContext.AmenityMaps.Where(x => x.AccommodationID == aHotel.Id).ToList();
        }

        public List<Amenity> GetAmenities()
        {
            return DbContext.Amenities.ToList();
        }

        public int Save(Hotel aHotel)
        {
            string FuncName = $"{ClassName}Save (Hotel = {aHotel.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aHotel.Id > 0)
                {
                    Table.Update(aHotel);
                    lAction = "Updated";
                }
                else
                    Table.Add(aHotel);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Hotel records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Hotel", e);
                throw e;
            }
            return lCount;

        }

        public Hotel Get(int aHotelID)
        {
            return Table
                .FirstOrDefault(x => x.Id == aHotelID);
        }

        public List<Hotel> GetAll()
        {
            return Table
            .ToList();
        }
    }
}
