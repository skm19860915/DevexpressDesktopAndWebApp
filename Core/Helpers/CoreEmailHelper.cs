using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading;
using BlitzerCore.Models;
using BlitzerCore.Utilities;

namespace BlitzerCore.Helpers
{
    public class CoreEmailHelper : IEmailHelper
    {
        const string ClassName = "CoreEmailHelper::";

        const string SENDERSNAME = "Eze2Travel";
        public const string ADMIN_EMAIL_1 = "eric@eze2travel.com";
        public const string ADMIN_EMAIL_2 = "Silke@eze2travel.com";
        public const string NewLine = "<br/>";
        public IConfiguration Configuration { get; }
        public CoreEmailHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public static void SendEmail(string aHeader, string aMessage)
        //{
        //    const string lDefaultEmailAddr = "publicericwatson@gmail.com";

        //    SendEmail(lDefaultEmailAddr, aHeader, aMessage);
        //}

        //public static void SendEmail(string aEmailAddr, string aHeader, string aMessage)
        //{
        //    List<string> lTo = new List<string>();
        //    List<string> lCC = new List<string>();
        //    lTo.Add(aEmailAddr);

        //    SendEmail(lTo, lCC, aHeader, aMessage);
        //}

        //public static bool SendPasswordReset(int userID, string userEmail, string urlRequest, string userName)
        //{
        //    try
        //    {
        //        string lHeader = "Your Execution Solution Password Reset";
        //        string lBody = string.Format("Dear {0}\n <br><br> We will reset your password, please click on the below link to complete your password reset:\n <br><br> <a href =\"{1}\" title=\"ChangePassword\">Click here to reset your password</a>", userName, urlRequest);
        //        List<string> lMailTo = new List<string>();
        //        lMailTo.Add(userEmail);
        //        SendEmailAsync(lMailTo, new List<string>(), lHeader, lBody);
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //        Logger.LogException("Failed to send reset password email", ex);
        //        return false;
        //    }
        //    return true;
        //}

        public string Footer()
        {
            return "<br/><br/><a href = \"" + Configuration["AppSettings:AppURL"] + "\"><i>Powered by " + Configuration["AppSettings:AppName"] + "</i></a>";
        }

        public static void SendSystemEmail(string aHeader, string aMessage)
        {
            var AdminEmail = "Eric@eze2travel.com";
            var AdminPass = "Atoe!6315";
            System.Net.Mail.MailMessage lMail = new System.Net.Mail.MailMessage();
            lMail.From = new System.Net.Mail.MailAddress(AdminEmail, SENDERSNAME);
            lMail.Body = aMessage;
            lMail.IsBodyHtml = true;

            lMail.To.Add(ADMIN_EMAIL_1);

            var lHost = "[" + System.Net.Dns.GetHostName() + "] ";
            var lEnv = "PROD " + lHost;
            if (System.Diagnostics.Debugger.IsAttached == true)
                lEnv = "DEV " + lHost;
            lMail.Subject = lEnv + aHeader;

            using (SmtpClient smtp = new SmtpClient("smtp.office365.com"))
            {
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
                smtp.EnableSsl = true;
                smtp.Send(lMail);
            }
        }

        public void SendEmail(List<string> aMailTo, List<string> aCCs, string aHeader, string aMessage, bool aSendToSupport = true)
        {
            string FuncName = ClassName + "SendEmail -";

            if (System.IO.Directory.GetCurrentDirectory().Contains("NUnit"))
            {
                Logger.LogDebug($"{FuncName} Not sending emails in Unit Testing");
                return;
            }

            var AdminEmail = Configuration["AppSettings:Email"];
            var AdminPass = Configuration["AppSettings:Password"];
            Logger.LogInfo("Admin Email : " + AdminEmail);
            System.Net.Mail.MailMessage lMail = new System.Net.Mail.MailMessage();
            lMail.From = new System.Net.Mail.MailAddress(AdminEmail, SENDERSNAME);
            lMail.Subject = aHeader;
            lMail.Body = aMessage;
            lMail.IsBodyHtml = true;

            var lCCs = aCCs;
            if (aCCs == null)
                lCCs = new List<string>();

            var lSendTestEmail = isDebugMode();
            if (lSendTestEmail)
            {
                lMail.To.Add(ADMIN_EMAIL_1);
                //lMail.To.Add(ADMIN_EMAIL_2);

                var lEnv = "PROD";
                if (System.Diagnostics.Debugger.IsAttached == true)
                    lEnv = "DEV";

                lMail.Body += Footer();

                lMail.Body += "<br/><br/>" + lEnv + "ENV : Emails would have been sent to -> ";
                foreach (var lAddr in aMailTo)
                    lMail.Body += lAddr + " ";
            }
            else
            {
                foreach (var CC in lCCs)
                    lMail.CC.Add(CC);
                foreach (var MailTo in aMailTo)
                {
                    try
                    {
                        if (MailTo != null && MailTo.Length > 0)
                            lMail.To.Add(MailTo);
                    }
                    catch (Exception e)
                    {
                        Logger.LogException($"User Email Address is invalid {MailTo}", e);
                    }
                }
                lMail.Body += Footer();
            }

            if ( aSendToSupport == true )
                lMail.Bcc.Add("Support@eze2travel.com");

            foreach (var lAddr in lMail.To)
                Logger.LogInfo("   To : " + lAddr);

            Logger.LogDebug("SMTP Addr :" + Configuration["AppSettings:SmtpAddress"].ToString());
            try
            {
                using (SmtpClient smtp = new SmtpClient(Configuration["AppSettings:SmtpAddress"].ToString()))
                {
                    smtp.Port = Convert.ToInt32(Configuration["AppSettings:SmtpPort"].ToString());
                    smtp.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
                    smtp.EnableSsl = true;
                    smtp.Send(lMail);
                }
            } catch (Exception e )
            {
                Logger.LogWarning("Failed to Send Email : " + e.Message);
            }
        }

