using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Models.UI;
using BlitzerCore.Utilities;
using BlitzerCore.DataAccess;
using BlitzerCore.UIHelpers;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BlitzerCore.Helpers;
using System.Linq;

namespace BlitzerCore.Business
{
    public class TagBusiness
    {
        private IDbContext DbContext { get;}
        public IConfiguration Configuration { get; }
        public TagDataAccess TagDataAccess { get; }

        public TagBusiness(IDbContext aContext, IConfiguration aConfiguration)
        {
            DbContext = aContext;
            Configuration = aConfiguration;
            TagDataAccess = new TagDataAccess(DbContext);
        }

        public Tag CreateTag(string aCategory, string aTag)
        {
            var lCategory = GetCategory(aCategory);
            var lTag = GetTag(aTag);
            AddTag(lCategory, lTag);
            return lTag;
        }

        private void AddTag(TagCategory aCategory, Tag aTag)
        {
            if ( aTag.TagCategories == null )
                aTag.TagCategories = new List<TagCategory>();

            if (aTag.TagCategories.Any(x => x.Id == aCategory.Id) == false)
            {
                aTag.TagCategories.Add(aCategory);
                TagDataAccess.Save(aTag);
            }
        }

        private Tag GetTag(string aTag)
        {
            var lTag = TagDataAccess.GetTag(aTag);
            if (lTag == null)
            {
                lTag = new Tag() { Name = aTag, Description = "" };
                TagDataAccess.Save(lTag);
                return lTag;
            }
            else
                return lTag;
        }

        public List<Tag> GetAll()
        {
            return TagDataAccess.GetTags();
        }

        private TagCategory GetCategory(string aCategory)
        {
            var lCategory = TagDataAccess.GetCategory (aCategory);
            if (lCategory == null)
            {
                lCategory = new TagCategory() { Name = aCategory, Description = "" };
                TagDataAccess.Save(lCategory);
                return lCategory;
            }
            else
                return lCategory;
        }

        public List<Tag> GetTags(Contact aContact)
        {
            return TagDataAccess.GetTags(aContact);
        }

        public void Add(Contact aContact, Tag aTag)
        {
            var lTagList = TagDataAccess.GetTags(aContact);
            if (lTagList != null && lTagList.Any(x => x.Id == aTag.Id))
                return;

            var lTaggedObj = new TaggedObject() { Contact = aContact, Tag = aTag };
            TagDataAccess.Save(lTaggedObj);
        }

        public void Remove(Contact aContact, Tag aTag)
        {
            var lTagList = TagDataAccess.GetTags(aContact);
            if (lTagList == null)
                return;

            var lTag = lTagList.Where(x => x.Id == aTag.Id).FirstOrDefault();
            if (lTag == null)
                return;

            TagDataAccess.Remove(aContact, aTag);
        }

    }
}
