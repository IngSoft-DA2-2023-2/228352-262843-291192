using BuildingManagerDataAccess.Context;
using BuildingManagerDataAccess.Repositories;
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
            services.AddScoped<ICategoryLogic, CategoryLogic>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBuildingLogic, BuildingLogic>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<IRequestLogic, RequestLogic>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IInvitationLogic, InvitationLogic>();
            services.AddScoped<IReportLogic, ReportLogic>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IConstructionCompanyLogic, ConstructionCompanyLogic>();
            services.AddScoped<IConstructionCompanyAdminLogic, ConstructionCompanyAdminLogic>();
            services.AddScoped<IConstructionCompanyRepository, ConstructionCompanyRepository>();
            services.AddDbContext<DbContext, BuildingManagerContext>(o => o.UseSqlServer(connectionString));
            return services;
        }
    }
}
