using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Repositories
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly DbContext _context;
        public MaintenanceRepository(DbContext context)
        {
            _context = context;
        }
        public MaintenanceStaff CreateMaintenanceStaff(MaintenanceStaff maintenanceStaff)
        {
            _context.Set<MaintenanceStaff>().Add(maintenanceStaff);
            _context.SaveChanges();

            return maintenanceStaff;
        }
    }
}
