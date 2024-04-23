using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;
        public UserRepository(DbContext context)
        {
            _context = context;
        }
        public User CreateUser(User user)
        {
            if (_context.Set<User>().Any(a => a.Email == user.Email))
            {
                throw new ValueDuplicatedException("Email");
            }
            _context.Set<User>().Add(user);
            _context.SaveChanges();

            return user;
        }

        public bool Exists(Guid userId)
        {
            return _context.Set<User>().Any(a => a.Id == userId);
        }

        public RoleType Role(Guid userId)
        {
            return _context.Set<User>().FirstOrDefault(a => a.Id == userId).Role;
        }
    }
}
