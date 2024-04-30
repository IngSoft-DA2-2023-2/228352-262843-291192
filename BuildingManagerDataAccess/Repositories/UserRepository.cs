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

        public bool ExistsFromSessionToken(Guid userId)
        {
            return _context.Set<User>().Any(a => a.SessionToken == userId);
        }
        public bool EmailExists(string email)
        {
            return _context.Set<User>().Any(a => a.Email == email);
        }

        public RoleType RoleFromSessionToken(Guid userId)
        {
            try
            {
                return _context.Set<User>().First(a => a.SessionToken == userId).Role;
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException("User not found.");
            }
        }

        public User DeleteUser(Guid userId, RoleType role)
        {
            User user;
            try
            {
                user = _context.Set<User>().First(i => i.Id == userId);
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException("User not found.");
            }
            if (user.Role != role)
            {
                throw new InvalidOperationException(role.ToString().ToLower() + " not found.");
            }
            _context.Set<User>().Remove(user);
            _context.SaveChanges();

            return user;
        }

        public Guid Login(string email, string password)
        {
            User user;
            try
            {
                user = _context.Set<User>().First(i => i.Email == email && i.Password == password);
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException("User not found.");
            }
            Guid newSessionToken = Guid.NewGuid();

            user.SessionToken = newSessionToken;
            _context.SaveChanges();

            return newSessionToken;
        }

        public Guid Logout(Guid sessionToken)
        {
            User user;
            try
            {
                user = _context.Set<User>().First(i => i.SessionToken == sessionToken);
            }
            catch (InvalidOperationException)
            {
                throw new ValueNotFoundException("User not found.");
            }
            Guid token = user.SessionToken!.Value;

            user.SessionToken = null;
            _context.SaveChanges();
            return token;

        }

        public User CreateInvitedUser(InvitationAnswer invitationAnswer)
        {
            throw new NotImplementedException();
        }
    }
}
