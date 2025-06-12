namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleToPermissionTenantController : BaseController<Guid, RoleToPermissionTenant>, IRoleToPermissionTenant
    {
        readonly Repository _repository;
        public RoleToPermissionTenantController(Repository repository = null!) : base(repository.SRoleToPermissionTenant)
        {
            _repository = repository;
        }
    }
}
