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
            throw new NotImplementedException();
        }
    }
}
