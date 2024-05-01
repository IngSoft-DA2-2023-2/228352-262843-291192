
using System;

namespace BuildingManagerDomain.Entities
{
    public struct MaintenanceReportData
    {
        public MaintenanceReportData(int openRequests, int closeRequests, int inProgressRequests, int averageClosingTime)
        {
            OpenRequests = openRequests;
            CloseRequests = closeRequests;
            InProgressRequests = inProgressRequests;
            AverageClosingTime = averageClosingTime;
        }
        public int OpenRequests { get; }
        public int CloseRequests { get; }
        public int InProgressRequests { get; }
        public int AverageClosingTime { get; }
    }
}
