using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using LowCostAvioFlights.Repositories;
using Microsoft.Extensions.Options;
using LowCostAvioFlights.Models;

namespace LowCostAvioFlights.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightSearchController : ControllerBase
    {
        private readonly ILogger<FlightSearchController> _logger;
        private readonly AmadeusOAuthClient _oauthClient;
        private readonly HttpClient _httpClient;
        private readonly IFlightSearchParametersRepository _repository;
        private readonly IOptions<AmadeusApiSettings> _apiSettings;

        public FlightSearchController(ILogger<FlightSearchController> logger, AmadeusOAuthClient oauthClient, HttpClient httpClient,
            IFlightSearchParametersRepository repository, IOptions<AmadeusApiSettings> amadeusApiSettings)
        {
            _logger = logger;
            _oauthClient = oauthClient;
            _httpClient = httpClient;
            _repository = repository;
            _apiSettings = amadeusApiSettings;
        }

        [HttpPost]
        public async Task<IActionResult> SearchFlights([FromBody] FlightSearchParametersDto parameters)
        {
            var accessToken = await _oauthClient.GetAccessTokenAsync();
            var amadeusResponse = await MakeAmadeusApiCallAsync(accessToken, parameters);

            var dbflightssearch = await _repository.GetFlightSearchParametersAsync();
            return StatusCode(StatusCodes.Status200OK);
        }

        private async Task<AmadeusResponse> MakeAmadeusApiCallAsync(string accessToken, FlightSearchParametersDto parameters)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentException("Access token is required", nameof(accessToken));
            }
            try
            {
                var baseUrl = _apiSettings.Value.BaseUrl;
                var endpoint = _apiSettings.Value.EndpointFlightOffer;

                _httpClient.BaseAddress = new Uri(baseUrl);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                parameters.OriginLocationCode = "RDU";
                parameters.DestinationLocationCode = "MUC";
                parameters.Adults = 1;
                parameters.DepartureDate = "2024-11-08";
                parameters.ReturnDate = "2024-11-10";
                var queryString = BuildQueryString(parameters);
                //queryString = "originLocationCode=RDU&destinationLocationCode=MUC&departureDate=2024-11-09&returnDate=2024-11-10&adults=1&currencyCode=EUR";

                var request = new HttpRequestMessage(HttpMethod.Get, $"{endpoint}?{queryString}");

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AmadeusResponse>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                // Handle the HTTP request exception
                _logger.LogError(ex.Message, ex);
                throw;
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        private string BuildQueryString(FlightSearchParametersDto parameters)
        {
            var queryString = $"originLocationCode={parameters.OriginLocationCode}&destinationLocationCode={parameters.DestinationLocationCode}&departureDate={parameters.DepartureDate}&returnDate={parameters.ReturnDate}&adults={parameters.Adults}&children={parameters.Children}&infants={parameters.Infants}";
            return queryString;
        }

        public class AmadeusResponse
        {
            public List<FlightOffer> FlightOffers { get; set; }
        }

        public class FlightOffer
        {
            public string Id { get; set; }
            public string DepartureAirport { get; set; }
            public string ArrivalAirport { get; set; }
            public DateTime DepartureDate { get; set; }
            public DateTime ArrivalDate { get; set; }
            public decimal Price { get; set; }
        }
    }

}
