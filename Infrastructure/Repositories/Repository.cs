using Application.Services;
using Application.Services.Authen;

namespace Infrastructure.Repositories;

public class Repository
{
    public IPermissions SPermissions { get; set; }
    public IPermissionTenant SPermissionTenant { get; set; }
    public IRoleToPermissions SRoleToPermissions { get; set; }
    public IRoleToPermissionTenant SRoleToPermissionTenant { get; set; }
    public ITenants STennats { get; set; }
    public IUserToTenant SUserToTenant { get; set; }

    public Repository(IPermissions sPermissions = null, IPermissionTenant sPermissionTenant = null
        , IRoleToPermissions sRoleToPermissions = null, IRoleToPermissionTenant sRoleToPermissionTenant = null
        , ITenants sTennats = null, IUserToTenant sUserToTenant = null)
    {
        SPermissions = sPermissions;
        SPermissionTenant = sPermissionTenant;
        SRoleToPermissions = sRoleToPermissions;
        SRoleToPermissionTenant = sRoleToPermissionTenant;
        STennats = sTennats;
        SUserToTenant = sUserToTenant;
    }
}
