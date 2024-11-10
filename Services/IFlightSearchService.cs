using LowCostAvioFlights.Models;

namespace LowCostAvioFlights.Services
{
    public interface IFlightSearchService
    {
        Task<FlightOffersResponse> SaveSearchParametersAndResponseAsync(FlightSearchParametersDto parameters, HttpContent responseContent);
        Task<FlightOffersResponse> GetFlightOffersResponseBySearchHashCodeServiceAsync(string searchHashCode);
        Task<FlightOffersResponse> GetLastFlightOffersServiceAsync(string last);
    }
}
