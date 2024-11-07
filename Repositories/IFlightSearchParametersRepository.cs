using LowCostAvioFlights.Domain;

namespace LowCostAvioFlights.Repositories
{
    public interface IFlightSearchParametersRepository
    {
        Task<FlightSearchParameters> GetFlightSearchParametersAsync(int id);
        Task<IEnumerable<FlightSearchParameters>> GetFlightSearchParametersAsync();
        Task CreateFlightSearchParametersAsync(FlightSearchParameters flightSearchParameters);
        Task UpdateFlightSearchParametersAsync(FlightSearchParameters flightSearchParameters);
        Task DeleteFlightSearchParametersAsync(int id);
    }
}
