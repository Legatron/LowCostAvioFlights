using AutoMapper;
using LowCostAvioFlights.Data;
using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LowCostAvioFlights.Repositories
{
    public class FlightSearchParametersRepository : IFlightSearchParametersRepository
    {
        private readonly LowCostAvioFlightsDbContext _context;
        private readonly IMapper _mapper;

        public FlightSearchParametersRepository(ILowCostAvioFlightsDbContext context, IMapper mapper)
        {
            _context = (LowCostAvioFlightsDbContext)context;
            _mapper = mapper;
        }

        public async Task<FlightSearchParametersDto> GetFlightSearchParametersAsync(int id)
        {
            var flightSearchParameters = await _context.FlightSearchParameters.FindAsync(id);
            return flightSearchParameters == null ? null : _mapper.Map<FlightSearchParametersDto>(flightSearchParameters);
        }

        public async Task<IEnumerable<FlightSearchParametersDto>> GetFlightSearchParametersAsync()
        {
            var flightSearchParameters = await _context.FlightSearchParameters.ToListAsync();
            return flightSearchParameters
                .Select(flightSearchParameter => _mapper.Map<FlightSearchParametersDto>(flightSearchParameter));
        }

        public async Task CreateFlightSearchParametersAsync(FlightSearchParametersDto flightSearchParametersDto)
        {
            var flightSearchParameters = _mapper.Map<FlightSearchParameters>(flightSearchParametersDto);
            _context.FlightSearchParameters.Add(flightSearchParameters);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlightSearchParametersAsync(FlightSearchParametersDto flightSearchParametersDto)
        {
            var flightSearchParameters = _mapper.Map<FlightSearchParameters>(flightSearchParametersDto);
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
