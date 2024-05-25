using BuildingManagerApi.Filters;
using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/construction-company")]
    public class ConstructionCompanyController : ControllerBase
    {
        private readonly IConstructionCompanyLogic _constructionCompanyLogic;

        public ConstructionCompanyController(IConstructionCompanyLogic constructionCompanyLogic)
        {
            _constructionCompanyLogic = constructionCompanyLogic;
        }

        [HttpPost]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult CreateConstructionCompany([FromBody] CreateConstructionCompanyRequest constructionCompanyRequest, [FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            CreateConstructionCompanyResponse createConstructionCompanyResponse = new(_constructionCompanyLogic.CreateConstructionCompany(constructionCompanyRequest.ToEntity(), sessionToken));
            return CreatedAtAction(nameof(CreateConstructionCompany), createConstructionCompanyResponse);
        }
    }
}
