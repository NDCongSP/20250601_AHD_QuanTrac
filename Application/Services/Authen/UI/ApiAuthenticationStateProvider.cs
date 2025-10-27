using Application.Extentions;
using Blazored.LocalStorage;
using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Application.Services.Authen.UI;

public class ApiAuthenticationStateProvider : AuthenticationStateProvider, IAccessTokenProvider
{
    //kiểm soát truy cập đồng thời vào tài nguyên trong các tình huống cần giới hạn số lượng tác vụ hoặc luồng có thể chạy cùng lúc,
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    readonly HttpClient _httpClient;
    readonly ILocalStorageService _localStorage;
    private readonly ISessionStorageService _sessionStorage;

    #region Public methods
    public ApiAuthenticationStateProvider(HttpClient httpClient, ILocalStorageService localStorage, ISessionStorageService sessionStorage = null)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _sessionStorage = sessionStorage;
    }

    private bool IsTokenExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        return jwtToken.ValidTo < DateTime.UtcNow;
    }

    /// <summary>
    /// Get state and claim.
    /// </summary>
    /// <returns></returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Kiểm tra cả localStorage và sessionStorage để lấy mã thông báo
        //var cachedToken = await _localStorage.GetItemAsync<string>(ConstantExtention.StorageConst.AuthToken)
        //            ?? await _sessionStorage.GetItemAsync<string>(ConstantExtention.StorageConst.AuthToken);

        var cachedToken = await GetCachedAuthTokenAsync();

        if (string.IsNullOrWhiteSpace(cachedToken))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Kiểm tra token có hết hạn không
        if (IsTokenExpired(cachedToken))
        {
            // Xóa token hết hạn
            await ClearCacheAsync();
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Giải mã mã thông báo và thiết lập danh tính của người dùng
        //identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

        // Generate claimsIdentity from cached token
        var claimsIdentity = new ClaimsIdentity(JwtHelper.GetClaimsFromJwt(cachedToken), "jwt");

        var user = new ClaimsPrincipal(claimsIdentity);

        return new AuthenticationState(user);
    }

    public void MarkUserAsAuthenticated()
    {
        var authState = GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(authState);

        //// Lưu mã thông báo vào nơi lưu trữ thích hợp
        //if (rememberMe)
        //{
        //    _localStorage.SetItemAsync(AuthTokenKey, token);
        //}
        //else
        //{
        //    _sessionStorage.SetItemAsync(AuthTokenKey, token);
        //}
    }

    public void MarkUserAsLoggedOut()
    {
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        var authState = Task.FromResult(new AuthenticationState(anonymousUser));
        NotifyAuthenticationStateChanged(authState);
    }
    public async ValueTask CacheAuthTokensAsync(string token, string refreshToken, string sessionId)
    {
        await _localStorage.SetItemAsync(ConstantExtention.StorageConst.AuthToken, token);
        await _localStorage.SetItemAsync(ConstantExtention.StorageConst.RefreshToken, refreshToken);
    }

    public async ValueTask CachePermissionsAsync(ICollection<string> permissions) =>
       await _localStorage.SetItemAsync(ConstantExtention.StorageConst.RemeberMe, permissions);

    public async Task ClearCacheAsync()
    {
        await _localStorage.RemoveItemAsync(ConstantExtention.StorageConst.AuthToken);
        await _localStorage.RemoveItemAsync(ConstantExtention.StorageConst.RefreshToken);
        await _localStorage.RemoveItemAsync(ConstantExtention.StorageConst.RemeberMe);
    }

    public async ValueTask<AccessTokenResult> RequestAccessToken()
    {
        // We make sure the access token is only refreshed by one thread at a time. The other ones have to wait.
        await _semaphore.WaitAsync();
        try
        {
            string token = await GetCachedAuthTokenAsync();
            string refreshToken = await GetCachedRefreshTokenAsync();

            // Nếu không có token và không có refresh token → chế độ không đăng nhập
            if (string.IsNullOrWhiteSpace(token) && string.IsNullOrWhiteSpace(refreshToken))
            {
                // Return Success với null token để chạy mode không đăng nhập
                return new AccessTokenResult(AccessTokenResultStatus.Success, new AccessToken() { Value = null }, string.Empty);
            }

            // Nếu có token, kiểm tra expiration
            if (!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    if (IsTokenExpired(token))
                    {
                        // Token hết hạn
                        // Nếu có refresh token, xóa token cũ và return null để trigger refresh
                        if (!string.IsNullOrWhiteSpace(refreshToken))
                        {
                            // Xóa token hết hạn, giữ refresh token để RetryRefreshTokenHandler xử lý
                            await _localStorage.RemoveItemAsync(ConstantExtention.StorageConst.AuthToken);
                            // Return null token để API trả 401 và trigger refresh
                            return new AccessTokenResult(AccessTokenResultStatus.Success, new AccessToken() { Value = null }, string.Empty);
                        }
                        else
                        {
                            // Không có refresh token, xóa hết và chạy mode không đăng nhập
                            await ClearCacheAsync();
                            MarkUserAsLoggedOut();
                            return new AccessTokenResult(AccessTokenResultStatus.Success, new AccessToken() { Value = null }, string.Empty);
                        }
                    }

                    // Token còn hạn, return token
                    return new AccessTokenResult(AccessTokenResultStatus.Success, new AccessToken() { Value = token }, string.Empty);
                }
                catch (Exception)
                {
                    // Token không hợp lệ, xóa và xử lý như token hết hạn
                    await ClearCacheAsync();
                    MarkUserAsLoggedOut();
                    return new AccessTokenResult(AccessTokenResultStatus.Success, new AccessToken() { Value = null }, string.Empty);
                }
            }

            // Chỉ có refresh token mà không có token (trường hợp bất thường)
            // Xóa refresh token cũ và chạy mode không đăng nhập
            await ClearCacheAsync();
            MarkUserAsLoggedOut();
            return new AccessTokenResult(AccessTokenResultStatus.Success, new AccessToken() { Value = null }, string.Empty);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options) =>
       RequestAccessToken();


    #endregion

    #region Private Methods
    private async ValueTask<string> GetCachedAuthTokenAsync() =>
      await _localStorage.GetItemAsync<string>(ConstantExtention.StorageConst.AuthToken);

    private async ValueTask<string> GetCachedRefreshTokenAsync() =>
       await _localStorage.GetItemAsync<string>(ConstantExtention.StorageConst.RefreshToken);

    private async ValueTask<ICollection<string>> GetCachedPermissionsAsync() =>
      await _localStorage.GetItemAsync<ICollection<string>>(ConstantExtention.StorageConst.RemeberMe);

    public static DateTimeOffset GetExpiration(ClaimsPrincipal principal)
    {
        return DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(principal.FindFirst("exp")?.Value));
    }
    #endregion
}
