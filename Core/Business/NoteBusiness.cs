using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.DataAccess;
using BlitzerCore.Models;
using BlitzerCore.Models.UI;
using BlitzerCore.UIHelpers;
using BlitzerCore.Utilities;

namespace BlitzerCore.Business
{
    public class NoteBusiness 
    {
        const string ClassName = "NoteBusiness::";

        public const int NewsPageSize = 4;
        private IDbContext DbContext { get; set; }

        public NoteBusiness(IDbContext aContext) 
        {
            DbContext = aContext;
        }
        //public void Save(Note model, Task aTask)
        //{
        //    // if the news items has a parent, we new the project 
        //    if (model.ParentID != null)
        //    {
        //        var lParent = getNote(model.ParentID.Value);
        //        if (lParent != null)
        //        {
        //            model.UserStoryID = lParent.UserStoryID;
        //            model.ContactId = lParent.ContactId;
        //        }
        //    }

        //    Logger.LogMessage("NoteBusiness.save");
        //    NotesDataAccess NFDA = new NotesDataAccess(DbContext);
        //    if (aTask != null)
        //        model.TaskID = aTask.Id;
        //    NFDA.Save(model);
        //}

        //public void save(Note model, UserStory aUserStory)
        //{
        //    Logger.LogMessage("NoteBusiness.save");
        //    NotesDataAccess NFDA = new NotesDataAccess(DbContext);
        //    model.UserStoryID = aUserStory.Id;
        //    NFDA.Save(model);
        //}

        public Note CreateNote ( Opportunity aOpp)
        {
            return new Note() { Opportunity = aOpp, OpportunityId = aOpp.ID, When = DateTime.Now};
        }

        public int Save(Note aModel, Opportunity aOpp = null )
        {
            var lOppId = aModel.OpportunityId;
            if (aOpp != null)
                lOppId = aOpp.ID;

            string FuncName = $"{ClassName}Save (Note {aModel.Memo}, Opp {lOppId})";
            Logger.LogMessage(FuncName);
            var NDA = new NotesDataAccess(DbContext);
            if ( aOpp != null )
                aModel.OpportunityId = aOpp.ID;
            return NDA.Save(aModel);
        }

        public int Save(Note aModel)
        {
            string FuncName = $"{ClassName}Save (Note {aModel.Memo})";
            Logger.LogMessage(FuncName);
            var NDA = new NotesDataAccess(DbContext);
            return NDA.Save(aModel);
        }

        //public void save(Note model, Company aCompany)
        //{
        //    Logger.LogMessage("NoteBusiness.save");
        //    NotesDataAccess NFDA = new NotesDataAccess(DbContext);
        //    model.CompanyID = aCompany.Id;
        //    NFDA.Save(model);
        //}
        //public Note getNote(int id)
        //{
        //    Logger.LogMessage("NoteBusiness.getNote : " + id);
        //    NotesDataAccess NDA = new NotesDataAccess(DbContext);
        //    return NDA.GetNoteById(id);
        //}

        //public IEnumerable<Note> getNoteByAge(int aAge, int aOrgID)
        //{
        //    NotesDataAccess NDA = new NotesDataAccess(DbContext);
        //    return NDA.GetNotesByAge(aAge, aOrgID);

        //}

        //public IEnumerable<Note> getNotePage(int page, int? sourceId)
        //{
        //    Logger.LogMessage("NoteBusiness.getNotePage : " + page);
        //    NotesDataAccess NDA = new NotesDataAccess(DbContext);
        //    return NDA.GetNotes();

        //}

        //public IEnumerable<News_Flattened> GetRawNewsData(int aUserID)
        //{
        //    int? aPage = null;
        //    //int aProjID = 0;
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create(DbContextScopeOption.ForceCreateNew))
        //    {
        //        Employee lEmp = new EmployeeBusiness(lScope).GetEmployeeById(aUserID);
        //        if (lEmp == null)
        //            return new List<News_Flattened>();

        //        var lNewsData = new NoteBusiness(lScope).BuildNewsSnapShot(lEmp.OrgID, null);
        //        //Logger.LogStats("HomeController.Note", lSW.ElapsedMilliseconds.ToString());

        //        if (lNewsData.Parent.Count > NewsPageSize * 10)
        //            lNewsData.Parent = lNewsData.Parent.GetRange(0, NewsPageSize * 10);

        //        if (aPage == null) { aPage = 1; }
        //        lNewsData.TotalItems = lNewsData.Parent.Count();
        //        lNewsData.NumberOfPages = Convert.ToInt32(Math.Ceiling((double)lNewsData.Parent.Count() / NewsPageSize));
        //        lNewsData.Parent = lNewsData.Parent.Skip(NewsPageSize * (aPage.Value - 1)).Take(NewsPageSize).ToList();
        //        lNewsData.CurrentPage = aPage.Value;

        //        var lDiff = lNewsData.TimeZoneDiff;
        //        foreach (var lNewItem in lNewsData.Parent)
        //        {
        //            lNewItem.ActionDateTime = lNewItem.ActionDateTime.AddHours(lDiff);
        //            lNewItem.TimeAgo = TimeUtils.TimeAgo(lNewItem.ActionDateTime);
        //        }
        //        foreach (var lNewItem in lNewsData.Children)
        //        {
        //            lNewItem.ActionDateTime = lNewItem.ActionDateTime.AddHours(lDiff);
        //            lNewItem.TimeAgo = TimeUtils.TimeAgo(lNewItem.ActionDateTime);
        //        }

        //        List<News_Flattened> lResults = lNewsData.Parent;
        //        lResults.AddRange(lNewsData.Children);

        //        return lResults.AsEnumerable();
        //    }
        //}

        //public IEnumerable<Note> GetNotes()
        //{
        //    NotesDataAccess NDA = new NotesDataAccess(DbContext);
        //    return NDA.GetNotes();
        //}

        //public Note DeleteNote(int id)
        //{
        //    NotesDataAccess NDA = new NotesDataAccess(DbContext);
        //    return NDA.DeleteNote(id);
        //}


        //public string GetName(Common.NewsSnapShot aSnapShot, Common.Note aFeed)
        //{
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create(DbContextScopeOption.ForceCreateNew))
        //    {

        //        try
        //        {
        //            if (aFeed.SourceType == Common.Note.SourceTypes.ADDEDCOMMENTPROJECT || aFeed.SourceType == Common.Note.SourceTypes.ADDEDPROJECT)
        //            {
        //                var lProject = new ProjectBusiness(lScope).GetProjectById(aFeed.SourceId, false);
        //                if (lProject != null)
        //                {
        //                    aSnapShot.ProjectValueStream(aFeed.SourceId, lProject.ValueStreamType);
        //                    return lProject.ProjectName;
        //                }
        //            }
        //            else if (aFeed.SourceType == Common.Note.SourceTypes.ADDEDTASK || aFeed.SourceType == Common.Note.SourceTypes.ADDEDCOMMENTWORK)
        //                return new WorkBusiness(lScope).GetWorkByID(aFeed.SourceId).Name;

        //            return "Unknown";
        //        }
        //        catch (Exception)
        //        {
        //            return "Big Problem";
        //        }
        //    }
        //}
    }
}
