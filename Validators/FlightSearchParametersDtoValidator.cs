using LowCostAvioFlights.Models;
using FluentValidation;

namespace LowCostAvioFlights.Validators
{
    public class FlightSearchParametersDtoValidator : AbstractValidator<FlightSearchParametersDto>
    {
        public FlightSearchParametersDtoValidator()
        {
            RuleFor(x => x.OriginLocationCode).NotEmpty();
            RuleFor(x => x.DestinationLocationCode).NotEmpty();
            RuleFor(x => x.DepartureDate).NotEmpty().Must(BeAfterToday);
        }

        private bool BeAfterToday(string date)
        {
            if (DateTime.TryParse(date, out DateTime parsedDate))
            {
                return parsedDate >= DateTime.Today;
            }
            return false;
        }
    }
}
