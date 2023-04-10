using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzerCore.Models
{
    public class JsonWebTokenModel
    {
        public DateTime Expired { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
    }
}
