using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using LowCostAvioFlights.Repositories;
using Microsoft.Extensions.Options;
using LowCostAvioFlights.Models;
using LowCostAvioFlights.Services;
using Azure;

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
        private readonly FlightSearchService _flightSearchService;
        private readonly AmadeusApiClientService _amadeusApiClientService;
        public FlightSearchController(ILogger<FlightSearchController> logger, AmadeusOAuthClient oauthClient, HttpClient httpClient,
            IFlightSearchParametersRepository repository, IOptions<AmadeusApiSettings> amadeusApiSettings, 
            FlightSearchService flightSearchService, AmadeusApiClientService amadeusApiClientService)
        {
            _logger = logger;
            _oauthClient = oauthClient;
            _httpClient = httpClient;
            _repository = repository;
            _apiSettings = amadeusApiSettings;
            _flightSearchService = flightSearchService;
            _amadeusApiClientService = amadeusApiClientService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchFlights([FromBody] FlightSearchParametersDto parameters)
        {
            try
            {
                var accessToken = await _oauthClient.GetAccessTokenAsync();
                parameters.OriginLocationCode = "RDU";
                parameters.DestinationLocationCode = "MUC";
                parameters.Adults = 1;
                parameters.DepartureDate = "2024-11-08";
                parameters.ReturnDate = "2024-11-10";
                parameters.CurrencyCode = "EUR";

                var amadeusResponse = await _amadeusApiClientService.MakeApiCallAsync(accessToken, parameters);
                amadeusResponse.EnsureSuccessStatusCode();

                var responseBody = await amadeusResponse.Content.ReadAsStringAsync();

                return Ok(await _flightSearchService.SaveSearchParametersAndResponseAsync(parameters, responseBody));
                //var dbflightssearch = await _repository.GetFlightSearchParametersAsync();
                //return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
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
            public string Data { get; set; }
        }
    }

}