        public void SendEmailAsync(string aMailTo, string aHeader, string aMessage)
        {
            List<string> lTo = new List<string>();
            List<string> lCC = new List<string>();

            lTo.Add(aMailTo);
            SendEmailAsync(lTo, lCC, aHeader, aMessage);
        }

        public void SendEmailAsync(List<string> aMailTo, List<string> aCCs, string aHeader, string aMessage)
        {
            try
            {
                System.Threading.Tasks.Task.Run(async () =>
                {
                    var AdminEmail = Configuration["AppSettings:Email"];
                    var AdminPass = Configuration["AppSettings:Password"];

                    if (AdminEmail == null && AdminPass == null)
                        return;

                    System.Net.Mail.MailMessage lMail = new System.Net.Mail.MailMessage();
                    lMail.From = new System.Net.Mail.MailAddress(AdminEmail, SENDERSNAME);
                    lMail.Subject = aHeader;
                    lMail.Body = aMessage;
                    lMail.IsBodyHtml = true;
                    if (isDebugMode())
                    {
                        lMail.To.Add(ADMIN_EMAIL_1);
                        //lMail.To.Add(ADMIN_EMAIL_2);

                        var lEnv = "PROD";
                        if (System.Diagnostics.Debugger.IsAttached == true)
                            lEnv = "DEV";
                        //if (NetUtilities.getHostName().ToUpper() == "SERVER")
                        //    lEnv = "QA";

                        lMail.Body += Footer();

                        lMail.Body += "<br/><br/>" + lEnv + "ENV : Emails would have been sent to -> ";
                        foreach (var lAddr in aMailTo)
                            lMail.Body += lAddr + " ";
                    }
                    else
                    {
                        foreach (var CC in aCCs)
                            lMail.CC.Add(CC);
                        foreach (var MailTo in aMailTo)
                            lMail.To.Add(MailTo);
                    }

                    using (SmtpClient smtp = new SmtpClient(Configuration["AppSettings:SmtpAddress"].ToString()))
                    {
                        smtp.Port = Convert.ToInt32(Configuration["AppSettings:SmtpPort"].ToString());
                        smtp.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(lMail);
                    }
                });
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                //Logger.LogException("Failed to send email", ex);
            }
        }

        public bool SendConfirmation(int userID, string userEmail, string urlRequest, string userName)
        {
            try
            {
                string lHeader = "Email confirmation";
                string lBody = string.Format("Dear {0}\n <br><br> Thank you for your registration, please click on the below link to complete your registration:\n <br><br> <a href =\"{1}\" title=\"User Email Confirm\">Click here to confirm your email</a>", userName, urlRequest);
                SendEmailAsync(userEmail, lHeader, lBody);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                BlitzerCore.Utilities.Logger.LogException("Failed to send registration email", ex);
                return false;
            }
            return true;
        }

        //public static bool SendNewEmployeeNotification(string userEmail, string userName)
        //{
        //    try
        //    {
        //        List<string> lMailTo = new List<string>();

        //        var lSubject = "New Account Notification";
        //        var lBody = "Dear " + userName +"\n <br><br> You have a new account on the "+ ConfigurationManager.AppSettings["AppName"].ToString() + ". The link to the site is "+ ConfigurationManager.AppSettings["AppURL"].ToString() + " and your password is " + Common.UserDefines.DEFAULT_PASSWORD;
        //        lMailTo.Add(userEmail);
        //        SendEmailAsync(lMailTo, new List<string>(), lSubject, lBody);
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //        Logger.LogException("Failed to send registration email", ex);
        //        return false;
        //    }
        //    return true;
        //}

