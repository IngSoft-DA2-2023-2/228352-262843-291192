using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using BuildingManagerApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/construction-company-admin")]
    public class ConstructionCompanyAdminController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        private readonly IConstructionCompanyLogic _constructionCompanyLogic;

        public ConstructionCompanyAdminController(IUserLogic userLogic, IConstructionCompanyLogic constructionCompanyLogic)
        {
            _userLogic = userLogic;
            _constructionCompanyLogic = constructionCompanyLogic;
        }

        [HttpPost]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult CreateConstructionCompanyAdmin([FromBody] CreateConstructionCompanyAdminRequest constructionCompanyAdminRequest, [FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            Guid userId = _userLogic.GetUserIdFromSessionToken(sessionToken);
            Guid companyId = _constructionCompanyLogic.GetCompanyIdFromUserId(userId);
            CreateConstructionCompanyAdminResponse createConstructionCompanyAdminResponse = new CreateConstructionCompanyAdminResponse(_userLogic.CreateUser(constructionCompanyAdminRequest.ToEntity()));
            Guid newUserId = createConstructionCompanyAdminResponse.Id;
            _constructionCompanyLogic.AssociateCompanyToUser(newUserId, companyId);
            return CreatedAtAction(nameof(CreateConstructionCompanyAdmin), createConstructionCompanyAdminResponse);
        }
    }
}