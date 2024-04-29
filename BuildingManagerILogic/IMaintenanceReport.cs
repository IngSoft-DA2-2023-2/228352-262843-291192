using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IMaintenanceReport
    {
        public List<MaintenanceData> GetReport();
    }
}