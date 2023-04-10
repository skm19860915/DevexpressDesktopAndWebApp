using System;
using System.Collections.Generic;
using System.Text;

namespace BlitzerCore.Models
{
    public class Kanban
    {
        public enum ViewMode
        {
            MyTasks,
            HoldUntil,
            DeadLine
        };

        public enum Source
        {
            MyTasks,
            CompanyTasks
        }
    }
}
