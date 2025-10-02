using Application.Services;
using Application.Services.Authen;

namespace Infrastructure.Repositories;

public class Repository
{
    public IPermissions SPermissions { get; set; }
    public IRoleToPermissions SRoleToPermissions { get; set; }
    public IFT01 SFT01s { get; set; }
    public IFT02 SFT02s { get; set; }
    public IFT03 SFT03s { get; set; }
    public IFT05 SFT05s { get; set; }
    public IFT06 SFT06s { get; set; }
    public IFT07 SFT07s { get; set; }

    public Repository(IPermissions sPermissions = null, IRoleToPermissions sRoleToPermissions = null, IFT01 sFT01s = null, IFT02 sFT02s = null
        , IFT03 sFT03s = null, IFT05 sFT05s = null, IFT06 sFT06s = null, IFT07 sFT07s = null)
    {
        SPermissions = sPermissions;
        SRoleToPermissions = sRoleToPermissions;
        SFT01s = sFT01s;
        SFT02s = sFT02s;
        SFT03s = sFT03s;
        SFT05s = sFT05s;
        SFT06s = sFT06s;
        SFT07s = sFT07s;
    }
}
