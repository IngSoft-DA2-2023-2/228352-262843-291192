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
            Guid userId = _userLogic.GetUserIdFromSessionToken(sessionToken);
            Guid companyId = _constructionCompanyLogic.GetCompanyIdFromUserId(userId);
            User newUser =_userLogic.CreateUser(user);
            _constructionCompanyLogic.AssociateCompanyToUser(newUser.Id, companyId);
            return newUser;
        }
    }
}
