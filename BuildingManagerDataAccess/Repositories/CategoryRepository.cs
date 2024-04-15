using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
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
            _context.Set<Category>().Add(category);
            _context.SaveChanges();
            return category;
        }
    }
}
