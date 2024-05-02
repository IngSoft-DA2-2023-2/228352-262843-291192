using Microsoft.AspNetCore.Mvc;
using BuildingManagerModels.Outer;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/logout")]
    public class LogoutController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public LogoutController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        public IActionResult Logout([FromHeader(Name = "Authorization")] Guid sessionToken)
        {
            LogoutResponse logoutResponse = new(_userLogic.Logout(sessionToken));
            return Ok(logoutResponse);
        }
    }
}