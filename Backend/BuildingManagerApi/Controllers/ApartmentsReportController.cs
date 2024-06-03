using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using BuildingManagerApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ApartmentsReportController : ControllerBase
    {
        private readonly IReportLogic _reportLogic;
        public ApartmentsReportController(IReportLogic reportLogic)
        {
            _reportLogic = reportLogic;
        }

        [HttpGet("{buildingId}/apartments")]
        [AuthenticationFilter(RoleType.MANAGER)]
        public IActionResult GetReport([FromRoute] Guid buildingId)
        {
            ApartmentsReportResponse apartmentsReportResponse = new(_reportLogic.GetReport(buildingId, null, ReportType.APARTMENTS));
            return Ok(apartmentsReportResponse);
        }
    }
}
