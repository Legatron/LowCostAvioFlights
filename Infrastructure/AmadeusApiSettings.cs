namespace LowCostAvioFlights.Infrastructure
{
    public class AmadeusApiSettings
    {
        public string BearerToken { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string BaseUrl { get; set; }
        public string OauthTokenHttps { get; set; }
        public string EndpointFlightOffer { get; set; }
        public string EndpointAIATAAirport { get; set; }
        public string EndpointAIATAAirportSetings { get; set; }
    }
}
