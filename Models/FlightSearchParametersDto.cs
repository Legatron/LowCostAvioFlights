using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LowCostAvioFlights.Models
{
    public class FlightSearchParametersDto
    {
        public int Id { get; set; }
        public string OriginLocationCode { get; set; }
        public string DestinationLocationCode { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Infants { get; set; }
        public string CurrencyCode { get; set; }
        [MaxLength]
        public string? JsonResponseFlightSearchPayload { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime? Updated { get; set; }
        [ConcurrencyCheck]
        public string SearchHashCode { get; set; }
        //public SearchStatus SearchStatus { get; set; }

        [NotMapped]
        public string DBError { get; set; }
    }

    //public enum SearchStatus
    //{
    //    Complete,
    //    InProgress,
    //    Error
    //}
}
