namespace Application.Services
{
    [BasePath(ApiRoutes.Tenants.BasePath)]
    public interface ITenants : IRepository<int, TenantAuth>
    {

    }
}
