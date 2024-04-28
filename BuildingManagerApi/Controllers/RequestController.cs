using Microsoft.AspNetCore.Mvc;
using BuildingManagerModels.Outer;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;

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
        public IActionResult CreateRequest([FromBody] CreateRequestRequest requestRequest)
        {
            RequestResponse createRequestResponse = new(_requestLogic.CreateRequest(requestRequest.ToEntity()));
            return CreatedAtAction(nameof(CreateRequest), createRequestResponse);
        }
    }
}