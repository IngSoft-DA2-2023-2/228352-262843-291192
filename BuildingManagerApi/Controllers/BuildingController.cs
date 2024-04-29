using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using ECommerceApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/buildings")]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingLogic _buildingLogic;

        public BuildingController(IBuildingLogic buildingLogic)
        {
            _buildingLogic = buildingLogic;
        }

        [HttpPost]
        [AuthenticationFilter(RoleType.MANAGER)]
        public IActionResult CreateBuilding([FromBody] CreateBuildingRequest buildingRequest)
        {
            Guid managerId = Guid.Parse(HttpContext.Request.Headers["Authorization"]!);
            buildingRequest.ManagerId = managerId;

            CreateBuildingResponse createBuildingResponse = new CreateBuildingResponse(_buildingLogic.CreateBuilding(buildingRequest.ToEntity()));
            return CreatedAtAction(nameof(CreateBuilding), createBuildingResponse);
        }
    }
}
