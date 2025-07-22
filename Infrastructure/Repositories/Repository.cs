using Application.Services;
using Application.Services.Authen;

namespace Infrastructure.Repositories;

public class Repository
{
    public IPermissions SPermissions { get; set; }
    public IRoleToPermissions SRoleToPermissions { get; set; }
    public IFT01 SFT01s { get; set; }

    public Repository(IPermissions sPermissions = null, IRoleToPermissions sRoleToPermissions = null, IFT01 sFT01s = null)
    {
        SPermissions = sPermissions;
        SRoleToPermissions = sRoleToPermissions;
        SFT01s = sFT01s;
    }
}
