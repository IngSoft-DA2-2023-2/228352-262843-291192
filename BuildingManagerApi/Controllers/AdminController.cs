using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
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
        public IActionResult CreateAdmin([FromBody] CreateAdminRequest adminRequest)
        {
            try
            {
                CreateAdminResponse createAdminResponse = new CreateAdminResponse(_userLogic.CreateUser(adminRequest.ToEntity()));
                return CreatedAtAction(nameof(CreateAdmin), createAdminResponse);
            }
            catch (Exception ex) when (ex is DuplicatedValueException || ex is InvalidArgumentException)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}