
using System;

namespace BuildingManagerDomain.Entities
{
    public struct BuildingsReportData
    {
        public BuildingsReportData(int openRequests, int closeRequests, int inProgressRequests, Guid buildingId)
        {
            OpenRequests = openRequests;
            CloseRequests = closeRequests;
            InProgressRequests = inProgressRequests;
            BuildingId = buildingId;
        }
        public int OpenRequests { get; }
        public int CloseRequests { get; }
        public int InProgressRequests { get; }
        public Guid BuildingId { get; }
    }
}
