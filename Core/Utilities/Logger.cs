using System;
using Gurock.SmartInspect;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using BlitzerCore.Models;
using BlitzerCore.DataAccess;
using Microsoft.Extensions.Configuration;
using BlitzerCore.Utilities;

namespace BlitzerCore.Utilities
{
    public class Logger
    {
        private const string TABLENAME = "LogMsgs";
        private const string MSGTYPE = "MsgType";

        private static BufferBlock<LogMsg> mMsgConsumer = new BufferBlock<LogMsg>();
        public static bool LogToDB { get { return true; } }
        public static bool LogStatsToDB { get { return false; } }
        public static IConnectionFactory ConnectionFactory { get; set; }
        public static string ConnectionString { get; set; }

        public static void Init(string aAppName)
        {
            SiAuto.Si.Enabled = true;
            SiAuto.Si.AppName = aAppName;
            SiAuto.Si.Connections = "tcp(host=\"Hercules\")";

        }

        public static Stopwatch StartStopWatch()
        {
            Stopwatch lStopWatch = new Stopwatch();
            lStopWatch.Start();
            return lStopWatch;
        }

        public static void StopWatchElapsedTime(Stopwatch aStopWatch, string aLocation)
        {
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = aStopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Logger.LogInfo(aLocation + " Elapsed Time " + elapsedTime);
        }

        public static void StopStopWatch(Stopwatch aStopWatch, string aLocation )
        {
            aStopWatch.Stop();
            StopWatchElapsedTime(aStopWatch, aLocation);
        }
        public static void LogError(string aMsg, string aDetails = null)
        {
            if (LogToDB)
            {

                mMsgConsumer.Post( new LogMsg(LogMsg.MsgTypes.Error, aMsg));
            }
            if ( aDetails == null )
                BlitzerCore.Helpers.CoreEmailHelper.SendSystemEmail("Critical Error", aMsg);
            else
                BlitzerCore.Helpers.CoreEmailHelper.SendSystemEmail(aMsg, aDetails);
            SiAuto.Main.LogError(aMsg);
        }

        private static void WriteToDB(LogMsg aMsg)
        {
            if (ConnectionFactory == null)
                return;

            using (IDbContext lContext = ConnectionFactory.GetDbContext())
            {
                LogMsgDataAccess lLMDA = new LogMsgDataAccess(lContext);
                lLMDA.SaveLogMsg(aMsg);
            }
        }

        public static void LogDebug(string aMsg)
        {
            if (LogToDB)
            {
               mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Debug, aMsg));
            }
            SiAuto.Main.LogDebug(aMsg);
            Debug.WriteLine(aMsg);
        }

        public static void LogTracing(string aMsg)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Tracing, aMsg));
            }
            SiAuto.Main.LogVerbose(aMsg);
            Debug.WriteLine(aMsg);
        }

        public static Task<int> InitConsummer()
        {
            return StartLoggerConsummer(mMsgConsumer);
        }


        private static async Task<int> StartLoggerConsummer(ISourceBlock<LogMsg> aDataBuffer)
        {
            // Initialize a counter to track the number of bytes that are processed.
            int bytesProcessed = 0;

            //Read from the source buffer until the source buffer has no
            //available output data.
            while (await aDataBuffer.OutputAvailableAsync())
            {
                try
                {
                    LogMsg data = aDataBuffer.Receive();
                    WriteToDB(data);
                }
                catch (Exception e)
                {
                    SiAuto.Main.LogError("Exception writing to DB : " + e);
                    Debug.WriteLine("Exception writing to DB : " + e);
                }
            }

            return bytesProcessed;
        }
        public static void LogStats(string aMsg, long aTime)
        {
            LogStats(aMsg, aTime.ToString());
        }

        public static void LogStats(string aMsg, string aTime)
        {
            if (LogStatsToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Stat, aMsg, aTime) );
                //lQB.AddRAWNameValuePair("Value", aTime.ToString());
            }
            SiAuto.Main.LogMessage(aMsg + "= " + aTime + "ms");
            Debug.WriteLine(aMsg + "= " + aTime + "ms");
        }

        public static void LogSQL(string aText, string aSQL)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.SQL, aText));
            }
            SiAuto.Main.LogSql(aText, aText + " " + aSQL);
        }

        public static void LogException(string aMsg, Exception ex)
        {

            SiAuto.Main.LogException(aMsg, ex);
            Debug.WriteLine(aMsg);

            var lInnerExcept = ex.Message;
            if (ex.InnerException != null)
                lInnerExcept += "<br><br>Inner Except : " + ex.InnerException.Message;

            BlitzerCore.Helpers.CoreEmailHelper.SendSystemEmail(aMsg, lInnerExcept);
            // Do not attempt to write to db until we have a connect to the DB
            mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Exception, aMsg + " : " + ex.Message + " -> " + lInnerExcept));
        }

        public static void LogWarning(string aMsg)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Info, aMsg));
            }

            SiAuto.Main.LogWarning(aMsg);
        }

        public static void EnterFunction(string aMethod)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Info, aMethod +"->"));
            }

            SiAuto.Main.EnterMethod(aMethod);
        }

        public static void LeaveFunction(string aMethod)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Info, aMethod + "<-"));
            }

            SiAuto.Main.LeaveMethod(aMethod);
        }

        public static void LogValue (string aMsg, string aValue)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Info, aMsg));
            }

            SiAuto.Main.LogString(aMsg, aValue);
        }

        public static void LogValue(string aMsg, int aValue)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Info, aMsg));
            }

            SiAuto.Main.LogInt(aMsg, aValue);
        }
        public static void LogValue(string aMsg, int? aValue)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Info, aMsg));
            }

            if (aValue != null)
                SiAuto.Main.LogInt(aMsg, aValue.Value);
            else
                SiAuto.Main.LogDebug(aMsg + " was NULL");
        }

        public static void LogValue(string aMsg, double aValue)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Info, aMsg));
            }
            SiAuto.Main.LogDouble(aMsg, aValue);
        }

        public static void LogMessage(string aMsg)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Message, aMsg));
            }
            SiAuto.Main.LogMessage(aMsg);
            Debug.WriteLine(aMsg);
        }

        public static void LogInfo(string aMsg)
        {
            if (LogToDB)
            {
                mMsgConsumer.Post(new LogMsg(LogMsg.MsgTypes.Info, aMsg));
            }
            SiAuto.Main.LogMessage(aMsg);
            Debug.WriteLine(aMsg);
        }

    }
}