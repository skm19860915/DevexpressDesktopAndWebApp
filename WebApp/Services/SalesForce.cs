using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace WebApp.Services
{
    public class SalesForce
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;
        private string mInstanceURL;
        private string mToken;

        public SalesForce(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void Login()
        {
            String jsonResponse;

            var lUserName = Configuration.GetSection("SalesForce").GetValue<string>("username");
            var lPassword = Configuration.GetSection("SalesForce").GetValue<string>("password");
            var lClientID = Configuration.GetSection("SalesForce").GetValue<string>("clientId");
            var lClientSecret = Configuration.GetSection("SalesForce").GetValue<string>("clientSecret");

            using (var client = new HttpClient())
            {
                var request = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"client_id", lClientID},
                {"client_secret", lClientSecret},
                {"username", lUserName},
                //{"password", Password + Token}
                {"password", lPassword}
            }
                );
                request.Headers.Add("X-PrettyPrint", "1");
                var response = client.PostAsync(SalesForceClient.LOGIN_ENDPOINT, request).Result;
                jsonResponse = response.Content.ReadAsStringAsync().Result;
            }
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
            mToken = values["access_token"];
            mInstanceURL = values["instance_url"];
        }
    }
}
