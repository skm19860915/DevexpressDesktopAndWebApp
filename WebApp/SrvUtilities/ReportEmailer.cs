using System;
using BlitzerCore.Models;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using BlitzerCore.Utilities;
using Microsoft.Extensions.Configuration;

namespace WebApp.SrvUtilities
{
    public class ReportEmailer : IReportEmailer
    {
        IConfiguration mConfiguration;

        public ReportEmailer(IConfiguration aConfig)
        {
            mConfiguration = aConfig;
        }

        //public void DailyAgendaEmail(Employee aEmp)
        //{
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create(DbContextScopeOption.ForceCreateNew))
        //    {
        //        StringWriter lXMLStream = new StringWriter();
        //        WorkBusiness lWB = new WorkBusiness(lScope);
        //        ProjectBusiness lPB = new ProjectBusiness(lScope);

        //        if (aEmp.Role == Roles.Guess)
        //            return;

        //        XmlTextWriter textWriter = new XmlTextWriter(lXMLStream);
        //        var lOpps = new OpportunityBusiness(lScope).GetAllOpportunities(aEmp.EmployeeID).Where(x => x.isValid && x.Issuer.OrgID == aEmp.OrgID).OrderBy(x => x.StageID);
        //        var lProjects = lPB.GetAllProjects().Where(x => x.isActive && x.Issuer.OrgID == aEmp.OrgID && x.Works != null && x.Works.Count > 0 && x.ValueStreamType == Project.ValueStreamTypes.PROJECT
        //        && x.Works.Where(y => y.isActive && y.Owner.Primary.EmployeeID == aEmp.EmployeeID).Count() > 0);

        //        try
        //        {
        //            textWriter.Formatting = Formatting.Indented;
        //            //Open the xml document to write
        //            textWriter.WriteStartDocument();
        //            //write element
        //            textWriter.WriteStartElement("Agenda");

        //            /* Start of the Completed portion */
        //            textWriter.WriteStartElement("Completed");
        //            var lData = lWB.getUserWork(aEmp.EmployeeID);
        //            DateTime lStart = DateTime.Now.AddDays(1);
        //            DateTime lEnd = DateTime.Now.AddDays(-1);

        //            while (lEnd.DayOfWeek != DayOfWeek.Friday)
        //                lEnd = lEnd.AddDays(1);
        //            lEnd = new DateTime(lEnd.Year, lEnd.Month, lEnd.Day, 23, 59, 0);
        //            lStart = lEnd.AddDays(-6);
        //            //while (lStart.DayOfWeek != DayOfWeek.Friday)
        //            //    lStart = lStart.AddDays(-1);

        //            var lCompletedData = lData.Where(x => x.Status == Work.StatusType.COMPLETED && x.CompletedDate >= lStart && x.CompletedDate < lEnd && x.CompletedDate != null);

        //            foreach (Work lWork in lCompletedData.OrderByDescending(x => x.CreatedAt))
        //            {
        //                //write attribute
        //                textWriter.WriteStartElement("Work");
        //                textWriter.WriteElementString("Name", lWork.Name);
        //                var lPName = "No Project";
        //                if (lWork.Project != null)
        //                    lPName = lWork.Project.ProjectName;
        //                textWriter.WriteElementString("ProjectName", lPName);
        //                textWriter.WriteElementString("Completed", lWork.CompletedDate.Value.ToString());
        //                textWriter.WriteElementString("Age", TimeUtils.TimeAgo(lWork.CreatedAt));
        //                textWriter.WriteEndElement(); // end of Work
        //            }
        //            textWriter.WriteEndElement(); // end of Completed Data




        //            textWriter.WriteStartElement("Pipeline");

        //            foreach (Opportunity lOpp in lOpps.Where(x => x.Works.Count > 0))
        //            {
        //                var lOneWork = lOpp.Works.FirstOrDefault();
        //                if (lOpp.Works.Where(x => x.isActive == true).Select(x => x.Owner.Primary.EmployeeID).Contains(aEmp.EmployeeID) == false)
        //                    continue;

