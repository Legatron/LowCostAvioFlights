using LowCostAvioFlights.Domain;

using Microsoft.EntityFrameworkCore;


namespace LowCostAvioFlights.Data
{
    public class LowCostAvioFlightsDbContext : DbContext, ILowCostAvioFlightsDbContext
    {
        public LowCostAvioFlightsDbContext(DbContextOptions<LowCostAvioFlightsDbContext> options)
        : base(options)
        {
        }

        public DbSet<FlightSearchParameters> FlightSearchParameters { get; set; }
    }
}
