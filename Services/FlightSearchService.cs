using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Models;
using LowCostAvioFlights.Repositories;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<FlightOffersResponse> SaveSearchParametersAndResponseAsync(FlightSearchParametersDto parameters, HttpContent responseContent)
        {
            var stream = await responseContent.ReadAsStreamAsync();
            var responseBody = await new StreamReader(stream).ReadToEndAsync();
            var flightOffersResponse = JsonConvert.DeserializeObject<FlightOffersResponse>(responseBody);

            parameters.JsonResponseFlightSearchPayload = responseBody;
            await _repository.SaveFlightSearchParametersAndResultAsync(parameters).ConfigureAwait(false);

            return flightOffersResponse;
        }
        public async Task<FlightOffersResponse> GetFlightOffersResponseBySearchHashCodeServiceAsync(string searchHashCode)
        {
           return await _repository.GetFlightOffersResponseBySearchHashCodeAsync(searchHashCode);
        }

        public async Task<FlightOffersResponse> GetLastFlightOffersServiceAsync(string last)
        {
            return await _repository.GetLastSerchedFlightOfferAsync(last);
        }


    }
}
