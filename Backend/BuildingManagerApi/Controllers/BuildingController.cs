using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using BuildingManagerApi.Filters;
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
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult CreateBuilding([FromBody] CreateBuildingRequest buildingRequest, [FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            CreateBuildingResponse createBuildingResponse = new CreateBuildingResponse(_buildingLogic.CreateBuilding(buildingRequest.ToEntity(), sessionToken));
            return CreatedAtAction(nameof(CreateBuilding), createBuildingResponse);
        }

        [HttpDelete("{buildingId}")]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult DeleteBuilding([FromRoute] Guid buildingId, [FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            DeleteBuildingResponse deleteBuildingResponse = new DeleteBuildingResponse(_buildingLogic.DeleteBuilding(buildingId, sessionToken));
            return Ok(deleteBuildingResponse);
        }

        [HttpPut("{buildingId}")]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult UpdateBuilding([FromRoute] Guid buildingId, [FromBody] UpdateBuildingRequest buildingRequest)
        {
            buildingRequest.Id = buildingId;
            UpdateBuildingResponse updateBuildingResponse = new UpdateBuildingResponse(_buildingLogic.UpdateBuilding(buildingRequest.ToEntity()));
            return CreatedAtAction(nameof(UpdateBuilding), updateBuildingResponse);
        }

        [HttpGet]
        [AuthenticationFilter(RoleType.ADMIN)]
        public IActionResult ListBuildings()
        {
            ListBuildingsResponse listBuildingsResponse = new(_buildingLogic.ListBuildings());
            return Ok(listBuildingsResponse);
        }

        [HttpPut("{buildingId}/managers")]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult UpdateBuildingManager([FromRoute] Guid buildingId, [FromBody] UpdateBuildingManagerRequest updateManagerRequest)
        {
            updateManagerRequest.Validate();
            UpdateBuildingManagerResponse updateBuildingManagerResponse = new UpdateBuildingManagerResponse(_buildingLogic.ModifyBuildingManager(updateManagerRequest.ManagerId, buildingId), buildingId);
            return Ok(updateBuildingManagerResponse);
        }

        [HttpGet("{buildingId}")]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult GetBuildingDetails([FromRoute] Guid buildingId)
        {
            BuildingDetailsResponse getBuildingByNameResponse = new BuildingDetailsResponse(_buildingLogic.GetBuildingDetails(buildingId));
            return Ok(getBuildingByNameResponse);
        }
    }
}
