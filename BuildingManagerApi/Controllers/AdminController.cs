using BuildingManagerILogic;
using BuildingManagerModels.Inner;
using BuildingManagerModels.Outer;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/admins")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminLogic _adminLogic;

        public AdminController(IAdminLogic adminLogic)
        {
            _adminLogic = adminLogic;
        }

        [HttpPost]
        public IActionResult CreateAdmin([FromBody] CreateAdminRequest adminRequest)
        {
            try
            {
                CreateAdminResponse createAdminResponse = new CreateAdminResponse(_adminLogic.CreateAdmin(adminRequest.ToEntity()));
                return CreatedAtAction(nameof(CreateAdmin), createAdminResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
        }
    }
}