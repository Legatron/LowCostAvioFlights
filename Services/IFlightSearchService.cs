using LowCostAvioFlights.Models;
using static LowCostAvioFlights.Controllers.FlightSearchController;

namespace LowCostAvioFlights.Services
{
    public interface IFlightSearchService
    {
        Task<FlightOffersResponse> SaveSearchParametersAndResponseAsync(FlightSearchParametersDto parameters, string responseBody);
    }
}
