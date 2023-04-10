using System;
using System.Collections.Generic;
using System.Text;
using BlitzerCore.Models;

namespace BlitzerCore.Business
{
    public class NewTripTaskManager
    {
        private IDbContext mContext;

        public NewTripTaskManager(IDbContext context)
        {
            mContext = context;
        }
        public List<TaskTemplate> GetTaskTemplates(Agent aAgent)
        {
            var lTasks = new List<TaskTemplate>();
            var lEze = new CompanyBusiness(mContext).Get(61);
            var lTask = new TaskTemplate() { CompanyId = 61, Company = lEze, Name = "New TripTask", Opportunity = false, FromStartDate = 0 };
            var lTask1 = new TaskTemplate() { CompanyId = 61, Company = lEze,  Name = "Assign Seats", Opportunity = false, FromStartDate = 0 };

            lTasks.Add(lTask);
            lTasks.Add(lTask1);

            return lTasks;
        }

       
    }
}
