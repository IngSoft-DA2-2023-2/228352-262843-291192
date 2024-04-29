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
        public IActionResult Logout()
        {
            string? stringSessionToken = HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(stringSessionToken) || !Guid.TryParse(stringSessionToken, out _))
            {
                return new ObjectResult(new { ErrorMessage = "A token is required" })
                {
                    StatusCode = 401
                };
            }
            Guid sessionToken = Guid.Parse(stringSessionToken);

            LogoutResponse logoutResponse = new(_userLogic.Logout(sessionToken));
            return Ok(logoutResponse);
        }
    }
}