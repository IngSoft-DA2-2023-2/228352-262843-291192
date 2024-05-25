using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface IConstructionCompanyRepository
    {
        ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany); 
    }
}
