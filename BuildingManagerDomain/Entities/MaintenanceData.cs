
namespace BuildingManagerDomain.Entities
{
    public struct MaintenanceData
    {
        public MaintenanceData(int openRequests, int closeRequests)
        {
            OpenRequests = openRequests;
            CloseRequests = closeRequests;
        }
        public int OpenRequests { get; }
        public int CloseRequests { get; }
    }
}
