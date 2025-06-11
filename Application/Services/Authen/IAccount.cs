using RestEase;
using Application.Extentions;
using Application.DTOs.Response;
using Application.DTOs.Request.Account;
using Application.DTOs.Response.Account;

namespace Application.Services.Authen;

public interface IAccount
{
    [Post(ApiRoutes.Identity.CreateSuperAdminAccount)]
    Task<GeneralResponse> CreateSuperAdminAsync();

    [Post(ApiRoutes.Identity.CreateAccount)]
    Task<GeneralResponse> CreateAccountAsync([Body] CreateAccountRequestDTO model);

    [Post(ApiRoutes.Identity.Login)]
    Task<LoginResponse> LoginAccountAsync([Body] LoginRequestDTO model);

    [Post(ApiRoutes.Identity.RefreshToken)]
    Task<LoginResponse> RefreshTokenAsync([Body] RefreshTokenRequestDTO model);

    [Post(ApiRoutes.Identity.CreateRole)]
    Task<GeneralResponse> CreateRoleAsync([Body] CreateRoleRequestDTO model);

    [Get(ApiRoutes.Identity.RoleList)]
    Task<List<GetRoleResponseDTO>> GetRolesAsync();

    [Get(ApiRoutes.Identity.UserWithRole)]
    Task<List<GetUserWithRoleResponseDTO>> GetUsersWithRolesAsync();
    
    [Post(ApiRoutes.Identity.ChangeUserRole)]
    Task<GeneralResponse> ChangeUserRoleAsync([Body] AssignUserRoleRequestDTO model);

    [Post(ApiRoutes.Identity.ChangePassword)]
    Task<GeneralResponse> ChangePassAsync([Body] ChangePassRequestDTO model);

    [Post(ApiRoutes.Identity.AssignUserRole)]
    Task<GeneralResponse> AssignUserRoleAsync([Body] AssignUserRoleRequestDTO model);

    [Post(ApiRoutes.Identity.DeleteUser)]
    Task<GeneralResponse> DeleteUserAsync([Body] UpdateDeleteRequestDTO model);

    [Post(ApiRoutes.Identity.DeleteUserRole)]
    Task<GeneralResponse> DeleteUserRoleAsync([Body] AssignUserRoleRequestDTO model);

    [Post(ApiRoutes.Identity.UpdateRole)]
    Task<GeneralResponse> UpdateRoleAsync([Body] UpdateDeleteRequestDTO model);

    [Post(ApiRoutes.Identity.UpdateRoleDTO)]
    Task<GeneralResponse> UpdateRoleDTOAsync([Body] CreateRoleRequestDTO model);

    [Post(ApiRoutes.Identity.UpdateUserInfo)]
    Task<GeneralResponse> UpdateUserInfoAsync([Body]UpdateUserInfoRequestDTO model);

    [Get(ApiRoutes.Identity.UserGetById)]
    Task<GetUserWithRoleResponseDTO> UserGetById([Path] string id);

    [Post(ApiRoutes.Identity.DeleteRole)]
    Task<GeneralResponse> DeleteRoleAsync([Body] UpdateDeleteRequestDTO model);

    [Get(ApiRoutes.Identity.UserGetByEmail)]
    Task<GetUserWithRoleResponseDTO> UserGetByEmailAsync([Path] string email);

    [Get(ApiRoutes.Identity.RoleGetById)]
    Task<GetRoleResponseDTO> RoleGetById([Path] string id);
    
    [Get(ApiRoutes.Identity.CheckPasswordAsync)]
    Task<string> CheckPasswordAsync([Path] string email, [Path] string password);
}
