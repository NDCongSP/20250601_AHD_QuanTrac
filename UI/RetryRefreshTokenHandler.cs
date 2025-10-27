using Application.DTOs.Request.Account;
using Application.Services.Authen;
using Application.Services.Authen.UI;
using Blazored.LocalStorage;
using Polly;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;

namespace UI
{
    public static class RetryRefreshTokenHandler
    {
        public static IAsyncPolicy<HttpResponseMessage> GetTokenRefresher(
            IServiceProvider provider,
            HttpRequestMessage request)
        {
            return Policy<HttpResponseMessage>
                .HandleResult(response => response.StatusCode == HttpStatusCode.Unauthorized)
                .RetryAsync(async (_, __) =>
                {
                    try
                    {
                        var _localStorage = provider.GetRequiredService<ILocalStorageService>();
                        var _authStateProvider = provider.GetRequiredService<ApiAuthenticationStateProvider>();
                        var authService = provider.GetRequiredService<IAccount>();
                        var token = await _localStorage.GetItemAsync<string>(ConstantExtention.StorageConst.AuthToken);
                        var refreshToken = await _localStorage.GetItemAsync<string>(ConstantExtention.StorageConst.RefreshToken);

                        // Nếu không có refresh token → không thể refresh, xóa cache và fail
                        if (string.IsNullOrEmpty(refreshToken))
                        {
                            // Xóa cache và throw exception để request fail
                            // Component cần authentication sẽ tự redirect nếu cần
                            await _authStateProvider.ClearCacheAsync();
                            _authStateProvider.MarkUserAsLoggedOut();
                            throw new Exception("RefreshToken bị thiếu, không thể làm mới token.");
                        }

                        // Token có thể null (đã bị xóa do hết hạn), nhưng vẫn có refresh token
                        // Thử refresh với token null hoặc token cũ
                        var model = new RefreshTokenRequestDTO()
                        {
                            Token = token ?? string.Empty,
                            RefreshToken = refreshToken,
                        };

                        var loginResponse = await authService.RefreshTokenAsync(model);

                        if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
                        {
                            // Refresh thất bại (có thể do refresh token cũng hết hạn)
                            // Xóa cache và throw exception
                            await _authStateProvider.ClearCacheAsync();
                            _authStateProvider.MarkUserAsLoggedOut();
                            throw new Exception("Không refresh được token mới. Refresh token có thể đã hết hạn.");
                        }

                        // Refresh thành công
                        //Set local storage
                        await _authStateProvider.CacheAuthTokensAsync(loginResponse.Token, loginResponse.RefreshToken, string.Empty);
                        _authStateProvider.MarkUserAsAuthenticated();
                        // Gán token mới vào header của httpClient
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse.Token);
                        await Task.Delay(10);
                        request.SetPolicyExecutionContext(new Context());
                    }
                    catch
                    {
                        // Không redirect, chỉ throw exception
                        // Các trang cần authentication sẽ tự động redirect qua AuthorizeView
                        throw;
                    }
                });
        }
    }
}
