using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.AspNetHelper
{
    public class ExtendedSelectionListItem : SelectListItem
    {
        public int ParentId { get; set; }
        public ExtendedSelectionListItem (int aResortId, string aValue, string aText): base (aValue, aText)
        {
            ParentId = aResortId;
        }
    }
}
