using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models.UI;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlitzerCore.Models
{
    public interface IBlazorService
    {
        List<Contact> AddContact(int aTripId, string aContactId);
        List<Contact> RemoveContact(int aTripId, string aContactId);
        List<UIUserStory> GetOpenRequirements();
        List<UIUserStory> GetUnderReViewRequirements();
        List<SelectListItem> GetActiveSprintsList();
        List<UISprint> GetActiveSprints();
        UITrip GetTrip(int aTrip);
        HouseHold GetHouseHold(string aMember);
        List<UIContactCore> AddHouseHoldMember(int aHouseHoldId, string aMemberId);
        List<UIContactCore> RemoveHouseHoldMember(int aHouseHoldId, string aMember);
    }
}
