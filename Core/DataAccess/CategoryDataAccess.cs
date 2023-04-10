using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlitzerCore.DataAccess
{
    public class CategoryDataAccess
    {

        IDbContext DbContext { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<Category> Table { get; set; }

        public CategoryDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            Table = DbContext.Category;
        }

        public Category Get(int aID)
        {
            return Table
                .FirstOrDefault(x => x.Id == aID);
        }

        public List<Category> GetAll()
        {
            return Table
                .ToList();
        }

        public int Save(Category aCategory)
        {
            if (aCategory.Id > 0)
                Table.Update(aCategory);
            else
                Table.Add(aCategory);

            return DbContext.SaveChanges();
        }

    }
}
