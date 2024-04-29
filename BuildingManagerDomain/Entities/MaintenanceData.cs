
namespace BuildingManagerDomain.Entities
{
    public struct MaintenanceData
    {
        public MaintenanceData(int openRequests, int closeRequests, int inProgressRequests)
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
