using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public class InvoiceBusiness
    {
        IDbContext mContext = null;

        public InvoiceBusiness(IDbContext aContext)
        {
            mContext = aContext;
        }

        public Invoice CreateInvoice(Trip aTrip)
        {
            Invoice lInvoice = new Invoice()
            {
                TripID = aTrip.ID,
                Client = aTrip.Travelers[0].User
            };

            new InvoiceDataAccess(mContext).Save(lInvoice);

            return lInvoice;
        }

        public Invoice Get(Trip aTrip )
        {
            var lInvoice = new InvoiceDataAccess(mContext).Get(aTrip);
            if (lInvoice == null)
                lInvoice = CreateInvoice(aTrip);

            return lInvoice;
        }

        public Invoice Get (int aId )
        {
            return new InvoiceDataAccess(mContext).Get(aId);
        }

    }
}
