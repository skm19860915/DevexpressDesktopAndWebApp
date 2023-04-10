using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using System.Collections.Generic;

namespace BlitzerCore.UIHelpers
{
    public class TagCategoryUIHelper
    {
        public static List<UITagCategory> Convert(int tagId, List<TagCategory> categories)
        {
            var lOutput = new List<UITagCategory>();
            foreach (var category in categories)
                lOutput.Add(Convert(tagId, category));
            return lOutput;
        }

        private static UITagCategory Convert(int tagId, TagCategory category)
        {
            var uiCategory = new UITagCategory();
            uiCategory.Id = category.Id;
            uiCategory.Name = category.Name;
            uiCategory.Description = category.Description;
            uiCategory.TagId = tagId;

            return uiCategory;
        }
    }
}
