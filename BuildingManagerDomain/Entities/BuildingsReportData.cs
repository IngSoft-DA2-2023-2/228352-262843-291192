
namespace BuildingManagerDomain.Entities
{
    public struct BuildingsReportData
    {
        public BuildingsReportData(int openRequests)
        {
            OpenRequests = openRequests;
            
        }
        public int OpenRequests { get; }
    }
}
