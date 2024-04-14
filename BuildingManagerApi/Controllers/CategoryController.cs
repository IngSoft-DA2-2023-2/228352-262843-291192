using BuildingManagerILogic;
using Microsoft.AspNetCore.Mvc;
using BuildingManagerModels.Outer;

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
            CreateCategoryResponse createCategoryResponse = new CreateCategoryResponse(_categoryLogic.CreateCategory(categoryName));
            return CreatedAtAction(nameof(CreateCategory), createCategoryResponse);
        }
    }
}
