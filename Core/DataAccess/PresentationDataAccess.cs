using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace BlitzerCore.DataAccess
{
    public class PresentationDataAccess
    {

        IDbContext DbContext { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Presentation> Table { get; set; }

        public PresentationDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Presentations;
        }

        public Presentation Get(int aID)
        {
            var presentation = Table.FirstOrDefault(x => x.Id == aID);

            if (presentation != null && presentation.Id > 0)
            {
                presentation.Queue = DbContext.PresentationQueueItems.Where(m => m.PresentationId == aID).OrderBy(m => m.Location).ToList();
            }

            return presentation;
        }


        public List<Presentation> Get()
        {
            return Table
                .ToList();
        }

        public Presentation Get(string aGuid)
        {
            return Table
                .Include(x => x.Queue).ThenInclude(x1 => x1.WebPage)
                .Include(x => x.Queue).ThenInclude(x1 => x1.Presentation)
                .Where(x => x.Guid == aGuid && x.Status == Presentation.Statuses.Ready)
                .FirstOrDefault();
        }

        public List<Presentation> GetAll()
        {
            return Table
                .ToList();
        }

        public int Save(Presentation aPresenation)
        {
            if (aPresenation.Id > 0)
                Table.Update(aPresenation);
            else
                Table.Add(aPresenation);

            return DbContext.SaveChanges();
        }

        public List<WebPage> GetWebPages()
        {
            return DbContext.WebPages.ToList();
        }
    }
}
