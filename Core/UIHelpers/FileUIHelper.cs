using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.DataAccess;
using BlitzerCore.Business;
using BlitzerCore.Helpers;

namespace BlitzerCore.UIHelpers
{
    public class FileUIHelper
    {
        public static UIFile Convert ( File aFile )
        {
            var lUIFile = new UIFile {
                Description = aFile.Description,
                ID = aFile.ID,
                ContactId = aFile.ContactId,
                FileType = aFile.FileType.Name,
                FileTypeId = aFile.FileTypeId,
                Name = aFile.Name,
                OpportunityId = aFile.OpportunityId,
                Owner = ContactUIHelper.Convert(aFile.Owner, false),
                URI = aFile.URI,
                Version = aFile.Version,
                Date = aFile.Date
            };
            return lUIFile;
        }
    }
}
