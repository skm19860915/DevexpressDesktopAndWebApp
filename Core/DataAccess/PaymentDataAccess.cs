using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Utilities;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    class PaymentDataAccess
    {
        const string ClassName = "PaymentDataAccess::";
        IDbContext DbContext{ get; set;}
        public DbSet<Payment> Table { get; set; }

        public PaymentDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Payments;

        }

        public int Save(Payment aPayment)
        {
            string FuncName = $"{ClassName}Save (Payment = {aPayment.PaymentID}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aPayment.PaymentID > 0)
                {
                    Table.Update(aPayment);
                    lAction = "Updated";
                }
                else
                    Table.Add(aPayment);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} payment records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save payment", e);
                throw e;
            }
            return lCount;

        }

        public Payment Get(int aPaymentID)
        {
            return Table
                .Include(x=>x.Booking).ThenInclude(sub=>sub.Trip)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x=>x.Card)
                .FirstOrDefault(x => x.PaymentID == aPaymentID);
        }
    }
}
