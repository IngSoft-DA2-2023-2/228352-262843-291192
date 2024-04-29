
using System.Collections.Generic;

namespace BuildingManagerDomain.Entities
{
    public class MaintenanceReport
    {
        public List<MaintenanceData> MaintenanceDatas { get; set; }
        public List<Request> Requests { get; set; }
    }
}
