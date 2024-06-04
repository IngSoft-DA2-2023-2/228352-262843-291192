using System;
using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IConstructionCompanyRepository
    {
        ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany, Guid constructionCompanyAdminId); 
        ConstructionCompany ModifyConstructionCompanyName(Guid constructionCompanyId, string name, Guid userId);
        Guid GetCompanyIdFromUserId(Guid userId);
        void AssociateCompanyToUser(Guid userId, Guid companyId);
        bool IsUserAssociatedToCompany(Guid userId, Guid companyId);
    }
}
