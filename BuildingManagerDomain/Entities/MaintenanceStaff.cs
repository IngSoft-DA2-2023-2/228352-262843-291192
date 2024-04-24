using BuildingManagerDomain.Enums;

namespace BuildingManagerDomain.Entities
{
    public class MaintenanceStaff: User
    {
        public MaintenanceStaff()
        {
            Role = RoleType.MAINTENANCE;
        }
    }
}
