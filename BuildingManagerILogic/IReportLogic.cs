using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;

namespace BuildingManagerILogic
{
    public interface IReportLogic
    {
        public List<ReportData> GetReport(Guid? identifier, string filter, ReportType reportType);
    }
}