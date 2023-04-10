using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Business;
using BlitzerCore.Models;
using BlitzerCore.Utilities;
using BlitzerCore.Models.UI;
using BlitzerCore.Helpers;
using BlitzerCore.UIHelpers;
namespace BlitzerCore.UIHelpers
{
    public class NoteUIHelper
    {
        public static IEnumerable<UINote> Convert(IEnumerable <Note> aNotes)
        {
            var lOutput = new List<UINote>();
            if (aNotes == null)
                return lOutput;

            foreach (var lNote in aNotes.Where(x=>x.Memo != null).OrderByDescending(y=>y.When))
                lOutput.Add(Convert(lNote));
            return lOutput;
        }

        public static UINote Convert(Note aNote)
        {
            var lUINote = new UINote();
            lUINote.Id = aNote.Id;
            lUINote.Memo = aNote.Memo;
            lUINote.When = DataHelper.GetDateTimeString(aNote.When);
            lUINote.Who = aNote.Who;
            lUINote.Where = aNote.Where;
            lUINote.WriterId = aNote.WriterId;
            lUINote.Writer = aNote.Writer.Name;

            if (aNote.Opportunity is Trip)
                lUINote.IsTrip = true;
            else
                lUINote.IsTrip = false;

            if ( aNote.Opportunity != null )
                lUINote.Opportunity = aNote.Opportunity.Name;
            if (aNote.Contact != null)
                lUINote.Contact = aNote.Contact.Name;

            lUINote.ContactId = aNote.ContactId;
            lUINote.OpportunityId = aNote.OpportunityId;
            return lUINote;
        }
    }
}
