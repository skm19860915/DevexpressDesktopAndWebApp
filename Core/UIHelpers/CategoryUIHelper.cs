using BlitzerCore.Models.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlitzerCore.UIHelpers
{
    public class CategoryUIHelper
    {
        public static List<UICategory> Convert(List<UITag> tags)
        {
            //var testTags = new List<UITag>();

            //var testCategories1 = new List<UITagCategory>();
            
            //var testCategory1 = new UITagCategory()
            //{
            //    Id = 1,
            //    Name = "Cate1",
            //    Description = "1111111",
            //    TagId = 1
            //};
            //var testCategory2 = new UITagCategory()
            //{
            //    Id = 2,
            //    Name = "Cate2",
            //    Description = "2222222222222",
            //    TagId = 2
            //};
            //testCategories1.Add(testCategory1);
            //testCategories1.Add(testCategory2);

            //var testTag1 = new UITag()
            //{
            //    Id = 1,
            //    Name = "Test1",
            //    Description = "Description 1",
            //    TagCategories = testCategories1
            //};
            //var testTag2 = new UITag()
            //{
            //    Id = 2,
            //    Name = "Test2",
            //    Description = "Description 2",
            //    TagCategories = testCategories1
            //};

            //testTags.Add(testTag1);
            //testTags.Add(testTag2);


            var lOutput = new List<UICategory>();
            foreach(var tag in tags)
            {
                var tagCategories = tag.TagCategories;
                foreach(var tCategory in tagCategories)
                {
                    if(lOutput.Count > 0)
                    {
                        var record = lOutput.FirstOrDefault(x => x.Id == tCategory.Id);
                        if(record == null)
                        {
                            var category = new UICategory();
                            category.Id = tCategory.Id;
                            category.Name = tCategory.Name;
                            category.Description = tCategory.Description;
                            var outputTags = new List<UITag>();
                            outputTags.Add(tag);
                            category.Tags = outputTags;

                            lOutput.Add(category);
                        }
                        else
                            record.Tags.Add(tag);
                    }
                    else
                    {
                        var category = new UICategory();
                        category.Id = tCategory.Id;
                        category.Name = tCategory.Name;
                        category.Description = tCategory.Description;
                        var outputTags = new List<UITag>();
                        outputTags.Add(tag);
                        category.Tags = outputTags;

                        lOutput.Add(category);
                    }
                }
            }

            return lOutput;
        }
    }
}
