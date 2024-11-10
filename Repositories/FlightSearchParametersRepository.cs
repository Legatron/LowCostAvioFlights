using AutoMapper;
using LowCostAvioFlights.Controllers;
using LowCostAvioFlights.Data;
using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LowCostAvioFlights.Repositories
{
    public class FlightSearchParametersRepository : IFlightSearchParametersRepository
    {
        private readonly LowCostAvioFlightsDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FlightSearchParametersRepository> _logger;

        public FlightSearchParametersRepository(ILowCostAvioFlightsDbContext context, IMapper mapper, ILogger<FlightSearchParametersRepository> logger)
        {
            _context = (LowCostAvioFlightsDbContext)context;
            _mapper = mapper;
            _logger = logger;
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

        public async Task SaveFlightSearchParametersAndResultAsync(FlightSearchParametersDto flightSearchParametersDto)
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
        public async Task<FlightOffersResponse> GetFlightOffersResponseBySearchHashCodeAsync(string searchHashCode)
        {
            try
            {
                var flightSearchParameters = await _context.FlightSearchParameters
                    .FirstOrDefaultAsync(fsp => fsp.SearchHashCode == searchHashCode);

                if (flightSearchParameters != null)
                {
                    var jsonResponseFlightSearchPayload = flightSearchParameters.JsonResponseFlightSearchPayload;
                    var flightOffersResponse = JsonConvert.DeserializeObject<FlightOffersResponse>(jsonResponseFlightSearchPayload);
                    return flightOffersResponse;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting flight offers response by search hash code");
                return null;
            }
        }

        public async Task<FlightOffersResponse> GetLastSerchedFlightOfferAsync(string last)
        {
            try
            {
                var flightSearchParameters = await _context.FlightSearchParameters
                            .FirstOrDefaultAsync(fd => fd.JsonResponseFlightSearchPayload != null);

                if (flightSearchParameters != null)
                {
                    var jsonResponseFlightSearchPayload = flightSearchParameters.JsonResponseFlightSearchPayload;
                    var flightOffersResponse = JsonConvert.DeserializeObject<FlightOffersResponse>(jsonResponseFlightSearchPayload);
                    return flightOffersResponse;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting last flight offer");
                return null;
            }
        }
    }
}
