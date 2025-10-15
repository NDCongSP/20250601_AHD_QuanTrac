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
            services.AddScoped<Repository>();
            services.AddScoped<IAccount, RepositoryAccountServices>();
            services.AddScoped<IPermissions, RepositoryPermissionsServices>();
            services.AddScoped<IRoleToPermissions, RepositoryRoleToPermissionsServices>();
            services.AddScoped<IFT01, RepositoryFT01Services>();
            services.AddScoped<IFT02, RepositoryFT02Services>();
            services.AddScoped<IFT03, RepositoryFT03Services>();
            services.AddScoped<IFT05, RepositoryFT05Services>();
            services.AddScoped<IFT06, RepositoryFT06Services>();
            services.AddScoped<IFT07, RepositoryFT07Services>();
            services.AddScoped<IScadaUser, RepositoryScadaUserServices>();
        }
    }
}
