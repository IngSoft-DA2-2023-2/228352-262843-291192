
using System;

namespace BuildingManagerDomain.Entities
{
    public struct MaintenanceReportData
    {
        public MaintenanceReportData(int openRequests, int closeRequests)
        {
            OpenRequests = openRequests;
            CloseRequests = closeRequests;
        }
        public int OpenRequests { get; }
        public int CloseRequests { get; }
    }
}
