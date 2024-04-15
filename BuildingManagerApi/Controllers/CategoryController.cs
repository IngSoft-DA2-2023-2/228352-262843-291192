using BuildingManagerILogic;
using Microsoft.AspNetCore.Mvc;
using BuildingManagerModels.Outer;
using BuildingManagerModels.CustomExceptions;
using BuildingManagerILogic.Exceptions;
using BuildingManagerModels.Inner;

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
        public IActionResult CreateCategory([FromBody] CreateCategoryRequest categoryRequest)
        {
            try
            {
                CreateCategoryResponse createCategoryResponse = new CreateCategoryResponse(_categoryLogic.CreateCategory(categoryRequest.ToEntity()));
                return CreatedAtAction(nameof(CreateCategory), createCategoryResponse);
            }
            catch (Exception ex) when (ex is DuplicatedValueException || ex is InvalidArgumentException)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch
            {
                return StatusCode(500);
            }
            
        }
    }
}
