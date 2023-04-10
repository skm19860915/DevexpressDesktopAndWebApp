using System.Linq;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using BlitzerCore.Utilities;

namespace BlitzerCore.DataAccess
{
    public class TagDataAccess
    {
        const string ClassName = "TagDataAccess::";
        IDbContext DbContext { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Tag> TagTable { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<TagCategory> CategoryTable { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<TaggedObject> TaggedObjsTable { get; set; }

        public TagDataAccess(IDbContext aContext)
        {
            DbContext = aContext;
            TagTable = DbContext.Tags;
            CategoryTable = DbContext.TagCategories;
            TaggedObjsTable = DbContext.TaggedObjects;
        }

        public TagCategory GetCategory (string aCategory )
        {
            var lResults = CategoryTable.Where(x => x.Name.ToUpper() == aCategory.ToUpper()).ToList();

            if (aCategory == null || lResults.Count() == 0)
                return null;

            return lResults.First();
        }
        public Tag GetTag(string aTag)
        {
            if (aTag == null || TagTable.Any(x => x.Name.ToUpper() == aTag.ToUpper()) == false )
                return null;

            return TagTable.First(x => x.Name.ToUpper() == aTag.ToUpper());
        }
        public int Save ( TagCategory aCategory )
        {
            string FuncName = $"{ClassName}Save (Category = {aCategory.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aCategory.Id > 0)
                {
                    CategoryTable.Update(aCategory);
                    lAction = "Updated";
                }
                else
                    CategoryTable.Add(aCategory);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} TagCategory records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save TagCategory", e);
                throw e;
            }
            return lCount;
        }

        public int Save(TaggedObject aTaggedObj)
        {
            string FuncName = $"{ClassName}Save (TaggedObj = {aTaggedObj.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aTaggedObj.Id > 0)
                {
                    TaggedObjsTable.Update(aTaggedObj);
                    lAction = "Updated";
                }
                else
                    TaggedObjsTable.Add(aTaggedObj);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} TaggedObject records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save TaggedObject", e);
                throw e;
            }
            return lCount;
        }

        public int Remove(Contact aContact, Tag aTag)
        {
            string FuncName = $"{ClassName}Save (Remove = {aContact.Name}, Tag = {aTag.Name}";
            try
            {
                var lTaggedObj = TaggedObjsTable.Where(x => x.ContactId == aContact.Id && x.Tag.Id == aTag.Id).FirstOrDefault();


                if (lTaggedObj == null )
                    return 0;

                DbContext.TaggedObjects.Remove(lTaggedObj);
                DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} Removed TaggedObject records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to remove TaggedObject", e);
                throw e;
            }
            return 1;
        }


        public List<Tag> GetTags()
        {
            return TagTable
                .Include(x => x.TagCategories)
                .ToList();
        }

        public int Save(Tag aTag)
        {
            string FuncName = $"{ClassName}Save (Tag = {aTag.Id}";
            string lAction = "Added";
            var lCount = 0;
            try
            {
                if (aTag.Id > 0)
                {
                    TagTable.Update(aTag);
                    lAction = "Updated";
                }
                else
                    TagTable.Add(aTag);

                lCount = DbContext.SaveChanges();
                Logger.LogInfo($"{FuncName} {lAction} {lCount} Tag records");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} Failed to save Tag", e);
                throw e;
            }
            return lCount;
        }

        public List<Tag> GetTags(Contact aContact)
        {
            return TaggedObjsTable.Where(x=>x.ContactId == aContact.Id).Select(x=>x.Tag).ToList();
        }
    }
}
