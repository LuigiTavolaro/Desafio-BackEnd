using Microsoft.Extensions.Caching.Memory;

namespace DesafioBackEndProject.Infrastructure.Cache
{
    public static class CacheManager
    {
        private static readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public static void Set<T>(string key, T value, TimeSpan expirationTime)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime,
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            _cache.Set(key, value, cacheEntryOptions);
        }

        public static T? Get<T>(string key)
        {
            _cache.TryGetValue(key, out T? value);
            return value;
        }

        public static void Remove(string key)
        {
            _cache.Remove(key);
        }


    }
}



