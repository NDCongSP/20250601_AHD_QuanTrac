namespace Application.Services.Authen
{
    [BasePath(ApiRoutes.RoleToPermissionTenant.BasePath)]
    public interface IRoleToPermissionTenant:IRepository<Guid,RoleToPermissionTenant>
    {
    }
}
