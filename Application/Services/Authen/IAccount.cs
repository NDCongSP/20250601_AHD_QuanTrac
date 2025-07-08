using Application.DTOs.Request.Account;

namespace Application.Services.Authen;

[BasePath(ApiRoutes.Identity.BasePath)]
public interface IAccount
{
    [Post(ApiRoutes.Identity.CreateSuperAdminAsync)]
    Task<GeneralResponse> CreateSuperAdminAsync();

    [Post(ApiRoutes.Identity.CreateAccountAsync)]
    Task<GeneralResponse> CreateAccountAsync([Body] CreateAccountRequestDTO model);

    [Post(ApiRoutes.Identity.LoginAccountAsync)]
    Task<Result<LoginResponse>> LoginAccountAsync([Body] LoginRequestDTO model);

    [Post(ApiRoutes.Identity.RefreshTokenAsync)]
    Task<Result<LoginResponse>> RefreshTokenAsync([Body] RefreshTokenRequestDTO model);

    [Post(ApiRoutes.Identity.CreateRoleAsync)]
    Task<GeneralResponse> CreateRoleAsync([Body] CreateRoleRequestDTO model);

    [Get(ApiRoutes.Identity.GetRolesAsync)]
    Task<List<GetRoleResponseDTO>> GetRolesAsync();

    [Get(ApiRoutes.Identity.GetUsersWithRolesAsync)]
    Task<List<GetUserWithRoleResponseDTO>> GetUsersWithRolesAsync();
    
    [Post(ApiRoutes.Identity.ChangeUserRoleAsync)]
    Task<GeneralResponse> ChangeUserRoleAsync([Body] AssignUserRoleRequestDTO model);

    [Post(ApiRoutes.Identity.ChangePassAsync)]
    Task<GeneralResponse> ChangePassAsync([Body] ChangePassRequestDTO model);

    [Post(ApiRoutes.Identity.AssignUserRoleAsync)]
    Task<GeneralResponse> AssignUserRoleAsync([Body] AssignUserRoleRequestDTO model);

    [Post(ApiRoutes.Identity.DeleteUserAsync)]
    Task<GeneralResponse> DeleteUserAsync([Body] UpdateDeleteRequestDTO model);

    [Post(ApiRoutes.Identity.DeleteUserRoleAsync)]
    Task<GeneralResponse> DeleteUserRoleAsync([Body] AssignUserRoleRequestDTO model);

    [Post(ApiRoutes.Identity.UpdateRoleAsync)]
    Task<GeneralResponse> UpdateRoleAsync([Body] UpdateDeleteRequestDTO model);

    [Post(ApiRoutes.Identity.UpdateRoleDTOAsync)]
    Task<GeneralResponse> UpdateRoleDTOAsync([Body] CreateRoleRequestDTO model);

    [Post(ApiRoutes.Identity.UpdateUserInfoAsync)]
    Task<GeneralResponse> UpdateUserInfoAsync([Body]UpdateUserInfoRequestDTO model);

    [Get(ApiRoutes.Identity.UserGetByIdAsync)]
    Task<GetUserWithRoleResponseDTO> UserGetByIdAsync([Path] string id);

    [Post(ApiRoutes.Identity.DeleteRoleAsync)]
    Task<GeneralResponse> DeleteRoleAsync([Body] UpdateDeleteRequestDTO model);

    [Get(ApiRoutes.Identity.UserGetByEmailAsync)]
    Task<GetUserWithRoleResponseDTO> UserGetByEmailAsync([Path] string email);

    [Get(ApiRoutes.Identity.RoleGetByIdAsync)]
    Task<GetRoleResponseDTO> RoleGetByIdAsync([Path] string id);
    
    [Get(ApiRoutes.Identity.CheckPasswordAsync)]
    Task<string> CheckPasswordAsync([Path] string email, [Path] string password);
}
