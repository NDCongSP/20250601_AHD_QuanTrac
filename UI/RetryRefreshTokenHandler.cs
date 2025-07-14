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
                    var navigation = provider.GetRequiredService<NavigationManager>();
                    try
                    {
                        var _localStorage = provider.GetRequiredService<ILocalStorageService>();
                        var _authStateProvider = provider.GetRequiredService<ApiAuthenticationStateProvider>();
                        var authService = provider.GetRequiredService<IAccount>();
                        var token = await _localStorage.GetItemAsync<string>(ConstantExtention.StorageConst.AuthToken);
                        var refreshToken = await _localStorage.GetItemAsync<string>(ConstantExtention.StorageConst.RefreshToken);

                        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshToken))
                        {
                            navigation.NavigateTo("/login");
                            throw new Exception("Token hoặc RefreshToken bị thiếu.");
                        }

                        var model = new RefreshTokenRequestDTO()
                        {
                            Token = token,
                            RefreshToken = refreshToken,
                        };

                        var loginResponse = await authService.RefreshTokenAsync(model);

                        if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
                        {
                            navigation.NavigateTo("/login");
                            throw new Exception("Không refresh được token mới.");
                        }

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
                        // Redirect về trang login khi có lỗi
                        navigation.NavigateTo("/login");
                    }
                });
        }
    }
}
