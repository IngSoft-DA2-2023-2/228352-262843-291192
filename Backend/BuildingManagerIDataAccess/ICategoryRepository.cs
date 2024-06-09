using BuildingManagerDomain.Entities;

namespace BuildingManagerIDataAccess
{
    public interface ICategoryRepository
    {
        Category CreateCategory(Category category);
        List<Category> ListCategories();
    }
}