        //public static bool SendNewWorkNotification(int userID, string userEmail, string userName, string aURL, Work aWork)
        //{
        //    try
        //    {
        //        var AdminEmail = ConfigurationManager.AppSettings["Email"].ToString();
        //        var AdminPass = ConfigurationManager.AppSettings["Password"].ToString();
        //        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(new System.Net.Mail.MailAddress(AdminEmail, SENDERSNAME), new System.Net.Mail.MailAddress(userEmail));
        //        m.Subject = "New Work Notification";

        //        m.Body = string.Format("Dear {0}\n <br><br> You receive a new work notification :\n <br>{2}<br> <a href =\"{1}\" title=\"View Tasks\">View Tasks</a>", userName, aURL, aWork.Name);
        //        m.IsBodyHtml = true;
        //        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SmtpAddress"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString()));
        //        smtp.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
        //        smtp.EnableSsl = true;
        //        smtp.Send(m);
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //        Logger.LogException("Failed to send registration email", ex);
        //        return false;
        //    }
        //    return true;
        //}
        //public static bool SendSlippageNotification(string aURL, Work aWork, UserTypes.SlippageState aState)
        //{
        //    try
        //    {
        //        string lSubject = "Slippage Warning";
        //        string lMessageBody = "Warning";

        //        if (aState == UserTypes.SlippageState.LATE)
        //        {
        //            lSubject = "Slippage Notification";
        //            lMessageBody = "Notification";
        //        }

        //        var AdminEmail = ConfigurationManager.AppSettings["Email"].ToString();
        //        var AdminPass = ConfigurationManager.AppSettings["Password"].ToString();
        //        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(new System.Net.Mail.MailAddress(AdminEmail, SENDERSNAME), new System.Net.Mail.MailAddress(aWork.Owner.Primary.Email));
        //        m.Subject = lSubject;

        //        m.Body = string.Format("Dear {0}\n <br><br> You receive a Slippage {3} :\n <br>{2}<br> <a href =\"{1}\" title=\"User Email Confirm\">Click here to confirm your email</a>", aWork.Owner.Primary.FullName, aURL, aWork.Name, lMessageBody);
        //        Logger.LogDebug("Sending Email Notification -> " + m.Body + " to : " + m.To.ElementAt(0).Address);
        //        m.IsBodyHtml = true;
        //        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SmtpAddress"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString()));
        //        smtp.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
        //        smtp.EnableSsl = true;
        //        smtp.Send(m);
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //        Logger.LogException("Failed to send registration email", ex);
        //        return false;
        //    }
        //    return true;
        //}


        //public static bool SendIssuerSlippageNotification(string aURL, Work aWork, UserTypes.SlippageState aState)
        //{
        //    if (aWork.Issuer.EmployeeID == aWork.Owner.Primary.EmployeeID)
        //        return false;

        //    try
        //    {
        //        string lSubject = "Slippage Warning";
        //        string lMessageBody = "Warning";

        //        if (aState == UserTypes.SlippageState.LATE)
        //        {
        //            lSubject = "Slippage Notification";
        //            lMessageBody = "Notification";
        //        }

        //        var AdminEmail = ConfigurationManager.AppSettings["Email"].ToString();
        //        var AdminPass = ConfigurationManager.AppSettings["Password"].ToString();
        //        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(new System.Net.Mail.MailAddress(AdminEmail, SENDERSNAME), new System.Net.Mail.MailAddress(aWork.Issuer.Email));
        //        m.Subject = lSubject;

        //        m.Body = string.Format("Dear {0}\n <br><br> You are receiving a Slippage {3} which you assigned to {4} :\n <br>{2}<br> <a href =\"{1}\" title=\"User Email Confirm\">Click here to view tasks</a>", aWork.Issuer.FullName, aURL, aWork.Name, lMessageBody, aWork.Owner.Primary.FullName);
        //        m.IsBodyHtml = true;
        //        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SmtpAddress"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"].ToString()));
        //        smtp.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);
        //        smtp.EnableSsl = true;
        //        smtp.Send(m);
        //    }
        //    catch (Exception ex)
        //    {
        //        var message = ex.Message;
        //        Logger.LogException("Failed to send registration email", ex);
        //        return false;
        //    }
        //    return true;
        //}


        //void populateSubject(MailMessage aMailItem, Meeting aMeeting, string aPrefix)
        //{
        //    aMailItem.Subject = aPrefix + " : " + aMeeting.Name;
        //    List<Work> lT = new List<Work>();
        //    lT.ElementAt(0);
        //}

        //string populateAttendees(Meeting aMeeting, string aType)
        //{
        //    string lResult = "<b>" + aType + "</b> : ";

