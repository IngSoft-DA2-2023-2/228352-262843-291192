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
        private readonly IUserRepository _userRepository;
        public ConstructionCompanyLogic(IConstructionCompanyRepository constructionCompanyRepository, IUserRepository userRepository)
        {
            _constructionCompanyRepository = constructionCompanyRepository;
            _userRepository = userRepository;
        }
        public ConstructionCompany CreateConstructionCompany(ConstructionCompany constructionCompany, Guid sessionToken)
        {
            Guid userId;
            try
            {
                userId = _userRepository.GetUserIdFromSessionToken(sessionToken);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
            try
            {
                return _constructionCompanyRepository.CreateConstructionCompany(constructionCompany, userId);
            }
            catch (ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }
        }

        public Guid GetCompanyIdFromUserId(Guid userId)
        {
            try
            {
                return _constructionCompanyRepository.GetCompanyIdFromUserId(userId);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public ConstructionCompany ModifyName(Guid id, string name, Guid sessionToken)
        {
            Guid userId = _userRepository.GetUserIdFromSessionToken(sessionToken);
            try
            {
                return _constructionCompanyRepository.ModifyConstructionCompanyName(id, name, userId);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
            catch (ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }
        }
    }
}
