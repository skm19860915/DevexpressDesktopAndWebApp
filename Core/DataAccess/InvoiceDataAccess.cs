using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;



namespace BlitzerCore.DataAccess
{
    class InvoiceDataAccess
    {
        const string ClassName = "InvoiceDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<Invoice> Table { get; set; }

        public InvoiceDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Invoices;

        }

        public int Save(Invoice aInvoice)
        {
            string FuncName = $"{ClassName}Save (Invoice = {aInvoice.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aInvoice.Id > 0)
                {
                    Table.Update(aInvoice);
                    lAction = "Updated";
                }
                else
                    Table.Add(aInvoice);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Invoice records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Invoice", e);
                throw e;
            }
            return lCount;

        }

        public Invoice Get(int aInvoiceID)
        {
            return Table
                .Include(x=>x.Trip).ThenInclude(x1=>x1.Agent).ThenInclude(x2=>x2.PhoneNumbers)
                .Include(x => x.Trip).ThenInclude(x1 => x1.Agent).ThenInclude(x2 => x2.Emails)
                .Include(x=>x.Client)
                .FirstOrDefault(x => x.Id == aInvoiceID);
        }
        public Invoice Get(Trip aTrip)
        {
            return Table
                .Include(x => x.Trip).ThenInclude(x1 => x1.Agent).ThenInclude(x2 => x2.PhoneNumbers)
                .Include(x => x.Trip).ThenInclude(x1 => x1.Agent).ThenInclude(x2 => x2.Emails)
                .Include(x=>x.Trip).ThenInclude(x1=>x1.Bookings).ThenInclude(x3=>x3.Payments).ThenInclude(x4=>x4.Payee)
                .Include(x => x.Client)
                .FirstOrDefault(x => x.TripID == aTrip.ID);
        }

    }
}


