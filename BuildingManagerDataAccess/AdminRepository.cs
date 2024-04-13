using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DbContext _context;
        public AdminRepository(DbContext context)
        {
            _context = context;
        }
        public Admin CreateAdmin(Admin admin)
        {
            _context.Set<Admin>().Add(admin);
            _context.SaveChanges();
            return admin;
        }
    }
}
