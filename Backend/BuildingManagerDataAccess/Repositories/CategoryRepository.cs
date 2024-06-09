using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private DbContext _context;
        public CategoryRepository(DbContext context)
        {
            _context = context;
        }

        public Category AssignParent(Guid id, Guid parentId)
        {
            Category category = _context.Set<Category>().First(c => c.Id == id);
            category.ParentId = parentId;
            _context.SaveChanges();
            return category;
        }

        public Category CreateCategory(Category category)
        {
            if (_context.Set<Category>().Any(a => a.Name == category.Name))
            {
                throw new ValueDuplicatedException("Name");
            }
            _context.Set<Category>().Add(category);
            _context.SaveChanges();
            return category;
        }

        public List<Category> ListCategories()
        {
            return _context.Set<Category>().ToList();
        }
    }
}
