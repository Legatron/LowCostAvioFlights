using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Models;


namespace LowCostAvioFlights.Repositories
{
    public interface IFlightSearchParametersRepository
    {
        Task<FlightSearchParametersDto> GetFlightSearchParametersAsync(int id);
        Task<IEnumerable<FlightSearchParametersDto>> GetFlightSearchParametersAsync();
        Task CreateFlightSearchParametersAsync(FlightSearchParametersDto flightSearchParameters);
        Task UpdateFlightSearchParametersAsync(FlightSearchParametersDto flightSearchParameters);
        Task DeleteFlightSearchParametersAsync(int id);
    }
}
