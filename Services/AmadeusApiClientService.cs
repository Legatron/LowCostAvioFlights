
using LowCostAvioFlights.Infrastructure;
using LowCostAvioFlights.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;
using System.Text;


namespace LowCostAvioFlights.Services
{
    public class AmadeusApiClientService: IAmadeusApiClientService
    {
        private readonly IOptions<AmadeusApiSettings> _apiSettings;
        private readonly IHttpClientFactory _httpClientFactory;
        public AmadeusApiClientService(IOptions<AmadeusApiSettings> apiSettings, IHttpClientFactory httpClientFactory)
        {
            _apiSettings = apiSettings;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<HttpResponseMessage> GetFlightsAsync(string accessToken, FlightSearchParametersDto parameters)
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
            var queryString = new StringBuilder();

            queryString.Append($"originLocationCode={parameters.OriginLocationCode}");
            queryString.Append($"&destinationLocationCode={parameters.DestinationLocationCode}");
            queryString.Append($"&departureDate={parameters.DepartureDate}");

            if (!parameters.ReturnDate.IsNullOrEmpty())
            {
                queryString.Append($"&returnDate={parameters.ReturnDate}");
            }

            queryString.Append($"&adults={parameters.Adults}");
            queryString.Append($"&children={parameters.Children}");
            queryString.Append($"&infants={parameters.Infants}");
            
            return queryString.ToString();
        }
    }

    
}
