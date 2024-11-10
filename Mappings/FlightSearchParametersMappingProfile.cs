using AutoMapper;
using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Models;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;


namespace LowCostAvioFlights.Mappings
{
    public class FlightSearchParametersMappingProfile : Profile
    {
        public FlightSearchParametersMappingProfile()
        {
            CreateMap<FlightSearchParameters, FlightSearchParametersDto>()
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate.HasValue ? src.ReturnDate.Value.ToString() : null));

            CreateMap<FlightSearchParametersDto, FlightSearchParameters>()
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate.IsNullOrEmpty() ? null : src.ReturnDate.ToString() ));
        }
    }
}
