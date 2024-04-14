using BuildingManagerDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Context
{
    public class BuildingManagerContext: DbContext
    {
        public DbSet<Admin> Admins { get; set; }

        public BuildingManagerContext(DbContextOptions<BuildingManagerContext> options) : base(options)
        {
        }
    }
}