        //                //write attribute
        //                textWriter.WriteStartElement("Opportunity");
        //                textWriter.WriteElementString("Name", lOpp.ProjectName);
        //                textWriter.WriteElementString("Stage", lOpp.StageID.ToString());
        //                textWriter.WriteStartElement("ActionItems");
        //                foreach (Work lWork in lOpp.Works)
        //                {
        //                    // Filter out work not assigned to the report user
        //                    if (lWork.Owner.Primary.EmployeeID != aEmp.EmployeeID || lWork.isActive == false)
        //                        continue;

        //                    string lDeadline = "";
        //                    if (lWork.Deadline != null && lWork.Deadline.HasValue)
        //                        lDeadline = lWork.Deadline.Value.ToShortDateString();
        //                    textWriter.WriteStartElement("Work");
        //                    textWriter.WriteElementString("Name", lWork.Name);
        //                    textWriter.WriteElementString("Deadline", lDeadline);
        //                    textWriter.WriteElementString("Age", TimeUtils.TimeAgo(lWork.CreatedAt));
        //                    textWriter.WriteEndElement(); // end of Work
        //                }
        //                textWriter.WriteEndElement(); // End of Action Items
        //                textWriter.WriteEndElement();   // Endof Opportunity
        //            }
        //            textWriter.WriteEndElement();

        //            /* Start of the project portion */
        //            textWriter.WriteStartElement("Projects");
        //            lData = lWB.getUserWork(aEmp.EmployeeID);

        //            while (lStart.DayOfWeek != DayOfWeek.Friday)
        //                lStart = lStart.AddDays(1);
        //            while (lEnd.DayOfWeek != DayOfWeek.Friday)
        //                lEnd = lEnd.AddDays(-1);

        //            var lWorkData = lData.Where(x => x.isActive && (x.Deadline == null || (x.Deadline >= x.StartDate.Date && x.Deadline < lEnd.Date)));

        //            foreach (Project lProjct in lProjects.OrderBy(x => x.CreatedAt))
        //            {
        //                //write attribute
        //                textWriter.WriteStartElement("Projct");
        //                textWriter.WriteElementString("ProjectName", lProjct.ProjectName);
        //                textWriter.WriteStartElement("ActionItems");
        //                foreach (Work lWork in lProjct.Works)
        //                {
        //                    // Filter out work not assigned to the report user
        //                    if (lWork.Owner.Primary.EmployeeID != aEmp.EmployeeID || lWork.isActive == false)
        //                        continue;

        //                    string lDeadline = "";
        //                    if (lWork.Deadline != null && lWork.Deadline.HasValue)
        //                        lDeadline = lWork.Deadline.Value.ToShortDateString();
        //                    textWriter.WriteStartElement("Work");
        //                    textWriter.WriteElementString("Name", lWork.Name);
        //                    textWriter.WriteElementString("Deadline", lDeadline);
        //                    textWriter.WriteElementString("Age", TimeUtils.TimeAgo(lWork.CreatedAt));
        //                    textWriter.WriteEndElement(); // end of Work
        //                }
        //                textWriter.WriteEndElement(); // End of Action Items
        //                textWriter.WriteEndElement(); // end of Project
        //            }
        //            textWriter.WriteEndElement(); // end of Pipeline Data

        //            textWriter.WriteEndElement(); // end of Agenda
        //        }
        //        catch (Exception e1)
        //        {
        //            Logger.LogException("Failed to create pipeline xml", e1);
        //        }

        //        try
        //        {
        //            XslCompiledTransform xslt = new XslCompiledTransform();
        //            StringWriter lStringWriter = new StringWriter();

        //            string lXSLFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\AgendaStyle.xsl";
        //            string lCSSFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\AgendaStyleSheet.css";
        //            StreamReader lCSSStream = new StreamReader(lCSSFileName);
        //            string lCSSData = lCSSStream.ReadToEnd();
        //            xslt.Load(lXSLFileName);
        //            string lXML = lXMLStream.ToString();

