using BuildingManagerILogic;
using Microsoft.AspNetCore.Mvc;
using BuildingManagerModels.Outer;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.Inner;
using BuildingManagerDomain.Enums;
using BuildingManagerApi.Filters;

namespace BuildingManagerApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
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

        [HttpGet]
        public IActionResult GetCategories()
        {
            ListCategoriesResponse categoriesResponse = new(_categoryLogic.ListCategories());
            return Ok(categoriesResponse);
        }

        [HttpPut("{id}")]
        [AuthenticationFilter(RoleType.ADMIN)]
        public IActionResult AssignParent([FromRoute] Guid id, [FromBody] Guid parentId)
        {
            CategoryResponse assignParentResponse = new(_categoryLogic.AssignParent(id, parentId));
            return Ok(assignParentResponse);
        }

    }
}
