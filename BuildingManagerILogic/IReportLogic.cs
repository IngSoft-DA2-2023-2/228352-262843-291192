using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IReportLogic
    {
        public List<MaintenanceData> GetReport(Guid buildingId);
    }
}