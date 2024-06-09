using BuildingManagerDomain.Entities;

namespace BuildingManagerILogic
{
    public interface ICategoryLogic
    {
        public Category CreateCategory(Category category);
        public List<Category> ListCategories();
        public Category AssignParent(Guid id, Guid parentId);
    }
}
