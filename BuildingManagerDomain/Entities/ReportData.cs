
using System;

namespace BuildingManagerDomain.Entities
{
    public struct ReportData
    {
        public ReportData(int openRequests, int closeRequests, int inProgressRequests, int averageClosingTime, string maintainerName, Guid buildingId)
        {
            OpenRequests = openRequests;
            CloseRequests = closeRequests;
            InProgressRequests = inProgressRequests;
            AverageClosingTime = averageClosingTime;
            MaintainerName = maintainerName;
            BuildingId = buildingId;
        }
        public int OpenRequests { get; }
        public int CloseRequests { get; }
        public int InProgressRequests { get; }
        public int AverageClosingTime { get; }
        public string MaintainerName { get; }
        public Guid BuildingId { get; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ReportData)obj;
            return OpenRequests == other.OpenRequests &&
            CloseRequests == other.CloseRequests &&
            InProgressRequests == other.InProgressRequests &&
            AverageClosingTime == other.AverageClosingTime &&
            MaintainerName == other.MaintainerName &&
            BuildingId == other.BuildingId;
        }
    }
}
