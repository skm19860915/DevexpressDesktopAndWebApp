using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using Microsoft.EntityFrameworkCore;

/*

namespace BlitzerCore.DataAccess
{
    class TemplateDataAccess
    {
        const string ClassName = "TemplateDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<Template> Table { get; set; }

        public TemplateDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Templates;

        }

        public int Save(Template aTemplate)
        {
            string FuncName = $"{ClassName}Save (Template = {aTemplate.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aTemplate.TemplateID > 0)
                {
                    Table.Update(aTemplate);
                    lAction = "Updated";
                }
                else
                    Table.Add(aTemplate);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Template records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Template", e);
                throw e;
            }
            return lCount;

        }

        public Template Get(int aTemplateID)
        {
            return Table
                .Include(x => x.Booking)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .FirstOrDefault(x => x.TemplateID == aTemplateID);
        }
    }
}
*/