        //    foreach (Employee aEmp in aMeeting.Attendees)
        //    {
        //        lResult += aEmp.FullName;
        //        if (aEmp != aMeeting.Attendees.ElementAt(aMeeting.Attendees.Count - 1))
        //            lResult += ",  ";
        //    }

        //    lResult += "\n";

        //    return lResult;
        //}

        public static bool isDebugMode()
        {
            if (System.Diagnostics.Debugger.IsAttached == true)
                return true;

            return false;
        }

        //void populateDecissions(MailMessage aMailItem, Meeting aMeeting)
        //{
        //    if (aMeeting.Decisions.Count == 0)
        //        return;

        //    aMailItem.Body += "<h2><font color=\"Purple\" >Decisions</font></h2>";

        //    // get the list of users first
        //    List<string> lSubjects = new List<string>();
        //    Dictionary<string, List<Decision>> lDecbySubject = new Dictionary<string, List<Decision>>();
        //    foreach (Decision lDecission in aMeeting.Decisions)
        //    {
        //        if (lDecission == null || lDecission.Subject == null || lDecission.Description == null)
        //            continue;

        //        if (lSubjects.Contains(lDecission.Subject) == false)
        //            lSubjects.Add(lDecission.Subject);

        //        if (lDecbySubject.ContainsKey(lDecission.Subject) == false)
        //            lDecbySubject[lDecission.Subject] = new List<Decision>();

        //        List<Decision> lDecList = lDecbySubject[lDecission.Subject];
        //        lDecList.Add(lDecission);
        //    }


        //    foreach (string lSubject in lSubjects)
        //    {
        //        aMailItem.Body += "<ul><font color=\"blue\" size=\"5\"><b>" + lSubject + "</b></font>";
        //        aMailItem.Body += "<ul>";
        //        foreach (Decision lDecission in lDecbySubject[lSubject])
        //        {
        //            aMailItem.Body += "<li><font size=\"3\">" + lDecission.Description + "</Font></li>";
        //        }
        //        aMailItem.Body += "</ul></ul>";
        //    }
        //}

        //void populateTopics(MailMessage aMailItem, Meeting aMeeting)
        //{
        //    aMailItem.Body += "<h2><font color=\"Purple\" >Topics</font></h2>";

        //    aMailItem.Body += "<ul><table width=80%>";
        //    foreach (Decision lDecission in aMeeting.Decisions)
        //    {
        //        aMailItem.Body += "<tr><li><td width=70%>" + lDecission.Subject + "</font></td><td><b>" + lDecission.Duration + "</b></td></li></tr>";
        //    }
        //    aMailItem.Body += "</table></ul>";
        //}

        //public static void CreateAppointment(Meeting aMeeting, MessageBody aBody, string aOwnerEmail)
        //{
        //    Thread lBackGround = new Thread(() => IntCreateAppointment(aMeeting, aBody, aOwnerEmail));
        //    lBackGround.IsBackground = true;
        //    lBackGround.Start();

        //}

        //public  static void IntCreateAppointment(Meeting aMeeting, MessageBody aBody, string aOwnerEmail)
        //{
        //    try
        //    {
        //        var AdminEmail = ConfigurationManager.AppSettings["Email"].ToString();
        //        var AdminPass = ConfigurationManager.AppSettings["Password"].ToString();

        //        Microsoft.Exchange.WebServices.Data.ExchangeService service = new ExchangeService();
        //        service.Credentials = new System.Net.NetworkCredential(AdminEmail, AdminPass);

        //        service.AutodiscoverUrl(AdminEmail, RedirectionUrlValidationCallback);

        //        Microsoft.Exchange.WebServices.Data.Appointment appointment =
        //            new Microsoft.Exchange.WebServices.Data.Appointment(service);
        //        appointment.Subject = aMeeting.Name + " Meeting";
        //        appointment.Start = aMeeting.Date;
        //        appointment.End = appointment.Start.AddHours(1);
        //        appointment.Body = aBody;
        //        foreach (var lAttendee in aMeeting.Invites)
        //            appointment.RequiredAttendees.Add(lAttendee.Email);
        //        appointment.RequiredAttendees.Add(aOwnerEmail);
        //        appointment.Save(SendInvitationsMode.SendToAllAndSaveCopy);
        //    }
        //    catch (Exception e)
        //    {
        //        Logger.LogException("Failed to create Appointment ", e);
        //    }
        //}


        //private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        //{
        //    // The default for the validation callback is to reject the URL.
        //    bool result = false;

        //    Uri redirectionUri = new Uri(redirectionUrl);

        //    // Validate the contents of the redirection URL. In this simple validation
        //    // callback, the redirection URL is considered valid if it is using HTTPS
        //    // to encrypt the authentication credentials. 
        //    if (redirectionUri.Scheme == "https")
        //    {
        //        result = true;
        //    }
        //    return result;
        //}


    }
}

