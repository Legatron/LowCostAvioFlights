using LowCostAvioFlights.Domain;

using Microsoft.EntityFrameworkCore;


namespace LowCostAvioFlights.Data
{
    public interface ILowCostAvioFlightsDbContext
    {
        DbSet<FlightSearchParameters> FlightSearchParameters { get; set; }
    }
}