        //            StreamWriter lDataWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\AgendaData.xml");
        //            lDataWriter.Write(lXML);
        //            lDataWriter.Flush();
        //            lDataWriter.Close();

        //            //XMLStream
        //            XmlReader lXMLReader = XmlReader.Create(new StringReader(lXML));
        //            xslt.Transform(lXMLReader, null, lStringWriter);
        //            string lHTML = lStringWriter.ToString();
        //            lHTML = lHTML.Replace("[[InlineCSS]]", lCSSData);

        //            lDataWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\AgendaResult.html");
        //            lDataWriter.Write(lHTML);
        //            lDataWriter.Flush();
        //            lDataWriter.Close();

        //            if (System.Diagnostics.Debugger.IsAttached == false)
        //                Utilities.EmailHelper.SendEmail(aEmp.Email, "Your Week's Agenda", lHTML);
        //        }
        //        catch (Exception e)
        //        {
        //            Logger.LogException("Failed to create meeting email", e);
        //        }
        //    }
        //}

        //public void NewTaskEmail(Work aWork)
        //{
        //    StringWriter lXMLStream = new StringWriter();

        //    XmlTextWriter textWriter = new XmlTextWriter(lXMLStream);

        //    try
        //    {
        //        textWriter.Formatting = Formatting.Indented;
        //        //Open the xml document to write
        //        textWriter.WriteStartDocument();
        //        //write element
        //        textWriter.WriteStartElement("Work");

        //        //write attribute
        //        textWriter.WriteElementString("Name", aWork.Name);
        //        textWriter.WriteElementString("ID", aWork.ID.ToString());
        //        if (aWork.Project != null)
        //        {
        //            textWriter.WriteElementString("Project", aWork.Project.ProjectName);
        //            textWriter.WriteElementString("ProjectID", aWork.Project.ProjectID.ToString());
        //        }

        //        string lDueDate = "";
        //        if (aWork.Deadline == null )
        //            lDueDate = "";
        //        else
        //            lDueDate = aWork.Deadline.ToString();

        //        if (lDueDate.Length == 0)
        //            lDueDate = "N/A";

        //        string lDescription = "None";
        //        if (aWork.Description != null && aWork.Description.Length > 0)
        //            lDescription = aWork.Description;

        //        textWriter.WriteElementString("Issuer", aWork.Issuer.FullName);
        //        textWriter.WriteElementString("Created", aWork.CreatedAt.ToString());
        //        textWriter.WriteElementString("DueDate", lDueDate);
        //        textWriter.WriteElementString("Description", lDescription);
        //        textWriter.WriteEndElement();
        //    }
        //    catch (Exception e1)
        //    {
        //        Logger.LogException("Failed to create pipeline xml", e1);
        //    }

        //    try
        //    {
        //        XslCompiledTransform xslt = new XslCompiledTransform();
        //        StringWriter lStringWriter = new StringWriter();

        //        string lXSLFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\NewTask.xsl";
        //        string lCSSFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\NewTask.css";
        //        StreamReader lCSSStream = new StreamReader(lCSSFileName);
        //        string lCSSData = lCSSStream.ReadToEnd();
        //        xslt.Load(lXSLFileName);
        //        string lXML = lXMLStream.ToString();

        //        StreamWriter lDataWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\NewTask.xml");
        //        lDataWriter.Write(lXML);
        //        lDataWriter.Flush();
        //        lDataWriter.Close();

        //        //XMLStream
        //        XmlReader lXMLReader = XmlReader.Create(new StringReader(lXML));
        //        xslt.Transform(lXMLReader, null, lStringWriter);
        //        string lHTML = lStringWriter.ToString();
        //        lHTML = lHTML.Replace("[[InlineCSS]]", lCSSData);

        //        lDataWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\NewTask.html");
        //        lDataWriter.Write(lHTML);
        //        lDataWriter.Flush();
        //        lDataWriter.Close();

        //        List<string> lTo = new List<string>();
        //        List<string> lCC = new List<string>();
        //        lTo.Add(aWork.Owner.Primary.Email);

