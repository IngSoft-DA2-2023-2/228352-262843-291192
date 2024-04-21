using Microsoft.AspNetCore.Mvc;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/invitations")]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationLogic _invitationLogic;

        public InvitationController(IInvitationLogic invitationLogic)
        {
            _invitationLogic = invitationLogic;
        }

        [HttpPost]
        public IActionResult CreateInvitation([FromBody] CreateInvitationRequest invitationRequest)
        {
            try
            {
                CreateInvitationResponse createInvitationResponse = new(_invitationLogic.CreateInvitation(invitationRequest.ToEntity()));
                return CreatedAtAction(nameof(CreateInvitation), createInvitationResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }

        }
    }
}