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

        public void AssociateCompanyToUser(Guid userId, Guid companyId)
        {
            var companyAdminAssociation = new CompanyAdminAssociation
            {
                ConstructionCompanyAdminId = userId,
                ConstructionCompanyId = companyId
            };
            _context.Set<CompanyAdminAssociation>().Add(companyAdminAssociation);
            _context.SaveChanges();
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

        public Guid GetCompanyIdFromUserId(Guid userId)
        {
            if (!_context.Set<CompanyAdminAssociation>().Any(a => a.ConstructionCompanyAdminId == userId))
            {
                throw new ValueNotFoundException("Company User Association");
            }
            return _context.Set<CompanyAdminAssociation>().First(i => i.ConstructionCompanyAdminId == userId).ConstructionCompanyId;
        }

        public ConstructionCompany ModifyConstructionCompanyName(Guid constructionCompanyId, string name, Guid userId)
        {
            if (_context.Set<ConstructionCompany>().Any(a => a.Name == name))
            {
                throw new ValueDuplicatedException("Name");
            }
            if (!_context.Set<ConstructionCompany>().Any(a => a.Id == constructionCompanyId))
            {
                throw new ValueNotFoundException("Construction Company");
            }
            if (!_context.Set<CompanyAdminAssociation>().Any(a => a.ConstructionCompanyId == constructionCompanyId && a.ConstructionCompanyAdminId == userId))
            {
                throw new ValueNotFoundException("Company User Association");
            }

            ConstructionCompany company = _context.Set<ConstructionCompany>().First(i => i.Id == constructionCompanyId);
            company.Name = name;
            _context.SaveChanges();

            return company;
        }
    }
}
