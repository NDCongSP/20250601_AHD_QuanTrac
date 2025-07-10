using Application.DTOs.Request.Account;
using Application.DTOs.Response;

namespace API.Controllers
{
    [Authorize]
    [Route($"api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        readonly IAccount _account;
        public AccountController(IAccount account)
        {
            _account = account;
        }

        //[Authorize(Policy ="Admin")]
        [HttpPost(ApiRoutes.Identity.CreateAccount)]
        public async Task<ActionResult<GeneralResponse>> CreateAccountAsync([Body] CreateAccountRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.CreateAccountAsync(model));
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<ActionResult<GeneralResponse>> LoginAccountAsync([Body] LoginRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");
            var result = await _account.LoginAccountAsync(model);
            //todo test
            //await _authStateProvider.CacheAuthTokensAsync(result.Token, result.RefreshToken, string.Empty);
            //((ApiAuthenticationStateProvider)_authStateProvider).MarkUserAsAuthenticated();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.RefreshToken)]
        public async Task<ActionResult<GeneralResponse>> RefreshTokenAsync([Body] RefreshTokenRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");
            var result = await _account.RefreshTokenAsync(model);
            //todo check
            //await _authStateProvider.CacheAuthTokensAsync(result.Token, result.RefreshToken, string.Empty);
            //((ApiAuthenticationStateProvider)_authStateProvider).MarkUserAsAuthenticated();
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Identity.CreateRole)]
        public async Task<ActionResult<GeneralResponse>> CreateRoleAsync([Body] CreateRoleRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.CreateRoleAsync(model));
        }

        [HttpGet(ApiRoutes.Identity.RoleList)]
        public async Task<ActionResult<IEnumerable<GetRoleResponseDTO>>> GetRoleAsync()
        {
            return Ok(await _account.GetRolesAsync());
        }

        [AllowAnonymous]
        [HttpPost(ApiRoutes.Identity.CreateSuperAdminAccount)]
        public async Task<ActionResult<GeneralResponse>> CreateSupperAdminAsync()
        {
            return Ok(await _account.CreateSuperAdminAsync());
        }

        [HttpGet(ApiRoutes.Identity.UserWithRole)]
        public async Task<ActionResult<List<GeneralResponse>>> GetUserWithRoleAsync()
        {
            return Ok(await _account.GetUsersWithRolesAsync());
        }

        [HttpPost(ApiRoutes.Identity.ChangePassword)]
        public async Task<ActionResult<GeneralResponse>> ChangePassAsync([Body]  ChangePassRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.ChangePassAsync(model));
        }

        [HttpPost(ApiRoutes.Identity.ChangeUserRole)]
        public async Task<ActionResult<GeneralResponse>> ChangeRoleAsync([Body] AssignUserRoleRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.ChangeUserRoleAsync(model));
        }

        [HttpPost(ApiRoutes.Identity.AssignUserRole)]
        public async Task<ActionResult<GeneralResponse>> AssignUserRoleAsync([Body] AssignUserRoleRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.AssignUserRoleAsync(model));
        }
        [HttpPost(ApiRoutes.Identity.Logout)]
        public async Task<ActionResult<bool>> LogoutAsync()
        {
            //await _authStateProvider.ClearCacheAsync();
            //((ApiAuthenticationStateProvider)_authStateProvider).MarkUserAsLoggedOut();
            return true;
            //todo navigate to login
            //_navigationManager.NavigateTo("/login");
        }

        [HttpPost(ApiRoutes.Identity.DeleteUser)]
        public async Task<ActionResult<GeneralResponse>> DeleteUserAsync([Body] UpdateDeleteRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.DeleteUserAsync(model));
        }

        [HttpPost(ApiRoutes.Identity.DeleteUserRole)]
        public async Task<ActionResult<GeneralResponse>> DeleteUserRoleAsync([Body] AssignUserRoleRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.DeleteUserRoleAsync(model));
        }

        [HttpPost(ApiRoutes.Identity.UpdateRole)]
        public async Task<ActionResult<GeneralResponse>> UpdateRoleAsync([Body] UpdateDeleteRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.UpdateRoleAsync(model));
        }

        [HttpPost(ApiRoutes.Identity.UpdateRoleDTO)]
        public async Task<ActionResult<GeneralResponse>> UpdateRoleDTOAsync([Body] CreateRoleRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.UpdateRoleDTOAsync(model));
        }

        [HttpPost(ApiRoutes.Identity.UpdateUserInfo)]
        public async Task<ActionResult<GeneralResponse>> UpdateUserInfoAsync([Body] UpdateUserInfoRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.UpdateUserInfoAsync(model));
        }

        [HttpGet(ApiRoutes.Identity.UserGetById)]
        public async Task<ActionResult<GeneralResponse>> UserGetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Model cannot be null");

            return Ok(await _account.UserGetById(id));
        }

        [HttpGet(ApiRoutes.Identity.UserGetByEmail)]
        public async Task<ActionResult<GeneralResponse>> UserGetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest("Model cannot be null");

            return Ok(await _account.UserGetByEmailAsync(email));
        }

        [HttpPost(ApiRoutes.Identity.DeleteRole)]
        public async Task<ActionResult<GeneralResponse>> DeleteRoleAsync([Body] UpdateDeleteRequestDTO model)
        {
            if (model == null) return BadRequest("Model cannot be null");

            return Ok(await _account.DeleteRoleAsync(model));
        }

        [HttpGet(ApiRoutes.Identity.RoleGetById)]
        public async Task<ActionResult<GeneralResponse>> RoleGetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id cannot be null");

            return Ok(await _account.RoleGetById(id));
        }

        [AllowAnonymous]
        [HttpGet(ApiRoutes.Identity.CheckPasswordAsync)]
        public async Task<ActionResult<string>> CheckPasswordAsync([Path] string email, [Path] string password)
        {
            return Ok(await _account.CheckPasswordAsync(email, password));
        }
    }
}
