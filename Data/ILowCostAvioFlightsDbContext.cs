using LowCostAvioFlights.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LowCostAvioFlights.Data
{
    public interface ILowCostAvioFlightsDbContext
    {
        DbSet<FlightSearchParameters> FlightSearchParameters { get; set; }
    }
}
