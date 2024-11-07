using LowCostAvioFlights.Data;
using LowCostAvioFlights.Domain;
using Microsoft.EntityFrameworkCore;

namespace LowCostAvioFlights.Repositories
{
    public class FlightSearchParametersRepository : IFlightSearchParametersRepository
    {
        private readonly LowCostAvioFlightsDbContext _context;

        public FlightSearchParametersRepository(ILowCostAvioFlightsDbContext context)
        {
            _context = (LowCostAvioFlightsDbContext)context;
        }

        public async Task<FlightSearchParameters> GetFlightSearchParametersAsync(int id)
        {
            return await _context.FlightSearchParameters.FindAsync(id);
        }

        public async Task<IEnumerable<FlightSearchParameters>> GetFlightSearchParametersAsync()
        {
            return await _context.FlightSearchParameters.ToListAsync();
        }

        public async Task CreateFlightSearchParametersAsync(FlightSearchParameters flightSearchParameters)
        {
            _context.FlightSearchParameters.Add(flightSearchParameters);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlightSearchParametersAsync(FlightSearchParameters flightSearchParameters)
        {
            _context.FlightSearchParameters.Update(flightSearchParameters);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlightSearchParametersAsync(int id)
        {
            var flightSearchParameters = await _context.FlightSearchParameters.FindAsync(id);
            if (flightSearchParameters != null)
            {
                _context.FlightSearchParameters.Remove(flightSearchParameters);
                await _context.SaveChangesAsync();
            }
        }
    }
}
