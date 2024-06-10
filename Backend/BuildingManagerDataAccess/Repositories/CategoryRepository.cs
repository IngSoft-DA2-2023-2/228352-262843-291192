using BuildingManagerDomain.Entities;
using BuildingManagerIDataAccess;
using BuildingManagerIDataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbContext _context;
        public CategoryRepository(DbContext context)
        {
            _context = context;
        }

        public Category AssignParent(Guid id, Guid parentId)
        {
            if (!_context.Set<Category>().Any(a => a.Id == id))
            {
                throw new ValueNotFoundException("Id");
            }
            if (!_context.Set<Category>().Any(a => a.Id == parentId))
            {
                throw new ValueNotFoundException("ParentId");
            }
            var isDescendant = IsDescendant(parentId, id);
            if (isDescendant)
            {
                throw new InvalidOperationException("Cannot assign a descendant category as the parent.");
            }
            Category category = _context.Set<Category>().First(c => c.Id == id);
            category.ParentId = parentId;
            _context.SaveChanges();
            category = _context.Set<Category>().Include(c => c.Parent).First(c => c.Id == id);
            return category;
        }

        private bool IsDescendant(Guid parentId, Guid id)
        {
            var children = _context.Set<Category>().Where(c => c.ParentId == id).ToList();
            foreach (var child in children)
            {
                if (child.Id == parentId || IsDescendant(parentId, child.Id))
                {
                    return true;
                }
            }
            return false;
        }

        public Category CreateCategory(Category category)
        {
            if (_context.Set<Category>().Any(a => a.Name == category.Name))
            {
                throw new ValueDuplicatedException("Name");
            }
            _context.Set<Category>().Add(category);
            _context.SaveChanges();
            return _context.Set<Category>().Include(c => c.Parent).First(c => c.Id == category.Id);
        }

        public List<Category> ListCategories()
        {
            return _context.Set<Category>().ToList();
        }
    }
}
