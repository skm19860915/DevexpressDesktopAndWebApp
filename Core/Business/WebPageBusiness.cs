using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;

namespace BlitzerCore.Business
{
    public enum WebPageType { Country, Resort, Ranking, SubPage }
    public class WebPageBusiness
    {
        IDbContext DbContext { get; set; }

        public WebPageBusiness(IDbContext aContext)
        {
            DbContext = aContext;
        }

        public Page CreatePage(WebPageType aType, string aTitle, string aHeader)
        {
            try
            {
                switch (aType)
                {
                    case WebPageType.SubPage:
                        SubPage lOutput = new SubPage() { Title = aTitle, PageTitle = aHeader, PageTypeId = 4 };
                        new SubPageDataAccess(DbContext).Save(lOutput as SubPage);
                        return lOutput;
                    default:
                        return null;
                }
            }
            catch ( Exception e)
            {
                Logger.LogException("Failed to create new web page", e);                
            }

            throw new InvalidOperationException("You Must select valid Page Type Type");
        }
    }
}

