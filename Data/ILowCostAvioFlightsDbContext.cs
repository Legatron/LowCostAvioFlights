using LowCostAvioFlights.Domain;
using LowCostAvioFlights.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LowCostAvioFlights.Data
{
    public interface ILowCostAvioFlightsDbContext
    {
        DbSet<FlightSearchParameters> FlightSearchParameters { get; set; }
    }
}
