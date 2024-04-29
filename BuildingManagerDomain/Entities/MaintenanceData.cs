
namespace BuildingManagerDomain.Entities
{
    public struct MaintenanceData
    {
        public MaintenanceData(int openRequests)
        {
            OpenRequests = openRequests;
        }
        public int OpenRequests{get;}
    }
}
