using System;
using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;

namespace BuildingManagerLogic
{
    public class ConstructionCompanyLogic : IConstructionCompanyLogic
    {
        private readonly IConstructionCompanyRepository _constructionCompanyRepository;
        public ConstructionCompanyLogic(IConstructionCompanyRepository constructionCompanyRepository)
        {
            _constructionCompanyRepository = constructionCompanyRepository;
        }
        public ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany, Guid sessionToken)
        {
            try
            {
                return _constructionCompanyRepository.CreateConstructionCompany(constructionCompany);
            }
            catch (ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }
        }
    }
}
