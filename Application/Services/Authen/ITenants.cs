using Application.Extentions;
using Application.Services.Base;
using Domain;
using RestEase;

namespace Application.Services
{
    [BasePath(ApiRoutes.Tenants.BasePath)]
    public interface ITenants : IRepository<int, TenantAuth>
    {

    }
}
