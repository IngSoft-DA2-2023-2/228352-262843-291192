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
        public ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany, Guid constructionCompanyAdminId)
        {
            if (_context.Set<ConstructionCompany>().Any(a => a.Name == constructionCompany.Name))
            {
                throw new ValueDuplicatedException("Name");
            }
            if (_context.Set<CompanyAdminAssociation>().Any(a => a.ConstructionCompanyAdminId == constructionCompanyAdminId))
            {
                throw new ValueDuplicatedException("User");
            }
            _context.Set<ConstructionCompany>().Add(constructionCompany);
            _context.SaveChanges();

            var companyAdminAssociation = new CompanyAdminAssociation
            {
                ConstructionCompanyAdminId = constructionCompanyAdminId,
                ConstructionCompanyId = constructionCompany.Id
            };
            _context.Set<CompanyAdminAssociation>().Add(companyAdminAssociation);
            _context.SaveChanges();

            return constructionCompany;
        }

        public ConstructionCompany ModifyConstructionCompanyName(Guid constructionCompanyId, string name, Guid userId)
        {
            ConstructionCompany company = _context.Set<ConstructionCompany>().First(i => i.Id == constructionCompanyId);
            company.Name = name;
            _context.SaveChanges();

            return company;
        }
    }
}
