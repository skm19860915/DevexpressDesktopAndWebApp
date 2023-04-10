using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models
{
    public interface IReportEmailer
    {
        void SendQuote(Contact aEmp, QuoteRequest aQuote);
    }
}
