using System;
using BuildingManagerDomain.Entities;
using BuildingManagerILogic;

namespace BuildingManagerLogic
{
    public class ConstructionCompanyAdminLogic : IConstructionCompanyAdminLogic
    {
        private readonly IConstructionCompanyLogic _constructionCompanyLogic;
        private readonly IUserLogic _userLogic;
        public ConstructionCompanyAdminLogic(IConstructionCompanyLogic constructionCompanyLogic, IUserLogic userLogic)
        {
            _constructionCompanyLogic = constructionCompanyLogic;
            _userLogic = userLogic;
        }

        public User CreateConstructionCompanyAdmin(User user, Guid sessionToken)
        {
            Guid companyId = GetCompanyIdFromSessionToken(sessionToken);
            User newUser = _userLogic.CreateUser(user);
            _constructionCompanyLogic.AssociateCompanyToUser(newUser.Id, companyId);
            return newUser;
        }

        private Guid GetCompanyIdFromSessionToken(Guid sessionToken)
        {
            Guid userId = _userLogic.GetUserIdFromSessionToken(sessionToken);
            return _constructionCompanyLogic.GetCompanyIdFromUserId(userId);
        }

        public ConstructionCompany GetConstructionCompany(Guid sessionToken)
        {
            Guid companyId = GetCompanyIdFromSessionToken(sessionToken);
            return _constructionCompanyLogic.GetConstructionCompany(companyId);
        }

        public List<BuildingResponse> GetBuildingsFromCCAdmin(Guid userId, Guid sessionToken)
        {
            Guid userIdFromSessionToken = _userLogic.GetUserIdFromSessionToken(sessionToken);
            if(userIdFromSessionToken != userId)
            {
                throw new InvalidOperationException("User not authorized to access this information.");
            }
            Guid companyId = GetCompanyIdFromSessionToken(sessionToken);
            return _constructionCompanyLogic.GetCompanyBuildings(companyId);
        }
    }
}
