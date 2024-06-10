using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Repositories
{
    public class OwnerRepository: IOwnerRepository
    {
        private readonly DbContext _context;

        public OwnerRepository(DbContext context)
        {
            _context = context;
        }

        public Owner CreateOwner(Owner owner)
        {
            if (_context.Set<Owner>().Any(a => a.Email == owner.Email))
            {
                throw new ValueDuplicatedException("Email");
            }
            _context.Set<Owner>().Add(owner);
            _context.SaveChanges();
            return owner;
        }
    }
}
