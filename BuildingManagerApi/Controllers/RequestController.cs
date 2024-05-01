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

        [HttpPut("{id}/attendance")]
        [AuthenticationFilter(RoleType.MAINTENANCE)]
        public IActionResult AttendRequest([FromRoute] Guid id, [FromHeader(Name = "Authorization")] Guid managerSessionToken)
        {
            RequestResponse updateRequestResponse = new(_requestLogic.AttendRequest(id, managerSessionToken));
            return Ok(updateRequestResponse);
        }

        [HttpPut("{id}/completed")]
        [AuthenticationFilter(RoleType.MAINTENANCE)]
        public IActionResult CompleteRequest([FromRoute] Guid id, int cost)
        {
            RequestResponse updateRequestResponse = new(_requestLogic.CompleteRequest(id, cost));
            return Ok(updateRequestResponse);
        }

        [HttpGet]
        [Route("assigned")]
        [AuthenticationFilter(RoleType.MAINTENANCE)]
        public IActionResult GetAssignedRequests([FromHeader(Name = "Authorization")] Guid managerSessionToken)
        {
            var assignedRequests = _requestLogic.GetAssignedRequests(managerSessionToken);
            return Ok(assignedRequests);
        }
    }
}