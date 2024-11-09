namespace LowCostAvioFlights.Infrastructure
{
    public interface IAmadeusOAuthClient
    {
        Task<string> GetAccessTokenAsync();
    }
}
