namespace LowCostAvioFlights.Services
{
    public interface ITokenCacheService
    {
        Task<string> GetCachedTokenAsync();
        Task CacheTokenAsync(string token, TimeSpan cacheDuration);
    }
}
