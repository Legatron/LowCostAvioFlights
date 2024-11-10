

namespace LowCostAvioFlights.Models
{
    public class FlightOffersResponse
    {
        public FlightOffer[] Data { get; set; }
        public Meta Meta { get; set; }
    }
    public class FlightOffer
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Source { get; set; }
        public bool InstantTicketingRequired { get; set; }
        public bool NonHomogeneous { get; set; }
        public bool OneWay { get; set; }
        public bool IsUpsellOffer { get; set; }
        public DateTime LastTicketingDate { get; set; }
        public DateTime LastTicketingDateTime { get; set; }
        public int NumberOfBookableSeats { get; set; }
        public Itinerary[] Itineraries { get; set; }
        public Price Price { get; set; }
        public PricingOptions PricingOptions { get; set; }
        public string[] ValidatingAirlineCodes { get; set; }
        public TravelerPricing[] TravelerPricings { get; set; }
    }

    public class Itinerary
    {
        public string Duration { get; set; }
        public Segment[] Segments { get; set; }
    }

    public class Segment
    {
        public Departure Departure { get; set; }
        public Arrival Arrival { get; set; }
        public string CarrierCode { get; set; }
        public string Number { get; set; }
        public Aircraft Aircraft { get; set; }
        public Operating Operating { get; set; }
        public string Duration { get; set; }
        public string Id { get; set; }
        public int NumberOfStops { get; set; }
        public bool BlacklistedInEU { get; set; }
    }

    public class Departure
    {
        public string IataCode { get; set; }
        public string Terminal { get; set; }
        public DateTime At { get; set; }
    }

    public class Arrival
    {
        public string IataCode { get; set; }
        public string Terminal { get; set; }
        public DateTime At { get; set; }
    }

    public class Aircraft
    {
        public string Code { get; set; }
    }

    public class Operating
    {
        public string CarrierCode { get; set; }
    }

    public class Price
    {
        public string Currency { get; set; }
        public decimal Total { get; set; }
        public decimal Base { get; set; }
        public Fee[] Fees { get; set; }
        public decimal GrandTotal { get; set; }
        public AdditionalService[] AdditionalServices { get; set; }
    }

    public class Fee
    {
        public decimal Amount { get; set; }
        public string Type { get; set; }
    }

    public class AdditionalService
    {
        public decimal Amount { get; set; }
        public string Type { get; set; }
    }

    public class PricingOptions
    {
        public string[] FareType { get; set; }
        public bool IncludedCheckedBagsOnly { get; set; }
    }

    public class TravelerPricing
    {
        public string TravelerId { get; set; }
        public string FareOption { get; set; }
        public string TravelerType { get; set; }
        public Price Price { get; set; }
        public FareDetailBySegment[] FareDetailsBySegment { get; set; }
    }

    public class FareDetailBySegment
    {
        public string SegmentId { get; set; }
        public string Cabin { get; set; }
        public string FareBasis { get; set; }
        public string BrandedFare { get; set; }
        public string BrandedFareLabel { get; set; }
        public string Class { get; set; }
        public IncludedCheckedBags IncludedCheckedBags { get; set; }
        public Amenity[] Amenities { get; set; }
    }

    public class IncludedCheckedBags
    {
        public int Quantity { get; set; }
    }

    public class Amenity
    {
        public string Description { get; set; }
        public bool IsChargeable { get; set; }
        public string AmenityType { get; set; }
        public AmenityProvider AmenityProvider { get; set; }
    }

    public class AmenityProvider
    {
        public string Name { get; set; }
    }
    public class Meta
    {
        public int Count { get; set; }
        public Links Links { get; set; }
    }

    public class Links
    {
        public string Self { get; set; }
    }
}
