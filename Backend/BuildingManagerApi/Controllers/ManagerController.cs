using Microsoft.AspNetCore.Mvc;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using BuildingManagerDomain.Enums;
using BuildingManagerApi.Filters;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/managers")]
    public class ManagerController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        private readonly IBuildingLogic _buildingLogic;

        public ManagerController(IUserLogic userLogic, IBuildingLogic buildingLogic)
        {
            _userLogic = userLogic;
            _buildingLogic = buildingLogic;
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter(RoleType.ADMIN)]
        public IActionResult DeleteManager([FromRoute] Guid id)
        {
            ManagerResponse deleteManagerResponse = new(_userLogic.DeleteUser(id, RoleType.MANAGER));
            return Ok(deleteManagerResponse);
        }

        [HttpGet("{id}/buildings")]
        [AuthenticationFilter(RoleType.MANAGER)]
        public IActionResult GetManagerBuildings([FromRoute] Guid id)
        {
            ManagerBuildingsResponse managerBuildingsResponse = new(_buildingLogic.GetManagerBuildings(id));
            return Ok(managerBuildingsResponse);
        }
    }
}