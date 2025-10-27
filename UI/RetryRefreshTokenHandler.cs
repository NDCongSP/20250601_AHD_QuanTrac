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
                .RetryAsync(1, async (outcome, retryNumber, context) =>
                {
                    try
                    {
                        var _localStorage = provider.GetRequiredService<ILocalStorageService>();
                        var _authStateProvider = provider.GetRequiredService<ApiAuthenticationStateProvider>();
                        var authService = provider.GetRequiredService<IAccount>();
                        var token = await _localStorage.GetItemAsync<string>(ConstantExtention.StorageConst.AuthToken);
                        var refreshToken = await _localStorage.GetItemAsync<string>(ConstantExtention.StorageConst.RefreshToken);

                        // Nếu không có refresh token → không thể refresh, xóa cache nhưng KHÔNG throw exception
                        if (string.IsNullOrEmpty(refreshToken))
                        {
                            // Xóa cache để đảm bảo clean state
                            await _authStateProvider.ClearCacheAsync();
                            _authStateProvider.MarkUserAsLoggedOut();
                            
                            // KHÔNG throw exception, để request fail với 401 và component xử lý
                            // Điều này cho phép trang public hoạt động bình thường
                            Console.WriteLine("RefreshToken bị thiếu, không thể làm mới token. Chạy mode không đăng nhập.");
                            return; // Exit gracefully
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
                            // Xóa cache nhưng KHÔNG throw exception
                            await _authStateProvider.ClearCacheAsync();
                            _authStateProvider.MarkUserAsLoggedOut();
                            Console.WriteLine("Không refresh được token mới. Refresh token có thể đã hết hạn.");
                            return; // Exit gracefully
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
                    catch (Exception ex)
                    {
                        // Log lỗi nhưng KHÔNG throw để tránh unhandled exception
                        Console.WriteLine($"Lỗi khi refresh token: {ex.Message}");
                        // Request sẽ fail với 401, component tự xử lý
                    }
                });
        }
    }
}
