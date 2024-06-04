using System;
using System.Collections.Generic;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;

namespace BuildingManagerLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IBuildingLogic _buildingLogic;
        public ReportLogic(IRequestRepository requestRepository, IBuildingLogic buildingLogic)
        {
            _requestRepository = requestRepository;
            _buildingLogic = buildingLogic;
        }
        public List<ReportData> GetReport(Guid? identifier, string filter, ReportType reportType)
        {
            if (reportType == ReportType.MAINTENANCE && identifier != null)
            {
                MaintenanceReport report = new MaintenanceReport(_requestRepository, _buildingLogic);
                return report.GetReport(identifier.Value, filter);
            }
            else if (reportType == ReportType.BUILDINGS)
            {
                BuildingsReport report = new BuildingsReport(_requestRepository, _buildingLogic);
                return report.GetReport(null, filter);
            }
            else if (reportType == ReportType.CATEGORIES)
            {
                CategoriesReport report = new CategoriesReport(_requestRepository, _buildingLogic);
                return report.GetReport(identifier.Value, filter);
            }
            else if (reportType == ReportType.APARTMENTS){
                ApartmentsReport report = new ApartmentsReport(_requestRepository, _buildingLogic);
                return report.GetReport(identifier.Value, filter);
            }
            
            throw  new NotFoundException(new Exception(), "Missing information.");
        }
    }
}