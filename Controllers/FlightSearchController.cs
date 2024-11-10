using LowCostAvioFlights.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using LowCostAvioFlights.Repositories;
using Microsoft.Extensions.Options;
using LowCostAvioFlights.Models;
using LowCostAvioFlights.Services;
using LowCostAvioFlights.Validators;


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
        private readonly IHashService _hashService;
        public FlightSearchController(ILogger<FlightSearchController> logger, IAmadeusOAuthClient oauthClient,
            IFlightSearchParametersRepository repository, IOptions<AmadeusApiSettings> amadeusApiSettings,
            IFlightSearchService flightSearchService, IAmadeusApiClientService amadeusApiClientService, IHashService hashService)
        {
            _logger = logger;
            _oauthClient = oauthClient;
            _repository = repository;
            _apiSettings = amadeusApiSettings;
            _flightSearchService = flightSearchService;
            _amadeusApiClientService = amadeusApiClientService;
            _hashService = hashService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchFlights([FromBody] FlightSearchParametersDto parameters)
        {
            var validator = new FlightSearchParametersDtoValidator();
            var validationResult = validator.Validate(parameters);

            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors.FirstOrDefault(e => e.PropertyName == "DepartureDate")?.ErrorMessage;
                if (errorMessage == null || errorMessage != null)
                {
                    errorMessage = "Please enter a departure date that is on or after today. e.g. " + DateTime.Today.Date.ToString("yyyy-MM-dd");
                }
                return BadRequest(new { error = errorMessage });
            }

            //prvo provjeri da li je već dohvaćeno
            var hash = _hashService.CreateHash(parameters);
            var cachedResult = await _flightSearchService.GetFlightOffersResponseBySearchHashCodeServiceAsync(hash);
            if (cachedResult != null)
            {
                return Ok(cachedResult);
            }

            parameters.SearchHashCode = hash;

            try
            {
                var accessToken = await _oauthClient.GetAccessTokenAsync();

                var amadeusResponse = await _amadeusApiClientService.GetFlightsAsync(accessToken, parameters);
                amadeusResponse.EnsureSuccessStatusCode();

                var flightOffersResponse = await _flightSearchService.SaveSearchParametersAndResponseAsync(parameters, amadeusResponse.Content);

                return Ok(flightOffersResponse);
               

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet(Name = "GetSerchedFlights")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cachedResult = await _flightSearchService.GetLastFlightOffersServiceAsync("");
                return Ok(cachedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, ex.Message);
            }

        }

    }

}
