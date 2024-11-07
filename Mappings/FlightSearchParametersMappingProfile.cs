using AutoMapper;
using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LowCostAvioFlights.Mappings
{
    public class FlightSearchParametersMappingProfile : Profile
    {
        public FlightSearchParametersMappingProfile()
        {
            CreateMap<FlightSearchParameters, FlightSearchParametersDto>();
            CreateMap<FlightSearchParametersDto, FlightSearchParameters>();
        }
    }
}
