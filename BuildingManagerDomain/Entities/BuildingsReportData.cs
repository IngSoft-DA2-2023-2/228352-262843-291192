
namespace BuildingManagerDomain.Entities
{
    public struct BuildingsReportData
    {
        public BuildingsReportData(int openRequests, int closeRequests, int inProgressRequests)
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
