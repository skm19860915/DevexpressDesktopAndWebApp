using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace WebApp.Services
{
    public class ActiveCampaign
    {
        public const string Version = "3";
        public string URL = $"https://eze2travel.api-us1.com/api/" + Version;
        public const string Key = "1cffb87866c4a255e6b08b4487815140794d12597d5df2416d326f4d47849634dfc6f461";

        public void SyncRemoteToLocal()
        {

        }

        public void SyncLocalToRemote()
        {

        }

        public List<Trip> GetRemoteDeals()
        {
            var lOutput = new List<Trip>();

            var lClient = new RestClient(URL);
            var request = new RestRequest(URL + "deals");

            return lOutput;
        }
    }
}
