using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace LowCostAvioFlights.Infrastructure
{
    public class AmadeusOAuthClient
    {
        private readonly IOptions<AmadeusApiSettings> _apiSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public AmadeusOAuthClient(IOptions<AmadeusApiSettings> apiSettings, IHttpClientFactory httpClientFactory)
        {
            _apiSettings = apiSettings;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> GetAccessTokenAsync()
        {
            var _httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _apiSettings.Value.OauthTokenHttps)
            {
                Content = new StringContent($"grant_type=client_credentials&client_id={_apiSettings.Value.ApiKey}&client_secret={_apiSettings.Value.ApiSecret}", Encoding.UTF8, "application/x-www-form-urlencoded")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            await using var stream = await response.Content.ReadAsStreamAsync();
            var oauthResults = await System.Text.Json.JsonSerializer.DeserializeAsync<TokenResponse>(stream);

            var x = oauthResults.access_token;
            var responseBody = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseBody);

            return tokenResponse.access_token;
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

