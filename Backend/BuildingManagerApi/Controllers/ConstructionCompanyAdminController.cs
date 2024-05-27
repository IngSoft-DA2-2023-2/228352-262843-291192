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
        private readonly IConstructionCompanyAdminLogic _constructionCompanyAdminLogic;

        public ConstructionCompanyAdminController(IConstructionCompanyAdminLogic constructionCompanyAdminLogic)
        {
            _constructionCompanyAdminLogic = constructionCompanyAdminLogic;
        }

        [HttpPost]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult CreateConstructionCompanyAdmin([FromBody] CreateConstructionCompanyAdminRequest constructionCompanyAdminRequest, [FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            CreateConstructionCompanyAdminResponse createConstructionCompanyAdminResponse = new CreateConstructionCompanyAdminResponse(_constructionCompanyAdminLogic.CreateConstructionCompanyAdmin(constructionCompanyAdminRequest.ToEntity(), sessionToken));
            return CreatedAtAction(nameof(CreateConstructionCompanyAdmin), createConstructionCompanyAdminResponse);
        }
    }
}