using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Repositories
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
            if (_context.Set<Admin>().Any(a => a.Email == admin.Email))
            {
                throw new ValueDuplicatedException("Email");
            }
            _context.Set<Admin>().Add(admin);
            _context.SaveChanges();

            return admin;
        }
    }
}
