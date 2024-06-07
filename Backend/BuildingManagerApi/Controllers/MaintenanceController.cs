using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using BuildingManagerApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/maintenances")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public MaintenanceController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        [AuthenticationFilter(RoleType.ADMIN)]
        public IActionResult CreateMaintenanceStaff([FromBody] CreateMaintenanceStaffRequest maintenanceStaffRequest)
        {
            CreateMaintenanceStaffResponse createMaintenanceStaffResponse = new CreateMaintenanceStaffResponse(_userLogic.CreateUser(maintenanceStaffRequest.ToEntity()));
            return CreatedAtAction(nameof(CreateMaintenanceStaff), createMaintenanceStaffResponse);
        }

        [HttpGet]
        [AuthenticationFilter(RoleType.MANAGER)]
        public IActionResult GetMaintainers()
        {
            MaintainersResponse maintainerResponse = new(_userLogic.GetMaintenanceStaff());
            return Ok(maintainerResponse);
        }
    }
}