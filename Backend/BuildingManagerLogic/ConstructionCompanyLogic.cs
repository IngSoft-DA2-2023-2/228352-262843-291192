using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;

namespace BuildingManagerLogic
{
    public class ConstructionCompanyLogic : IConstructionCompanyLogic
    {
        private readonly IConstructionCompanyRepository _constructionCompanyRepository;
        public ConstructionCompanyLogic(IConstructionCompanyRepository constructionCompanyRepository)
        {
            _constructionCompanyRepository = constructionCompanyRepository;
        }
        public ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany)
        {
            return _constructionCompanyRepository.CreateConstructionCompany(constructionCompany);
        }
    }
}
