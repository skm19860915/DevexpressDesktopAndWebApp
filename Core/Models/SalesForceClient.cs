using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzerCore.Models
{
    public class SalesForceClient
    {
        public const string LOGIN_ENDPOINT = "https://login.salesforce.com/services/oauth2/token";
        public const string API_ENDPOINT = "/services/data/v48.0/";

        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthToken { get; set; }
        public string InstanceUrl { get; set; }
    }
}
