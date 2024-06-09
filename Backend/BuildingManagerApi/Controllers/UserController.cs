using BuildingManagerApi.Filters;
using BuildingManagerDomain.Entities;
using BuildingManagerDomain.Enums;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpGet("managers")]
        [AuthenticationFilter(RoleType.CONSTRUCTIONCOMPANYADMIN, RoleType.ADMIN)]
        public IActionResult ListManagerUsers()
        {
            ListManagersResponse listManagersResponse = new(_userLogic.GetManagers());
            return Ok(listManagersResponse);
        }
    }
}
