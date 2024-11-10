
using LowCostAvioFlights.Models;


namespace LowCostAvioFlights.Repositories
{
    public interface IFlightSearchParametersRepository
    {
        Task<FlightSearchParametersDto> GetFlightSearchParametersAsync(int id);
        Task<IEnumerable<FlightSearchParametersDto>> GetFlightSearchParametersAsync();
        Task SaveFlightSearchParametersAndResultAsync(FlightSearchParametersDto flightSearchParameters);
        Task UpdateFlightSearchParametersAsync(FlightSearchParametersDto flightSearchParameters);
        Task DeleteFlightSearchParametersAsync(int id);
        Task<FlightOffersResponse> GetFlightOffersResponseBySearchHashCodeAsync(string searchHashCode);
        Task<FlightOffersResponse> GetLastSerchedFlightOfferAsync(string last);
    }
}
