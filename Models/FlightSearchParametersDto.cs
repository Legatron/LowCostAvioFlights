using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LowCostAvioFlights.Models
{
    /// <summary>
    /// Request parameters for flight search
    /// </summary>
    public class FlightSearchParametersDto
    {
        [SwaggerIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Origin location code, e.g. RDU
        /// </summary>
        [StringLength(3, MinimumLength = 3)]
        [Required]
        public required string OriginLocationCode { get; set; }

        /// <summary>
        /// Destination location code, e.g. MUC
        /// </summary>
        [StringLength(3, MinimumLength = 3)]
        [Required]
        public required string DestinationLocationCode { get; set; }

        /// <summary>
        /// Departure date, e.g. 2024-11-08
        /// </summary>
        [Required]
        public required string DepartureDate { get; set; }

        /// <summary>
        /// Return date, e.g. 2024-11-10
        /// </summary>
        public string? ReturnDate { get; set; }

        /// <summary>
        /// Number of adults, e.g. 1
        /// </summary>
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Please enter a positive integer value for Adults >= 1.")]
        public required int Adults { get; set; }

        /// <summary>
        /// Number of children, e.g. 0
        /// </summary>
        public int? Children { get; set; }

        /// <summary>
        /// Number of infants, e.g. 0
        /// </summary>
        public int? Infants { get; set; }

        /// <summary>
        /// Currency code, e.g. EUR
        /// </summary>
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public required string CurrencyCode { get; set; }

        [SwaggerIgnore]
        [MaxLength]
        public string? JsonResponseFlightSearchPayload { get; set; }
        [SwaggerIgnore]
        public DateTime Created { get; set; } = DateTime.UtcNow;
        [SwaggerIgnore]
        public DateTime? Updated { get; set; }
        [SwaggerIgnore]
        [ConcurrencyCheck]
        public string? SearchHashCode { get; set; }
        [SwaggerIgnore]
        public SearchStatus SearchStatus { get; set; }

        [SwaggerIgnore]
        public string? DBError { get; set; }
    }
    public enum SearchStatus
    {
        Complete,
        InProgress,
        Error
    }
}
