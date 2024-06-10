using BuildingManagerApi.Filters;
using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/owners")]
    public class OwnerController: ControllerBase
    {
        private readonly IOwnerLogic _ownerLogic;

        public OwnerController(IOwnerLogic ownerLogic)
        {
            _ownerLogic = ownerLogic;
        }

        [HttpPost]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult CreateOwner([FromBody] CreateOwnerRequest ownerRequest)
        {
            CreateOwnerResponse createOwnerResponse = new CreateOwnerResponse(_ownerLogic.CreateOwner(ownerRequest.ToEntity()));
            return CreatedAtAction(nameof(CreateOwner), createOwnerResponse);
        }
    }
}