        //        Utilities.EmailHelper.SendEmailAsync(lTo, lCC, "New Task Notification", lHTML);
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.LogException("Failed to create meeting email", e);
        //    }
        //}
        //public void TaskCompletedEmail(Work aWork)
        //{
        //    StringWriter lXMLStream = new StringWriter();

        //    XmlTextWriter textWriter = new XmlTextWriter(lXMLStream);

        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create(DbContextScopeOption.ForceCreateNew))
        //    {

        //        try
        //        {
        //            textWriter.Formatting = Formatting.Indented;
        //            //Open the xml document to write
        //            textWriter.WriteStartDocument();
        //            //write element
        //            textWriter.WriteStartElement("Work");

        //            //write attribute
        //            textWriter.WriteElementString("Name", aWork.Name);
        //            if (aWork.Project != null)
        //                textWriter.WriteElementString("Project", aWork.Project.ProjectName);
        //            textWriter.WriteElementString("Owner", aWork.Owner.Primary.FullName);
        //            textWriter.WriteElementString("Created", aWork.CreatedAt.ToString());
        //            textWriter.WriteElementString("DueDate", aWork.Deadline.ToString());
        //            textWriter.WriteElementString("Description", aWork.Description);

        //            // Write the Comments Out
        //            textWriter.WriteStartElement("Comments");
        //            NewsFeedBusiness lNFB = new NewsFeedBusiness(lScope);
        //            var lNewsData = lNFB.GetNewsFeeds()
        //                .Where(x => x.SourceId == aWork.ID && x.SourceTypeId == NewsFeed.SourceTypes.ADDEDCOMMENTWORK)
        //                .OrderByDescending(x => x.ActionDateTime);
        //            NewsFeed lNews = lNewsData.FirstOrDefault();
        //            foreach (var lNewItems in lNewsData)
        //            {
        //                textWriter.WriteStartElement("Comment");
        //                // Filter out work not assigned to the report user
        //                textWriter.WriteElementString("Owner", lNewItems.Employee.FullName);
        //                textWriter.WriteElementString("Date", lNewItems.ActionDateTime.ToString());
        //                textWriter.WriteElementString("Comment", lNewItems.News);
        //                textWriter.WriteEndElement(); // end of Comment
        //            }
        //            // end of Comments
        //            textWriter.WriteEndElement();

        //            // end of Work
        //            textWriter.WriteEndElement();
        //        }
        //        catch (Exception e1)
        //        {
        //            Logger.LogException("Failed to create pipeline xml", e1);
        //        }

        //        try
        //        {
        //            XslCompiledTransform xslt = new XslCompiledTransform();
        //            StringWriter lStringWriter = new StringWriter();

        //            string lXSLFileName = AppDomain.CurrentDomain.BaseDirectory +
        //                                  "\\Content\\Templates\\TaskCompleted.xsl";
        //            string lCSSFileName = AppDomain.CurrentDomain.BaseDirectory +
        //                                  "\\Content\\Templates\\TaskCompleted.css";
        //            StreamReader lCSSStream = new StreamReader(lCSSFileName);
        //            string lCSSData = lCSSStream.ReadToEnd();
        //            xslt.Load(lXSLFileName);
        //            string lXML = lXMLStream.ToString();

        //            StreamWriter lDataWriter =
        //                new StreamWriter(AppDomain.CurrentDomain.BaseDirectory +
        //                                 "\\Content\\Templates\\TaskCompleted.xml");
        //            lDataWriter.Write(lXML);
        //            lDataWriter.Flush();
        //            lDataWriter.Close();

        //            //XMLStream
        //            XmlReader lXMLReader = XmlReader.Create(new StringReader(lXML));
        //            xslt.Transform(lXMLReader, null, lStringWriter);
        //            string lHTML = lStringWriter.ToString();
        //            lHTML = lHTML.Replace("[[InlineCSS]]", lCSSData);

        //            lDataWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory +
        //                                           "\\Content\\Templates\\TaskCompleted.html");
        //            lDataWriter.Write(lHTML);
        //            lDataWriter.Flush();
        //            lDataWriter.Close();

        //            Utilities.EmailHelper.SendEmailAsync(aWork.Issuer.Email, "Task Completed Notification", lHTML);
        //        }
        //        catch (Exception e)
        //        {
        //            Logger.LogException("Failed to create meeting email", e);
        //        }
        //    }
        //}
        //public void DocumentAddedEmail(Common.File aFile)
        //{
        //    using (IDbContextScope lScope = new DbContextScopeFactory().Create(DbContextScopeOption.ForceCreateNew))
        //    {

        //        StringWriter lXMLStream = new StringWriter();

        //        XmlTextWriter textWriter = new XmlTextWriter(lXMLStream);

        //        Employee lEmp = new EmployeeBusiness(lScope).GetEmployeeById(aFile.EmployeeID);

        //        Project lProj = null;
        //        if (aFile.ProjectID != null)
        //            lProj = new ProjectBusiness(lScope).GetProjectById(aFile.ProjectID.Value, false);
        //        try
        //        {
        //            textWriter.Formatting = Formatting.Indented;
        //            //Open the xml document to write
        //            textWriter.WriteStartDocument();
        //            //write element
        //            textWriter.WriteStartElement("Document");

        //            //write attribute
        //            textWriter.WriteElementString("Name", aFile.Name);
        //            if (lProj != null)
        //                textWriter.WriteElementString("Project", lProj.ProjectName);

        //            textWriter.WriteElementString("Url", aFile.URI);
        //            textWriter.WriteElementString("Owner", lEmp.FullName);
        //            textWriter.WriteElementString("Created", DateTime.Now.ToString());
        //            textWriter.WriteElementString("Description", aFile.Description);

        //            // end of Document
        //            textWriter.WriteEndElement();
        //        }
        //        catch (Exception e1)
        //        {
        //            Logger.LogException("Failed to create new Document xml", e1);
        //        }

        //        try
        //        {
        //            XslCompiledTransform xslt = new XslCompiledTransform();
        //            StringWriter lStringWriter = new StringWriter();

        //            string lXSLFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\DocumentAdded.xsl";
        //            string lCSSFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\DocumentAdded.css";
        //            StreamReader lCSSStream = new StreamReader(lCSSFileName);
        //            string lCSSData = lCSSStream.ReadToEnd();
        //            xslt.Load(lXSLFileName);
        //            string lXML = lXMLStream.ToString();

        //            StreamWriter lDataWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\DocumentAdded.xml");
        //            lDataWriter.Write(lXML);
        //            lDataWriter.Flush();
        //            lDataWriter.Close();

        //            //XMLStream
        //            XmlReader lXMLReader = XmlReader.Create(new StringReader(lXML));
        //            xslt.Transform(lXMLReader, null, lStringWriter);
        //            string lHTML = lStringWriter.ToString();
        //            lHTML = lHTML.Replace("[[InlineCSS]]", lCSSData);

        //            lDataWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\DocumentAdded.html");
        //            lDataWriter.Write(lHTML);
        //            lDataWriter.Flush();
        //            lDataWriter.Close();

        //            int lProjectID = aFile.ProjectID.Value;

        //            var lProject = new ProjectBusiness(lScope).GetProject(lProjectID);

        //            var ACLS = new ACLBusiness(lScope).GetACLsForProject(lProject).Where(x => x.UserID != aFile.Owner.EmployeeID);
        //            List<string> lEmailAddr = ACLS.Where(x => x.User != null).Select(x => x.User.Email).ToList();
        //            lEmailAddr.AddRange(ACLS.Where(x => x.Team != null).SelectMany(x => x.Team.Employees).Select(a => a.Email));
        //            Utilities.EmailHelper.SendEmailAsync(lEmailAddr, new List<string>(), lProject.ProjectName + " :  New Document Added", lHTML);
        //        }

        //        catch (Exception e)
        //        {
        //            Logger.LogException("Failed to create Document email", e);
        //        }
        //    }
        //}

        public void SendQuote(Contact aEmp, QuoteRequest aQuote)
        {
            StringWriter lXMLStream = new StringWriter();
            XmlTextWriter textWriter = new XmlTextWriter(lXMLStream);

            //string lURL = Defines.PROD_URL;
            //if (System.Diagnostics.Debugger.IsAttached == true)
            //lURL = Defines.DEV_URL;

            try
            {
                textWriter.Formatting = Formatting.Indented;
                //Open the xml document to write
                textWriter.WriteStartDocument();
                //write element
                textWriter.WriteStartElement("Quote");

                textWriter.WriteStartElement("Agent");
                textWriter.WriteElementString("Name", aQuote.Agent.Name);
                textWriter.WriteElementString("Email", aQuote.Agent.PrimaryEmail);
                textWriter.WriteElementString("Phone", aQuote.Agent.PrimaryPhoneNumber);
                textWriter.WriteEndElement();

                foreach (var lFlight in aQuote.TransportationFilters)
                {
                    //// Write the Comments Out
                    //textWriter.WriteStartElement("Work");
                    //if (lSlipped.SlippedWork.Project != null)
                    //    textWriter.WriteElementString("Project", lSlipped.SlippedWork.Project.ProjectName);
                    //// Filter out work not assigned to the report user
                    //textWriter.WriteElementString("Name", lSlipped.SlippedWork.Name);
                    //textWriter.WriteElementString("URL", lURL + "/taskdetails/" + lSlipped.SlippedWork.ID.ToString());
                    //textWriter.WriteElementString("ID", lSlipped.SlippedWork.ID.ToString());
                    //textWriter.WriteStartElement("ActionType");
                    //if (lSlipped.SlippedWork.Issuer.EmployeeID != lSlipped.SlippedWork.Owner.Primary.EmployeeID)
                    //{
                    //    textWriter.WriteAttributeString("ChangeType", "Request Change");
                    //    textWriter.WriteString(lURL + "/NewDCRequest/" + lSlipped.SlippedWork.ID.ToString());
                    //}
                    //else
                    //{
                    //    textWriter.WriteAttributeString("ChangeType", "Change");
                    //    textWriter.WriteString(lURL + "/TaskDetails/" + lSlipped.SlippedWork.ID.ToString());
                    //}
                    //textWriter.WriteEndElement();
                    //textWriter.WriteElementString("Deadline", lSlipped.SlippedWork.Deadline.ToString());
                    ////textWriter.WriteElementString("TimeAgo", TimeUtils.TimeAgo(lSlipped.SlippedWork.Deadline));
                    //textWriter.WriteEndElement(); // end of Comment
                }
                // end of Work
                textWriter.WriteEndElement();
            }
            catch (Exception e1)
            {
                Logger.LogException("Failed to create Slippage email xml data file", e1);
            }

            try
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                StringWriter lStringWriter = new StringWriter();

                string lXSLFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\Quote.xsl";
                string lCSSFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\Quote.css";
                StreamReader lCSSStream = new StreamReader(lCSSFileName);
                string lCSSData = lCSSStream.ReadToEnd();
                xslt.Load(lXSLFileName);
                string lXML = lXMLStream.ToString();

                StreamWriter lDataWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\Quote.xml");
                lDataWriter.Write(lXML);
                lDataWriter.Flush();
                lDataWriter.Close();

                //XMLStream
                XmlReader lXMLReader = XmlReader.Create(new StringReader(lXML));
                xslt.Transform(lXMLReader, null, lStringWriter);
                string lHTML = lStringWriter.ToString();
                lHTML = lHTML.Replace("[[InlineCSS]]", lCSSData);

                lDataWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Content\\Templates\\Quote.html");
                lDataWriter.Write(lHTML);
                lDataWriter.Flush();
                lDataWriter.Close();

                var lUsers = new List<string>();
                lUsers.Add(aEmp.Emails[0].Address);
                new EmailHelper(mConfiguration).SendEmailAsync(lUsers, new List<string>(), "Quote", lHTML);
            }
            catch (Exception e)
            {
                Logger.LogException("Failed to create slippage email", e);
            }

        }
    }
}
