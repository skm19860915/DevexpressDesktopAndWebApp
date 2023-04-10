using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models
{
    public interface IEmailHelper
    {
        void SendEmail(List<string> aMailTo, List<string> aCCs, string aHeader, string aMessage, bool aSendToSupport = true);
    }
}
