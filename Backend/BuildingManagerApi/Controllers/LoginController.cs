using Microsoft.AspNetCore.Mvc;
using BuildingManagerModels.Outer;
using BuildingManagerILogic;
using BuildingManagerModels.Inner;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public LoginController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            LoginResponse loginResponse = new(_userLogic.Login(loginRequest.Email, loginRequest.Password));
            return CreatedAtAction(nameof(Login), loginResponse);
        }
    }
}