using BuildingManagerDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildingManagerDataAccess.Context
{
    public class BuildingManagerContext: DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MaintenanceStaff> MaintenanceStaff { get; set; }

        public BuildingManagerContext(DbContextOptions<BuildingManagerContext> options) : base(options)
        {
        }
    }
}
