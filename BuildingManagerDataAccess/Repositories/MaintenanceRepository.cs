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
            if (_context.Set<MaintenanceStaff>().Any(a => a.Email == maintenanceStaff.Email))
            {
                throw new ValueDuplicatedException("Email");
            }
            _context.Set<MaintenanceStaff>().Add(maintenanceStaff);
            _context.SaveChanges();

            return maintenanceStaff;
        }
    }
}
