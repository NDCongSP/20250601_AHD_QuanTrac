namespace Application.Services.Authen;

[BasePath(ApiRoutes.RoleToPermissions.BasePath)]
public interface IRoleToPermissions:IRepository<Guid, RoleToPermission>
{
    [Get(ApiRoutes.RoleToPermissions.GetByPermissionId)]
    Task<Result<List<RoleToPermission>>> GetByPermissionsIdAsync([Path]string id);
}
