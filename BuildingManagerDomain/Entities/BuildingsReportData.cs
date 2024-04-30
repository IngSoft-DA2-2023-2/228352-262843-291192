
namespace BuildingManagerDomain.Entities
{
    public struct BuildingsReportData
    {
        public BuildingsReportData(int openRequests, int closeRequests)
        {
            OpenRequests = openRequests;
            CloseRequests = closeRequests;
        }
        public int OpenRequests { get; }
        public int CloseRequests { get; }

    }
}
