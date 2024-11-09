using Microsoft.Extensions.Options;
using System.Text;
using LowCostAvioFlights.Services;

namespace LowCostAvioFlights.Infrastructure
{
    public class AmadeusOAuthClient
    {
        private readonly IOptions<AmadeusApiSettings> _apiSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenCacheService _tokenCacheService;

        public AmadeusOAuthClient(IOptions<AmadeusApiSettings> apiSettings, IHttpClientFactory httpClientFactory, 
             ITokenCacheService tokenCacheService)
        {
            _apiSettings = apiSettings;
            _httpClientFactory = httpClientFactory;
            _tokenCacheService = tokenCacheService;
        }
        public async Task<string> GetAccessTokenAsync()
        {
            var cachedToken = await _tokenCacheService.GetCachedTokenAsync();
            if (cachedToken != null)
            {
                return cachedToken;
            }

            var _httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _apiSettings.Value.OauthTokenHttps)
            {
                Content = new StringContent($"grant_type=client_credentials&client_id={_apiSettings.Value.ApiKey}&client_secret={_apiSettings.Value.ApiSecret}", Encoding.UTF8, "application/x-www-form-urlencoded")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            await using var stream = await response.Content.ReadAsStreamAsync();
            var oauthResults = await System.Text.Json.JsonSerializer.DeserializeAsync<TokenResponse>(stream);

            if (double.TryParse(_apiSettings.Value.CacheDuratrionForTokenMinuts, out double cacheDuration))
            {
                await _tokenCacheService.CacheTokenAsync(oauthResults.access_token, TimeSpan.FromMinutes(cacheDuration));
            }

            return oauthResults.access_token;
        }

    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string access_token { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
    }
}

