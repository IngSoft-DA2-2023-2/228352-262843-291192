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
        public IActionResult CreateConstructionCompany([FromBody] ConstructionCompanyRequest constructionCompanyRequest, [FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            ConstructionCompanyResponse createConstructionCompanyResponse = new(_constructionCompanyLogic.CreateConstructionCompany(constructionCompanyRequest.ToEntity(), sessionToken));
            return CreatedAtAction(nameof(CreateConstructionCompany), createConstructionCompanyResponse);
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult ModifyConstructionCompanyName([FromRoute] Guid id, [FromBody] ConstructionCompanyRequest constructionCompanyRequest, [FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            constructionCompanyRequest.Validate();
            ConstructionCompanyResponse modifyNameResponse = new(_constructionCompanyLogic.ModifyName(id, constructionCompanyRequest.Name, sessionToken));
            return Ok(modifyNameResponse);
        }
    }
}
