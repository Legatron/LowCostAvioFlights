using LowCostAvioFlights.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using LowCostAvioFlights.Repositories;
using Microsoft.Extensions.Options;
using LowCostAvioFlights.Models;
using LowCostAvioFlights.Services;


namespace LowCostAvioFlights.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightSearchController : ControllerBase
    {
        private readonly ILogger<FlightSearchController> _logger;
        private readonly IAmadeusOAuthClient _oauthClient;
        private readonly IFlightSearchParametersRepository _repository;
        private readonly IOptions<AmadeusApiSettings> _apiSettings;
        private readonly IFlightSearchService _flightSearchService;
        private readonly IAmadeusApiClientService _amadeusApiClientService;
        public FlightSearchController(ILogger<FlightSearchController> logger, IAmadeusOAuthClient oauthClient,
            IFlightSearchParametersRepository repository, IOptions<AmadeusApiSettings> amadeusApiSettings,
            IFlightSearchService flightSearchService, IAmadeusApiClientService amadeusApiClientService)
        {
            _logger = logger;
            _oauthClient = oauthClient;
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
                parameters.DepartureDate = "2024-11-09";
                parameters.ReturnDate = "2024-11-10";
                parameters.CurrencyCode = "EUR";

                var amadeusResponse = await _amadeusApiClientService.GetFlightsAsync(accessToken, parameters);
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
