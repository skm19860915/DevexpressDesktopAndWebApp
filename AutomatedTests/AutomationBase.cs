// Generated by Selenium IDE
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using BlitzerCore.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using BlitzerCore.Utilities;

using NUnit.Framework;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.DevTools.V85.DOM;

namespace AutomatedTests
{

    public class AutomationBase : BlitzerCore.WebBots.WebBotBase
    {
        public enum ENVS {DEV, QA};

        protected IWebDriver WebBot;

        protected static ENVS ENV { get; set; }
        const string ClassName = "AutomationBase::";

        public const string DASHBOARD_XPATH = "/html/body/main/div[2]/div[1]/div[1]/a/span";
        public const string PORTAL_LINK_XPATH = "//*[@id='main_menu']/li[2]/a";

        public const string DEV_CONN_STR = "server=.\\SQLEXPRESS;Initial Catalog=DEV; Integrated Security=True;multipleactiveresultsets=True;";
        public const string QA_CONN_STR = "server=tcp:Horse;Initial Catalog=QA; Integrated Security=True;multipleactiveresultsets=True;";


        // On the main portal screen
        public const string FIRST_ROW_OPPORTUNITY = "//*[@id='devextreme0']/div/div[6]/div/table/tbody/tr[1]/td[1]";
        public const string OPPORTUNITY_XPATH = "//*[@id='devextreme0']/div/div[6]/div/table/tbody/tr[1]/td[1]";
        public const string OPPORTUNITY_SAVE_BTN = "/html/body/main/div[2]/div[2]/div[2]/form/div/div[5]/input";
        // On the quote request screen
        //*[@id="QuoteTable"]/div[3]/div/div[8]/a
        public const string QUOTEREQUEST_FIRST_ROW_EDIT_BTN = "//*[@id='QuoteTable']/div[3]/div/div[8]/a";
        public const string PULLQUOTES_BTN = "/html/body/main/div[2]/div[2]/form/div/div[5]/div[4]/div[2]/input";

        // On the Client View Screen
        public const string FIRST_HOTEL_BOT_PRICE = "//*[@id='ICQuote']/form/div[1]/div[4]/div[2]/div/div[2]/table/tbody/tr[1]/td[2]";

        public AutomationBase(IWebDriver aWebDriver = null) : base(aWebDriver)
        {
            WebBot = aWebDriver;
        }

        [SetUp]
        public void SetUp()
        {
            ChromeOptions lOptions = new ChromeOptions();
            if (Dns.GetHostName().ToUpper() == "HERCULES")
            {
                ENV = ENVS.DEV;
                CleanDb(DEV_CONN_STR);
                lOptions.AddArgument("--start-maximized");
                Logger.LogInfo("Starting Chrom Maximized");
            }
            else
            {
                ENV = ENVS.QA;
                CleanDb(QA_CONN_STR);
                lOptions.AddArgument("--headless");
                lOptions.AddArgument("--window-size=1920,1080");
                Logger.LogInfo("Started Chrome with Headless option");
            }

            WebBot = new ChromeDriver(lOptions);
            mChrome = WebBot;
        }

        [TearDown]
        protected void TearDown()
        {
            Assert.AreEqual(0, GetLogMsgTypeCounts(LogMsg.MsgTypes.Exception), "There should be no Exceptions messages in the log after test execution");
            Assert.AreEqual(0, GetLogMsgTypeCounts(LogMsg.MsgTypes.Error), "There should be no Errors Message in log after test execution");

            Close();
        }

        public static void ClickWithXPath(IWebDriver aWebBot, string aXPath)
        {
            IWebElement lElement = null;
            do
            {
                lElement = aWebBot.FindElement(By.XPath(aXPath));
                if (lElement == null)
                    Thread.Sleep(500);

            } while (lElement == null);

            lElement.Click();
        }

        public int GetLogMsgTypeCounts(LogMsg.MsgTypes aType)
        {
            string FuncName = ClassName + $"GetLogMsgTypeCounts";
            string lCntStatement =$"Select Count(*) from LogMsgs where MsgType = {(int)aType}";
            int lCnt = 0;
            try
            {
                var configurationBuilder = new ConfigurationBuilder()
                    .AddJsonFile("UnitTesting.json")
                    .AddEnvironmentVariables();

                string lConnectionString = ENV == ENVS.DEV ? DEV_CONN_STR : QA_CONN_STR;

                using (SqlConnection Conn = new SqlConnection(lConnectionString))
                {
                    using (SqlCommand lCmd = new SqlCommand(lCntStatement, Conn))
                    {
                        Conn.Open();
                        lCnt = (int)lCmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName}", e);
            }

            return lCnt;
        }


        public void CleanDb(string aConnStr )
        {
            string FuncName = ClassName + $"CleanQADb";

            try
            {
                var configurationBuilder = new ConfigurationBuilder()
                    .AddJsonFile("UnitTesting.json")
                    .AddEnvironmentVariables();

                using (SqlConnection Conn = new SqlConnection(aConnStr))
                {
                    using (SqlCommand lCmd = new SqlCommand("dbo.CleanQa", Conn) { CommandType = System.Data.CommandType.StoredProcedure } )
                    {
                        Conn.Open();
                        lCmd.ExecuteNonQuery();
                    }
                }
                Logger.LogInfo($"{FuncName} - Successfully called Executed------------------------------------------------");
            }
            catch (Exception e)
            {
                Logger.LogException($"{FuncName} - Failed to call StoredProc", e);
            }
        }
    }
}
