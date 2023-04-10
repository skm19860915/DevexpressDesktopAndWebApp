using System;
using System.Collections.Generic;
using System.Linq;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    public class LogMsgDataAccess 
    {
        protected IDbContext mDBContext = null;

        public LogMsgDataAccess(IDbContext aContext)
        {
            mDBContext = aContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool LogMsgExists(int id)
        {
            try
            {
                return FindById(id) != null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LogMsg DeleteLogMsg(int id)
        {
            try
            {
                var LogMsg = FindById(id);
                if (LogMsg == null)
                {
                    return null;
                }
                mDBContext.LogMsgs.Remove(LogMsg);

                return LogMsg;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passedLogMsg"></param>
        /// <returns></returns>
        public LogMsg SaveLogMsg(LogMsg passedLogMsg)
        {
            try
            {
                if (passedLogMsg.ID == 0)
                    if (mDBContext != null)
                    {
                        mDBContext.LogMsgs.Add(passedLogMsg);
                        mDBContext.SaveChanges();
                    }
                return passedLogMsg;
            }
            catch (Exception e1)
            {
                //mDBContext.LogException("LogMsg Saved Failed", e1);
                throw new Exception(e1.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passedLogMsg"></param>
        public void UpdateLogMsg(LogMsg passedLogMsg)
        {
            try
            {
                SaveLogMsg(passedLogMsg);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LogMsg GetLogMsgById(int id)
        {
            try
            {
                var LogMsg = FindById(id);
                return LogMsg;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LogMsg> GetLogMsgs()
        {

            try
            {
                return mDBContext.LogMsgs.OrderByDescending(x => x.Timestamp);
            }
            catch (Exception e)
            {
                //mDBContext.LogException("LogMsgDataAccess.GetLogMsgs(Exception)", e);
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private LogMsg FindById(int id)
        {

            return GetLogMsgs().Where(x => x.ID == id).FirstOrDefault();
        }
    }
}
