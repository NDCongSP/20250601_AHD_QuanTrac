using Application.Services.Authen;

namespace Infrastructure.Repositories;

public class Repository
{
    public IAccount SAccount { get; set; }
    public IPermissions SPermissions { get; set; }
    public IRoleToPermissions SRoleToPermissions { get; set; }

    public Repository(IPermissions sPermissions = null
        , IRoleToPermissions sRoleToPermissions = null,
        IAccount sAccount = null)
    {
        SPermissions = sPermissions;
        SRoleToPermissions = sRoleToPermissions;
        SAccount = sAccount;
    }
}
