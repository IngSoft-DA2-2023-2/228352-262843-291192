using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;

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
            try
            {
                return _maintenanceRepository.CreateMaintenanceStaff(maintenanceStaff);
            }
            catch (ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }
        }
    }
}
