using LowCostAvioFlights.Models;

namespace LowCostAvioFlights.Services
{
    public interface IHashService
    {
       string CreateHash(FlightSearchParametersDto parameters);
    }
}
