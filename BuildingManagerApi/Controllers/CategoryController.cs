using BuildingManagerILogic;
using Microsoft.AspNetCore.Mvc;
using BuildingManagerModels.Outer;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.Inner;
using BuildingManagerDomain.Enums;
using ECommerceApi.Filters;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryLogic _categoryLogic;

        public CategoryController(ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        [HttpPost]
        [AuthenticationFilter(RoleType.ADMIN)]
        public IActionResult CreateCategory([FromBody] CreateCategoryRequest categoryRequest)
        {
            CreateCategoryResponse createCategoryResponse = new CreateCategoryResponse(_categoryLogic.CreateCategory(categoryRequest.ToEntity()));
            return CreatedAtAction(nameof(CreateCategory), createCategoryResponse);
        }
    }
}
