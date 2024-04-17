using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;

namespace BuildingManagerLogic
{
    public class MaintenaceLogic : IMaintenanceLogic
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        public MaintenaceLogic(IMaintenanceRepository maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }
        public MaintenanceStaff CreateMaintenanceStaff(MaintenanceStaff maintenanceStaff)
        {

            return _maintenanceRepository.CreateMaintenanceStaff(maintenanceStaff);
        }
    }
}
