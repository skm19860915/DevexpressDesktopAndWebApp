using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzerCore.Models
{
    public enum JsonType
    {
        Error = 0,
        Success = 1,
        Info = 2,
        Warning = 3,
        Question = 4
    }

    public class JsonModel
    {
        //[ScriptIgnore]
        public JsonType JsonType { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public object Result { get; set; }
        public string AlertType => JsonType.ToString();

        public JsonModel(JsonType jsonType, string message, string title = null, object result = null)
        {
            JsonType = jsonType;
            Message = message;
            Title = title;
            Result = result;
        }
    }
}
