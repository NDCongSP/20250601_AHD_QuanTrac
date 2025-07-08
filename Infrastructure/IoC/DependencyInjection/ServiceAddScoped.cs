using Application.Services;
using Application.Services.Authen;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
namespace Infrastructure.IoC.DependencyInjection
{
    public static class ServiceAddScoped
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAccount, RepositoryAccountServices>();
            services.AddScoped<IPermissions, RepositoryPermissionsServices>();
            services.AddScoped<IRoleToPermissions, RepositoryRoleToPermissionsServices>();
            services.AddScoped<Repository>();
        }
    }
}
