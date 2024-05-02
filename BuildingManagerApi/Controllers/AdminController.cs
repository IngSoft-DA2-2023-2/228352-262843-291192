using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using BuildingManagerApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/admins")]
    public class AdminController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public AdminController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        [AuthenticationFilter(RoleType.ADMIN)]
        public IActionResult CreateAdmin([FromBody] CreateAdminRequest adminRequest)
        {
            CreateAdminResponse createAdminResponse = new CreateAdminResponse(_userLogic.CreateUser(adminRequest.ToEntity()));
            return CreatedAtAction(nameof(CreateAdmin), createAdminResponse);
        }
    }
}