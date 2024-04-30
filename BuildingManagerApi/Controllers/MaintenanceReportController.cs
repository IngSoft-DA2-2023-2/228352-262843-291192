using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using ECommerceApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class MaintenanceReportController : ControllerBase
    {
        private readonly IReportLogic _reportLogic;
        public MaintenanceReportController(IReportLogic reportLogic)
        {
            _reportLogic = reportLogic;
        }

        [HttpGet("{buildingId}/maintenances")]
        [AuthenticationFilter(RoleType.MANAGER)]
        public IActionResult GetReport([FromRoute] Guid buildingId)
        {
            MaintenanceReportResponse maintenanceReportResponse = new(_reportLogic.GetReport(buildingId));
            return Ok(maintenanceReportResponse);
        }
    }
}