using Microsoft.AspNetCore.Mvc;
using BuildingManagerModels.Outer;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using ECommerceApi.Filters;
using BuildingManagerDomain.Enums;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/requests")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestLogic _requestLogic;

        public RequestController(IRequestLogic requestLogic)
        {
            _requestLogic = requestLogic;
        }

        [HttpPost]
        [AuthenticationFilter(RoleType.MANAGER)]
        public IActionResult CreateRequest([FromBody] CreateRequestRequest requestRequest)
        {
            RequestResponse createRequestResponse = new(_requestLogic.CreateRequest(requestRequest.ToEntity()));
            return CreatedAtAction(nameof(CreateRequest), createRequestResponse);
        }

        [HttpGet]
        [AuthenticationFilter(RoleType.MANAGER)]
        public IActionResult GetRequests([FromQuery] string[] categories)
        {
            var allRequests = _requestLogic.GetRequests();
            return Ok(allRequests);
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(RoleType.MANAGER)]
        public IActionResult AssignStaff([FromRoute] Guid id, [FromBody] Guid maintenanceStaffId)
        {
            RequestResponse updateRequestResponse = new(_requestLogic.AssignStaff(id, maintenanceStaffId));
            return Ok(updateRequestResponse);
        }
    }
}