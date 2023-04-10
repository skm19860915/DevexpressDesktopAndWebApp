using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlitzerCore.Utilities;
using BlitzerCore.Models;
namespace BlitzerCore.DataAccess
{
    public class NotesDataAccess
    {
        const string ClassName = "NotesDataAccess::";
        IDbContext DbContext { get; set; }
        public DbSet<Note> Table { get; set; }

        public NotesDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Notes;
        }

        public IEnumerable<Note> Get()
        {
            var lNotes = Table
                .Include(x => x.Contact)
                .Include(x => x.Company)
                .Include(x => x.Opportunity)
                .Include(x=>x.Writer)
                .Where(x => x.When > DateTime.Now.AddDays(-21) && x.Memo != null && x.Memo.Trim().Length > 0 )
                .OrderByDescending(x=>x.When);
            return lNotes;
        }

        public int Save(Note aNote)
        {
            string FuncName = $"{ClassName}Save (Note = {aNote.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aNote.Id > 0)
                {
                    Table.Update(aNote);
                    lAction = "Updated";
                }
                else
                    Table.Add(aNote);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} note records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save note", e);
                throw e;
            }
            return lCount;

        }

    }
}
