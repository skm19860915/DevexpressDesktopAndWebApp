using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzerCore.Models
{
    public class Role
    {
        public const string Admin = "Administrator";
        public const string Agent = "Agent";
        public const string Client = "Client";
        public const string Traveler = "Traveler";
        public const string Authenticated = Admin + "," + Agent + "," + Client;
    }
}
