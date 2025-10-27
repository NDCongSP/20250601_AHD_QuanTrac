using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Net.Http.Headers;

namespace Application.Services.Authen.UI
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly IAccessTokenProviderAccessor _tokenProviderAccessor;
        private readonly NavigationManager _navigation;


        public AuthenticationHeaderHandler(IAccessTokenProviderAccessor tokenProviderAccessor, NavigationManager navigation)
        {
            _tokenProviderAccessor = tokenProviderAccessor;
            _navigation = navigation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var isLogin = request.RequestUri?.AbsolutePath.Contains("/login") ?? false;

                // skip token endpoints
                if (!isLogin)
                {
                    var accessTokenResult = await _tokenProviderAccessor.TokenProvider.RequestAccessToken();
                    if (accessTokenResult.Status == AccessTokenResultStatus.RequiresRedirect)
                    {
                        // Chỉ redirect khi RequiresRedirect (user chưa login và cần login)
                        _navigation.NavigateTo(accessTokenResult.RedirectUrl);
                    }
                    else if (accessTokenResult.Status == AccessTokenResultStatus.Success)
                    {
                        if (accessTokenResult.TryGetToken(out AccessToken accessToken) && !string.IsNullOrWhiteSpace(accessToken.Value))
                        {
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Value);
                        }
                        // Không redirect nếu không có token, API sẽ trả 401 và RetryRefreshTokenHandler xử lý
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            var response = await base.SendAsync(request, cancellationToken);

            // Không redirect ở đây, để RetryRefreshTokenHandler xử lý 401
            // Nếu refresh token thất bại, AuthorizeView sẽ tự động redirect khi cần
            
            return response;
        }
    }
}
