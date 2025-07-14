namespace Application.Services.Authen
{
    [BasePath(ApiRoutes.Permissions.BasePath)]
    public interface IPermissions : IRepository<Guid, Permissions>
    {
        [Get(ApiRoutes.Permissions.GetAllPermissionWithAssignedRole)]
        Task<Result<List<PermissionsListResponseDTO>>> GetAllPermissionWithAssignedRoleAsync();
        [Post(ApiRoutes.Permissions.AddOrEdit)]
        Task<Result<PermissionsListResponseDTO>> AddOrEditAsync([Body] PermissionsListResponseDTO model);
    }
}
