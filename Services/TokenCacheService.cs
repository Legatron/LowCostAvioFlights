using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace LowCostAvioFlights.Services
{
    public class TokenCacheService : ITokenCacheService
    {
        private readonly IDistributedCache _cache;
        private const string CacheKey = "OAuthToken";
        private string _cachedBearerToken;

        public TokenCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<string> GetCachedTokenAsync()
        {
            var cacheEntry = await _cache.GetAsync(CacheKey);
            if (cacheEntry != null)
            {
                return Encoding.UTF8.GetString(cacheEntry);
            }
            return null;
        }

        public async Task CacheTokenAsync(string token, TimeSpan cacheDuration)
        {
            await _cache.SetAsync(CacheKey, Encoding.UTF8.GetBytes(token),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = cacheDuration });
        }
    }
}
