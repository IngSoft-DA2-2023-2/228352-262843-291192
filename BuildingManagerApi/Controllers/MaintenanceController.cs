using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/maintenances")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IMaintenanceLogic _maintenanceLogic;

        public MaintenanceController(IMaintenanceLogic maintenanceLogic)
        {
            _maintenanceLogic = maintenanceLogic;
        }

        [HttpPost]
        public IActionResult CreateMaintenanceStaff([FromBody] CreateMaintenanceStaffRequest maintenanceStaffRequest)
        {
            try
            {
                CreateMaintenanceStaffResponse createMaintenanceStaffResponse = new CreateMaintenanceStaffResponse(_maintenanceLogic.CreateMaintenanceStaff(maintenanceStaffRequest.ToEntity()));
                return CreatedAtAction(nameof(CreateMaintenanceStaff), createMaintenanceStaffResponse);
            }
            catch (Exception ex) when (ex is DuplicatedValueException || ex is InvalidArgumentException)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}