using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Models;
using LowCostAvioFlights.Repositories;
using Newtonsoft.Json;
using static LowCostAvioFlights.Controllers.FlightSearchController;

namespace LowCostAvioFlights.Services
{
    public class FlightSearchService: IFlightSearchService
    {
        private readonly IFlightSearchParametersRepository _repository;
        public FlightSearchService(IFlightSearchParametersRepository repository)
        {
            _repository = repository;
        }
        public async Task<FlightOffersResponse> SaveSearchParametersAndResponseAsync(FlightSearchParametersDto parameters, string responseBody)
        {
            parameters.JsonResponseFlightSearchPayload = responseBody;
            await _repository.CreateFlightSearchParametersAsync(parameters);

            return JsonConvert.DeserializeObject<FlightOffersResponse>(responseBody);
        }
    }
}
