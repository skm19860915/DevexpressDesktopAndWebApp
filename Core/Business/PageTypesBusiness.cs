using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BlitzerCore.Business
{
    public class PageTypesBusiness
    {
        public IDbContext DbContext { get; set; }

        public PageTypesBusiness(IDbContext aContext)
        {
            DbContext = aContext;
        }

        public List<SelectListItem> PageTypes()
        {
            List<SelectListItem> lOutput = new List<SelectListItem>();

            var lTypes = new PageDataAccess(DbContext).GetPageTypes();
            foreach (var lType in lTypes)
                lOutput.Add(new SelectListItem() { Value = lType.Id.ToString(), Text = lType.Name });

            return lOutput;
        }
    }
}
