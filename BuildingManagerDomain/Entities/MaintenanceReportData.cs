
using System;

namespace BuildingManagerDomain.Entities
{
    public struct MaintenanceReportData
    {
        public MaintenanceReportData(int openRequests)
        {
            OpenRequests = openRequests;
        }
        public int OpenRequests { get; }
    }
}
