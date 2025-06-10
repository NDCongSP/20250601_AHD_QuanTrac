using RestEase;
using Application.Extentions;
using Application.DTOs.Response;
using Application.DTOs.Request.Account;
using Application.DTOs.Response.Account;

namespace Application.Services.Authen;

public interface IAccount
{
    Task<GeneralResponse> CreateSuperAdminAsync();
    Task<GeneralResponse> CreateAccountAsync([Body] CreateAccountRequestDTO model);
    Task<LoginResponse> LoginAccountAsync([Body] LoginRequestDTO model);
    Task<LoginResponse> RefreshTokenAsync([Body] RefreshTokenRequestDTO model);
    Task<GeneralResponse> CreateRoleAsync([Body] CreateRoleRequestDTO model);
    Task<List<GetRoleResponseDTO>> GetRolesAsync();
    Task<List<GetUserWithRoleResponseDTO>> GetUsersWithRolesAsync();
    Task<GeneralResponse> ChangeUserRoleAsync([Body] AssignUserRoleRequestDTO model);
    Task<GeneralResponse> ChangePassAsync([Body] ChangePassRequestDTO model);
    Task<GeneralResponse> AssignUserRoleAsync([Body] AssignUserRoleRequestDTO model);
    Task<GeneralResponse> DeleteUserAsync([Body] UpdateDeleteRequestDTO model);

    Task<GeneralResponse> DeleteUserRoleAsync([Body] AssignUserRoleRequestDTO model);
    Task<GeneralResponse> UpdateRoleAsync([Body] UpdateDeleteRequestDTO model);
    Task<GeneralResponse> UpdateRoleDTOAsync([Body] CreateRoleRequestDTO model);

    Task<GeneralResponse> UpdateUserInfoAsync([Body]UpdateUserInfoRequestDTO model);
    Task<GetUserWithRoleResponseDTO> UserGetById([Path] string id);
    Task<GeneralResponse> DeleteRoleAsync([Body] UpdateDeleteRequestDTO model);
    Task<GetUserWithRoleResponseDTO> UserGetByEmailAsync([Path] string email);

    [Get(ApiRoutes.Identity.RoleGetById)]
    Task<GetRoleResponseDTO> RoleGetById([Path] string id);
}
