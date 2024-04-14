using BuildingManagerDataAccess;
using BuildingManagerDataAccess.Context;
using BuildingManagerIDataAccess;
using BuildingManagerILogic;
using BuildingManagerLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingManagerServiceFactory
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IAdminLogic, AdminLogic>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddDbContext<DbContext, BuildingManagerContext>(o => o.UseSqlServer(connectionString));
            return services;
        }
    }
}
