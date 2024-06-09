using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using BuildingManagerApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class CategoriesReportController : ControllerBase
    {
        private readonly IReportLogic _reportLogic;
        public CategoriesReportController(IReportLogic reportLogic)
        {
            _reportLogic = reportLogic;
        }

        [HttpGet("{buildingId}/categories")]
        [AuthenticationFilter(RoleType.ADMIN)]
        public IActionResult GetReport([FromRoute] Guid buildingId, [FromQuery] string? categoryName)
        {
            CategoriesReportResponse categoriesReportResponse = new(_reportLogic.GetReport(buildingId, categoryName, ReportType.CATEGORIES));
            return Ok(categoriesReportResponse);
        }
    }
}
