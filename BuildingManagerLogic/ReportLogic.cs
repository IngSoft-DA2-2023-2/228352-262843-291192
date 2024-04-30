using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;

namespace BuildingManagerLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly IRequestRepository _requestRepository;
        public ReportLogic(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }
        public List<MaintenanceData> GetReport(Guid buildingId, string maintainerName)
        {
            MaintenanceReport report = new MaintenanceReport(_requestRepository);
            return report.GetReport(buildingId, maintainerName);
        }
    }
}