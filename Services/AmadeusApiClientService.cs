
using LowCostAvioFlights.Infrastructure;
using LowCostAvioFlights.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;


namespace LowCostAvioFlights.Services
{
    public class AmadeusApiClientService
    {
        private readonly IOptions<AmadeusApiSettings> _apiSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        public AmadeusApiClientService(IOptions<AmadeusApiSettings> apiSettings, IHttpClientFactory httpClientFactory)
        {
            _apiSettings = apiSettings;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HttpResponseMessage> MakeApiCallAsync(string accessToken, FlightSearchParametersDto parameters)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access token is required", nameof(accessToken));
            }
            var baseUrl = _apiSettings.Value.BaseUrl;
            var endpoint = _apiSettings.Value.EndpointFlightOffer;

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var queryString = BuildQueryString(parameters);
            var request = new HttpRequestMessage(HttpMethod.Get, $"{endpoint}?{queryString}");

            return await httpClient.SendAsync(request);
        }
        private string BuildQueryString(FlightSearchParametersDto parameters)
        {
            var queryString = $"originLocationCode={parameters.OriginLocationCode}&destinationLocationCode={parameters.DestinationLocationCode}&departureDate={parameters.DepartureDate}&returnDate={parameters.ReturnDate}&adults={parameters.Adults}&children={parameters.Children}&infants={parameters.Infants}";
            return queryString;
        }
    }

    
}
