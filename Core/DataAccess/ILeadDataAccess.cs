using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlitzerCore.Models;

namespace BlitzerCore.DataAccess
{
    public interface IClientDataAccess
    {
        Client Save(Client aClient);
        Client Get(string aUserID);
    }
}
