using Application.Services.Authen;

namespace Infrastructure.Repositories;

public class Repository
{
    public IPermissions SPermissions { get; set; }
    public IRoleToPermissions SRoleToPermissions { get; set; }

    public Repository(IPermissions sPermissions = null, IRoleToPermissions sRoleToPermissions = null)
    {
        SPermissions = sPermissions;
        SRoleToPermissions = sRoleToPermissions;
    }
}
