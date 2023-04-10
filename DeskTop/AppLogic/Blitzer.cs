using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.DataServices;
using BlitzerCore.Models;
using BlitzerCore.Business;

namespace Desktop.AppLogic
{
    public class Blitzer
    {
        static Blitzer instance;
        public static Blitzer Instance { get { return instance; } }
        static Blitzer()
        {
            instance = new Blitzer();
        }
        private Blitzer()
        {

        }

        public Contact CurrentUser()
        {
            return new ContactBusiness(RepositoryContext.Instance).Get("eric@eze2travel.com");
        }
    }
}
