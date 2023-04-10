using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlitzerCore.Models
{
    public class LogMsg
    {
        public enum MsgTypes { Exception, Stat, Error, Debug, Tracing, SQL, Message,Info}

        public LogMsg()
        {
            Timestamp = new DateTime();
        }

        public LogMsg(MsgTypes aType, string aMsg, string aTime)
        {
            MsgType = aType;
            Description = aMsg;
            Value = aTime;
            Timestamp = DateTime.Now;
        }

        public LogMsg(MsgTypes aType, string aMsg)
        {
            MsgType = aType;
            Description = aMsg;
            Timestamp = DateTime.Now;
        }
        [Key]
        public int ID { get; set; }
        public MsgTypes MsgType { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
