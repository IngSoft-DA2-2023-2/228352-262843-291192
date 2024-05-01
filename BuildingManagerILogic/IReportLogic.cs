using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface IReportLogic
    {
        public List<ReportData> GetReport(Guid? identifier, string filter, string reportType);
    }
}