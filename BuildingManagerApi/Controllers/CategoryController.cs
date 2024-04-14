using BuildingManagerILogic;
using Microsoft.AspNetCore.Mvc;
using BuildingManagerModels.Outer;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerILogic.Exceptions;

namespace BuildingManagerApi.Controllers
{
    public class CategoryController: ControllerBase
    {
        private readonly ICategoryLogic _categoryLogic;

        public CategoryController(ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody] string categoryName)
        {
            try
            {
                CreateCategoryResponse createCategoryResponse = new CreateCategoryResponse(_categoryLogic.CreateCategory(categoryName));
                return CreatedAtAction(nameof(CreateCategory), createCategoryResponse);
            }
            catch (DuplicatedValueException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }
    }
}
