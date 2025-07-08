using Application.DTOs.Request.Account;
using Application.DTOs.Response;
using Application.Services.Authen.UI;

namespace API.Controllers
{
    [Authorize]
    [Route($"api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase, IAccount
    {
        readonly Repository _repository;

        public AccountController(Repository repository)
        {
            _repository = repository;
        }

        [HttpPost(ApiRoutes.Identity.AssignUserRoleAsync)]
        public async Task<GeneralResponse> AssignUserRoleAsync([Body] AssignUserRoleRequestDTO model)
        {
           return await _repository.SAccount.AssignUserRoleAsync(model);
        }

        [HttpPost(ApiRoutes.Identity.ChangePassAsync)]
        public async Task<GeneralResponse> ChangePassAsync([Body] ChangePassRequestDTO model)
        {
            return await _repository.SAccount.ChangePassAsync(model);
        }

        [HttpPost(ApiRoutes.Identity.ChangeUserRoleAsync)]
        public async Task<GeneralResponse> ChangeUserRoleAsync([Body] AssignUserRoleRequestDTO model)
        {
            return await _repository.SAccount.ChangeUserRoleAsync(model);
        }

        [HttpGet(ApiRoutes.Identity.CheckPasswordAsync)]
        public async Task<string> CheckPasswordAsync([Path] string email, [Path] string password)
        {
            return await _repository.SAccount.CheckPasswordAsync(email, password);
        }

        [HttpPost(ApiRoutes.Identity.CreateAccountAsync)]
        public async Task<GeneralResponse> CreateAccountAsync([Body] CreateAccountRequestDTO model)
        {
            return await _repository.SAccount.CreateAccountAsync(model);
        }

        [HttpPost(ApiRoutes.Identity.CreateRoleAsync)]
        public async Task<GeneralResponse> CreateRoleAsync([Body] CreateRoleRequestDTO model)
        {
            return await _repository.SAccount.CreateRoleAsync(model);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.CreateSuperAdminAsync)]
        public async Task<GeneralResponse> CreateSuperAdminAsync()
        {
            return await _repository.SAccount.CreateSuperAdminAsync();
        }

        [HttpPost(ApiRoutes.Identity.DeleteRoleAsync)]
        public async Task<GeneralResponse> DeleteRoleAsync([Body] UpdateDeleteRequestDTO model)
        {
            return await _repository.SAccount.DeleteRoleAsync(model);
        }

        [HttpPost(ApiRoutes.Identity.DeleteUserAsync)]
        public async Task<GeneralResponse> DeleteUserAsync([Body] UpdateDeleteRequestDTO model)
        {
            return await _repository.SAccount.DeleteUserAsync(model);
        }

        [HttpPost(ApiRoutes.Identity.DeleteUserRoleAsync)]
        public async Task<GeneralResponse> DeleteUserRoleAsync([Body] AssignUserRoleRequestDTO model)
        {
            return await _repository.SAccount.DeleteUserRoleAsync(model);
        }

        [HttpGet(ApiRoutes.Identity.GetRolesAsync)]
        public async Task<List<GetRoleResponseDTO>> GetRolesAsync()
        {
            return await _repository.SAccount.GetRolesAsync();
        }

        [HttpGet(ApiRoutes.Identity.GetUsersWithRolesAsync)]
        public async Task<List<GetUserWithRoleResponseDTO>> GetUsersWithRolesAsync()
        {
            return await _repository.SAccount.GetUsersWithRolesAsync();
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.LoginAccountAsync)]
        public async Task<Result<LoginResponse>> LoginAccountAsync([Body] LoginRequestDTO model)
        {
            return await _repository.SAccount.LoginAccountAsync(model);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.RefreshTokenAsync)]
        public async Task<Result<LoginResponse>> RefreshTokenAsync([Body] RefreshTokenRequestDTO model)
        {
            return await _repository.SAccount.RefreshTokenAsync(model);
        }

        [HttpGet(ApiRoutes.Identity.RoleGetByIdAsync)]
        public async Task<GetRoleResponseDTO> RoleGetByIdAsync([Path] string id)
        {
            return await _repository.SAccount.RoleGetByIdAsync(id);
        }

        [HttpPost(ApiRoutes.Identity.UpdateRoleAsync)]
        public async Task<GeneralResponse> UpdateRoleAsync([Body] UpdateDeleteRequestDTO model)
        {
            return await _repository.SAccount.UpdateRoleAsync(model);
        }

        [HttpPost(ApiRoutes.Identity.UpdateRoleDTOAsync)]
        public async Task<GeneralResponse> UpdateRoleDTOAsync([Body] CreateRoleRequestDTO model)
        {
            return await _repository.SAccount.UpdateRoleDTOAsync(model);
        }

        [HttpPost(ApiRoutes.Identity.UpdateUserInfoAsync)]
        public async Task<GeneralResponse> UpdateUserInfoAsync([Body] UpdateUserInfoRequestDTO model)
        {
            return await _repository.SAccount.UpdateUserInfoAsync(model);
        }

        [HttpGet(ApiRoutes.Identity.UserGetByEmailAsync)]
        public async Task<GetUserWithRoleResponseDTO> UserGetByEmailAsync([Path] string email)
        {
         return await _repository.SAccount.UserGetByEmailAsync(email);
        }

        [HttpGet(ApiRoutes.Identity.UserGetByIdAsync)]
        public async Task<GetUserWithRoleResponseDTO> UserGetByIdAsync([Path] string id)
        {
            return await _repository.SAccount.UserGetByIdAsync(id);
        }
    }
}
