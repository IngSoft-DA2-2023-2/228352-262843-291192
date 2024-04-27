using Microsoft.AspNetCore.Mvc;
using BuildingManagerILogic;
using BuildingManagerModels.Outer;
using BuildingManagerDomain.Enums;
using ECommerceApi.Filters;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/managers")]
    public class ManagerController : ControllerBase
    {
        private readonly IUserLogic _userLogic;

        public ManagerController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpDelete("{id}")]
        [AuthenticationFilter(RoleType.ADMIN)]
        public IActionResult DeleteManager([FromRoute] Guid id)
        {
            ManagerResponse deleteManagerResponse = new(_userLogic.DeleteUser(id));
            return Ok(deleteManagerResponse);
        }
    }
}