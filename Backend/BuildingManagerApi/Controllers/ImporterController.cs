using BuildingManagerApi.Filters;
using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/importers")]
    public class ImporterController : ControllerBase
    {
        private readonly IImporterLogic _importerLogic;
        public ImporterController(IImporterLogic importerLogic)
        {
            _importerLogic = importerLogic;
        }

        [HttpGet]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]
        public IActionResult GetImporters()
        {
            ListImportersResponse importers = new ListImportersResponse(_importerLogic.ListImporters());
            return Ok(importers);
        }

        [HttpPost("{importerName}")]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN)]

        public IActionResult ImportData([FromRoute] string importerName, [FromBody] string path, [FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            ImportBuildingsResponse data = new ImportBuildingsResponse(_importerLogic.ImportData(importerName, path, sessionToken));
            return CreatedAtAction(nameof(ImportData), data);
        }
    }
}
