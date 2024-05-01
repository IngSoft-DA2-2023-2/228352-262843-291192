using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using ECommerceApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class BuildingsReportController : ControllerBase
    {
        private readonly IReportLogic _reportLogic;
        public BuildingsReportController(IReportLogic reportLogic)
        {
            _reportLogic = reportLogic;
        }

        [HttpGet]
        [AuthenticationFilter(RoleType.MANAGER)]
        public IActionResult GetReport([FromQuery] Guid? buildingId)
        {
            BuildingsReportResponse buildingsReportResponse = new(_reportLogic.GetReport(null, buildingId.ToString(), ReportType.BUILDINGS));
            return Ok(buildingsReportResponse);
        }
    }
}