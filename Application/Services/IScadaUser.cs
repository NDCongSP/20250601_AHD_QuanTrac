using Application.DTOs.Request.Account;

namespace Application.Services;

[BasePath(ApiRoutes.ScadaUser.BasePath)]
public interface IScadaUser : IRepository<Guid, ScadaUser>
{
    [Post(ApiRoutes.ScadaUser.ChangePassword)]
    Task<Result<ScadaUser>> ChangePasswordAsync([Body] ChangePassRequestDTO model);

    [Post(ApiRoutes.ScadaUser.ResetPassword)]
    Task<Result<bool>> ResetPasswordAsync([Path] Guid id);
}
