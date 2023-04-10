using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using System.Collections.Generic;
using System.Linq;

namespace BlitzerCore.UIHelpers
{
    public class TagUIHelper
    {
        public static List<UITag> Convert(List<Tag> tags, List<Tag> allTagsWithCategories)
        {
            var lOutput = new List<UITag>();
            foreach (var tag in tags)
            {
                var updatedTag = allTagsWithCategories.FirstOrDefault(x => x.Id == tag.Id);
                if(updatedTag != null)
                    lOutput.Add(Convert(updatedTag));
            }
                
            return lOutput;
        }

        private static UITag Convert(Tag tag)
        {
            var uiTag = new UITag();
            uiTag.Id = tag.Id;
            uiTag.Name = tag.Name;
            uiTag.Description = tag.Description;
            uiTag.TagCategories = TagCategoryUIHelper.Convert(tag.Id, tag.TagCategories.ToList());

            return uiTag;
        }
    }
}
