using BlitzerCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlitzerCore.Business.AIFilters
{
    public class MinimizeDurationDepartAround8AM : AIFilter
    {
        public MinimizeDurationDepartAround8AM()
        {
            _AIFilterID = 1;
            _AIName = "Minimize Duration Depart Around 8AM";
            Description = "Minimize Duration Depart Around 8AM";
        }
    }
}
