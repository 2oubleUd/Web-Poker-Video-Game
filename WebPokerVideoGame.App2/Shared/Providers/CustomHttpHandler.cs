using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WebPokerVideoGame.App2.ViewModels.Accounts;



namespace WebPokerVideoGame.App2.Shared.Providers
{
    public class CustomHttpHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AuthenticationStateProvider _authStateProvider;

        public CustomHttpHandler(ILocalStorageService localStorageService, IHttpClientFactory httpClientFactory,
            AuthenticationStateProvider authStateProvider)
        {
            _localStorageService = localStorageService;
            _httpClientFactory = httpClientFactory;
            _authStateProvider = authStateProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.AbsolutePath.ToLower().Contains("login") ||
                request.RequestUri.AbsolutePath.ToLower().Contains("register") ||
                request.RequestUri.AbsolutePath.ToLower().Contains("renew-tokens"))
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var token = await _localStorageService.GetItemAsync<string>("jwt-access-token");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Add("Authorization", $"Bearer {token}");
            }

            var originalResponse = await base.SendAsync(request, cancellationToken);

            if(originalResponse.StatusCode == HttpStatusCode.Unauthorized) 
            {
                return await InvokeRefreshApiCall(originalResponse, request, token, cancellationToken);
            }

            return originalResponse;
        }

        private async Task<HttpResponseMessage> InvokeRefreshApiCall(HttpResponseMessage originalResponse, 
            HttpRequestMessage originalRequest, string jwtToken, CancellationToken cancellationToken)
        {
            var refreshToken = await _localStorageService.GetItemAsync<string>("refresh-token");

            var userClaims = Utility.ParseClaimsFromJwt(jwtToken);

            var renewTokenRequest = new RenewTokenRequestVm
            {
                RefreshToken = refreshToken,
                UserId = userClaims.ToList().Where(x => x.Type == "Sub")
                .Select(x => Convert.ToInt32(x.Value)).FirstOrDefault()
            };

            var jsonPayload = JsonSerializer.Serialize(renewTokenRequest);
            var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                
            var httpClient = _httpClientFactory.CreateClient("Dot7Api");
            var refreshTokenResponse = await httpClient.PostAsync("api/User/renew-tokens", requestContent);

            if(refreshTokenResponse.StatusCode == HttpStatusCode.OK)
            {
                var regeneratedToken = await refreshTokenResponse.Content.ReadFromJsonAsync<JwtTokenResponseVM>();
                await _localStorageService.SetItemAsync<string>("jwt-access-token", regeneratedToken.AccessToken);
                await _localStorageService.SetItemAsync<string>("refresh-token", regeneratedToken.RefreshToken);
                (_authStateProvider as CustomAuthProvider).NotifyAuthState();


                originalRequest.Headers.Remove("Authorization");
                originalRequest.Headers.Add("Authorization", $"Bearer {regeneratedToken.AccessToken}");
                return await base.SendAsync(originalRequest, cancellationToken);
            }

            return originalResponse;
        }
    }
}
