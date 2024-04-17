using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IMaintenanceRepository
    {
        MaintenanceStaff CreateMaintenanceStaff(MaintenanceStaff maintenanceStaff);
    }
}
