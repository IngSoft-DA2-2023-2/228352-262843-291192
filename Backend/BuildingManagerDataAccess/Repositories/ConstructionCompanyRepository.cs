using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Repositories
{
    public class ConstructionCompanyRepository : IConstructionCompanyRepository
    {
        private DbContext _context;
        public ConstructionCompanyRepository(DbContext context)
        {
            _context = context;
        }
        public ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany)
        {
            if (_context.Set<ConstructionCompany>().Any(a => a.Name == constructionCompany.Name))
            {
                throw new ValueDuplicatedException("Name");
            }
            _context.Set<ConstructionCompany>().Add(constructionCompany);
            _context.SaveChanges();
            return constructionCompany;
        }
    }
}
