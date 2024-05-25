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

        public ConstructionCompanyAdminController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        public IActionResult CreateConstructionCompanyAdmin([FromBody] CreateConstructionCompanyAdminRequest constructionCompanyAdminRequest)
        {
            CreateConstructionCompanyAdminResponse createConstructionCompanyAdminResponse = new CreateConstructionCompanyAdminResponse(_userLogic.CreateUser(constructionCompanyAdminRequest.ToEntity()));
            return CreatedAtAction(nameof(CreateConstructionCompanyAdmin), createConstructionCompanyAdminResponse);
        }
    }
}