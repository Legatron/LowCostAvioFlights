using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;

namespace LowCostAvioFlights.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightSearchController : ControllerBase
    {
        private readonly ILogger<FlightSearchController> _logger;
        private readonly AmadeusOAuthClient _oauthClient;
        private readonly HttpClient _httpClient;

        public FlightSearchController(ILogger<FlightSearchController> logger, AmadeusOAuthClient oauthClient, HttpClient httpClient)
        {
            _logger = logger;
            _oauthClient = oauthClient;
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> SearchFlights([FromBody] FlightSearchParameters parameters)
        {
            var accessToken = await _oauthClient.GetAccessTokenAsync();
            var amadeusResponse = await MakeAmadeusApiCallAsync(accessToken, parameters);
            // Use the access token to make the API call to Amadeus
            // ...

            return StatusCode(StatusCodes.Status200OK);
        }

        private async Task<AmadeusResponse> MakeAmadeusApiCallAsync(string accessToken, FlightSearchParameters parameters)
        {
            // Make the API call to Amadeus using the access token and search parameters
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.amadeus.com/v2/shopping/flight-offers")
            {
                Headers =
        {
            Authorization = new AuthenticationHeaderValue("Bearer", accessToken)
        }
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var amadeusResponse = JsonConvert.DeserializeObject<AmadeusResponse>(responseBody);

            return amadeusResponse;
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
