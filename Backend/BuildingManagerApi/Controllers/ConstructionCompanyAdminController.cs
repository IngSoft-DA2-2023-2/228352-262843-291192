using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using BuildingManagerApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/construction-company-admins")]
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

        [HttpGet]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult GetConstructionCompanyFromAdmin([FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            ConstructionCompanyResponse constructionCompanyResponse = new ConstructionCompanyResponse(_constructionCompanyAdminLogic.GetConstructionCompany(sessionToken));
            return Ok(constructionCompanyResponse);
        }

        [HttpGet]
        [Route("{id}/buildings")]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult GetBuildingsFromCCAdmin([FromRoute] Guid id, [FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            ListBuildingsResponse listBuildingsResponse = new ListBuildingsResponse(_constructionCompanyAdminLogic.GetBuildingsFromCCAdmin(id, sessionToken));
            return Ok(listBuildingsResponse);
        }
    }
}