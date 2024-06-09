using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using BuildingManagerILogic;
using BuildingManagerILogic.Exceptions;

namespace BuildingManagerLogic
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryLogic(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category AssignParent(Guid id, Guid parentId)
        {
            try
            {
                return _categoryRepository.AssignParent(id, parentId);
            }
            catch (ValueNotFoundException e)
            {
                throw new NotFoundException(e, e.Message);
            }
        }

        public Category CreateCategory(Category category)
        {
            try
            {
                return _categoryRepository.CreateCategory(category);
            }
            catch (ValueDuplicatedException e)
            {
                throw new DuplicatedValueException(e, e.Message);
            }

        }

        public List<Category> ListCategories()
        {
            return _categoryRepository.ListCategories();
        }
    }
}
