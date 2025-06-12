namespace API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RoleToPermissionsController : BaseController<Guid, RoleToPermission>, IRoleToPermissions
{
    readonly Repository _repository;

    public RoleToPermissionsController(Repository repository = null!) : base(repository.SRoleToPermissions)
    {
        _repository = repository;
    }

    [HttpGet(ApiRoutes.RoleToPermissions.GetByPermissionId)]
    public async Task<Result<List<RoleToPermission>>> GetByPermissionsIdAsync([Path] string id)
    {
        return await _repository.SRoleToPermissions.GetByPermissionsIdAsync(id);
    }
}
