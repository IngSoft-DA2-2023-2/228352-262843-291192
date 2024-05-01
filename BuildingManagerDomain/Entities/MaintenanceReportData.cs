
using System;

namespace BuildingManagerDomain.Entities
{
    public struct MaintenanceReportData
    {
        public MaintenanceReportData(int openRequests, int closeRequests, int inProgressRequests)
        {
            OpenRequests = openRequests;
            CloseRequests = closeRequests;
            InProgressRequests = inProgressRequests;
        }
        public int OpenRequests { get; }
        public int CloseRequests { get; }
        public int InProgressRequests { get; }
    }
}
