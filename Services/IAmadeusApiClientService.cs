using LowCostAvioFlights.Models;

namespace LowCostAvioFlights.Services
{
    public interface IAmadeusApiClientService
    {
        Task<HttpResponseMessage> GetFlightsAsync(string accessToken, FlightSearchParametersDto parameters);
    }
}
