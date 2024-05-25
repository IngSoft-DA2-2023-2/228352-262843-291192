using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
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
            _context.Set<ConstructionCompany>().Add(constructionCompany);
            _context.SaveChanges();
            return constructionCompany;
        }
    }
}
