using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;

namespace BuildingManagerLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly IRequestRepository _requestRepository;
        public ReportLogic(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }
        public List<MaintenanceData> GetReport(Guid? identifier, string filter, string reportType)
        {
            if (reportType == "maintenances" && identifier != null)
            {
                MaintenanceReport report = new MaintenanceReport(_requestRepository);
                return report.GetReport(identifier.Value, filter);
            }
            else if (reportType == "buildings")
            {
                BuildingsReport report = new BuildingsReport(_requestRepository);
                return report.GetReport(null, filter);
            }
            throw  new NotFoundException(new Exception(), "Report type not found.");
        }
    }
}