using BuildingManagerDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;

namespace BuildingManagerDataAccess.Context
{
    [ExcludeFromCodeCoverage]
    public class BuildingManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MaintenanceStaff> MaintenanceStaff { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<ConstructionCompany> ConstructionCompanies { get; set; }
        public DbSet<CompanyAdminAssociation> CompanyAdminAssociations { get; set; }

        public BuildingManagerContext(DbContextOptions<BuildingManagerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasDiscriminator<string>("user_type")
                    .HasValue<Admin>("admin_user")
                    .HasValue<Manager>("manager_user")
                    .HasValue<MaintenanceStaff>("maintenance_user")
                    .HasValue<ConstructionCompanyAdmin>("constructionCompanyAdmin_user");

            modelBuilder.Entity<Apartment>().ToTable("Apartments").HasKey(a => new { a.BuildingId, a.Floor, a.Number });
            modelBuilder.Entity<Owner>().ToTable("Owners").HasKey(o => o.Email);

            modelBuilder.Entity<Request>()
                .HasOne<Apartment>()
                .WithMany()
                .HasForeignKey(r => new { r.BuildingId, r.ApartmentFloor, r.ApartmentNumber });
            modelBuilder.Entity<Request>()
                        .HasOne<Category>(r => r.Category)
                        .WithMany()
                        .HasForeignKey(r => r.CategoryId);
            modelBuilder.Entity<Request>()
                        .HasOne<MaintenanceStaff>(r => r.MaintenanceStaff)
                        .WithMany()
                        .HasForeignKey(r => r.MaintainerStaffId)
                        .IsRequired(false);
            modelBuilder.Entity<Request>()
                        .HasOne<Manager>()
                        .WithMany()
                        .HasForeignKey(r => r.ManagerId);

            modelBuilder.Entity<Building>()
                        .HasMany<Apartment>(b => b.Apartments)
                        .WithOne()
                        .HasForeignKey(a => a.BuildingId)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Apartment>()
                        .HasOne<Owner>(a => a.Owner)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Apartment>()
                        .HasMany<Request>()
                        .WithOne()
                        .HasForeignKey(r => new { r.BuildingId, r.ApartmentFloor, r.ApartmentNumber })
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Request>()
                        .HasOne(r => r.MaintenanceStaff)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Request>()
                        .HasOne<Building>()
                        .WithMany()
                        .HasForeignKey(r => r.BuildingId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Building>()
                        .HasOne<ConstructionCompany>()
                        .WithMany()
                        .HasForeignKey(b => b.ConstructionCompanyId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompanyAdminAssociation>()
            .HasKey(c => new { c.ConstructionCompanyAdminId, c.ConstructionCompanyId });

            modelBuilder.Entity<CompanyAdminAssociation>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.ConstructionCompanyAdminId);

            modelBuilder.Entity<CompanyAdminAssociation>()
                .HasOne<ConstructionCompany>()
                .WithMany()
                .HasForeignKey(c => c.ConstructionCompanyId);
        }
    }
}
