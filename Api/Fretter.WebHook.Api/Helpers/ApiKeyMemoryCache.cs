using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.WebHook.Api.Helpers
{
    public class ApiKeyMemoryCache
    {
        public MemoryCache _cache { get; set; }
        public ApiKeyMemoryCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024
            });
        }


        public void Set(string apiKey, string value)
        {
            var existsCache = _cache.TryGetValue(apiKey, out string cacheEntry);

            if (existsCache)
            {
                _cache.Remove(apiKey);
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetSlidingExpiration(TimeSpan.FromHours(8));

            _cache.Set(apiKey, value, cacheEntryOptions);
        }

        public string Get(string apiKey)
        {
            string cacheEntry = string.Empty;

            _cache.TryGetValue(apiKey, out cacheEntry);

            return cacheEntry;
        }
    }
}
