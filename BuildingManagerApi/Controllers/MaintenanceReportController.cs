using BuildingManagerILogic;
using BuildingManagerModels.Outer;
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
        public IActionResult GetReport([FromRoute] Guid buildingId)
        {
            MaintenanceReportResponse maintenanceReportResponse = new(_reportLogic.GetReport());
            return Ok(maintenanceReportResponse);
        }
    }
}