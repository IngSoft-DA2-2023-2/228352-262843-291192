using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerLogic;
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
            Guid managerSessionToken = Guid.Parse(HttpContext.Request.Headers["Authorization"]!);
            _buildingLogic.GetManagerIdBySessionToken(managerSessionToken);

            CreateBuildingResponse createBuildingResponse = new CreateBuildingResponse(_buildingLogic.CreateBuilding(buildingRequest.ToEntity()));
            return CreatedAtAction(nameof(CreateBuilding), createBuildingResponse);
        }

        [HttpDelete("{buildingId}")]
        [AuthenticationFilter(RoleType.MANAGER)]
        public IActionResult DeleteBuilding([FromRoute] Guid buildingId)
        {
            DeleteBuildingResponse deleteBuildingResponse = new DeleteBuildingResponse(_buildingLogic.DeleteBuilding(buildingId));
            return Ok(deleteBuildingResponse);
        }
    }
}
